using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;
using System.Collections.Generic;
using System.Configuration;

namespace gosti2.Data
{
    public class DatabaseManager
    {
        // ✅ LISTA DE CONEXÕES PARA TESTE AUTOMÁTICO
        private static string[] connectionStringNames = new[]
        {
            "ApplicationDbContext",  // Nome correto para EF
            "CJ3027333PR2",
            "DefaultConnection",
            "LocalDB",
            "SQLEXPRESS"
        };

        // ✅ MÉTODO PARA OBTER STRING DE CONEXÃO FUNCIONAL
        public static string GetWorkingConnectionString()
        {
            // Primeiro tenta pegar do App.config pelo nome
            foreach (var connectionName in connectionStringNames)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings[connectionName]?.ConnectionString;
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            Console.WriteLine($"✅ Conexão bem-sucedida: {connectionName}");
                            return connectionString;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Falha na conexão {connectionName}: {ex.Message}");
                }
            }

            // Fallback: tenta conexões hardcoded
            var fallbackConnections = new[]
            {
                "Server=(localdb)\\MSSQLLocalDB;Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;",
                "Server=.\\SQLEXPRESS;Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;",
                "Data Source=.;Initial Catalog=CJ3027333PR2;Integrated Security=True;TrustServerCertificate=True;"
            };

            foreach (var connString in fallbackConnections)
            {
                try
                {
                    using (var connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        Console.WriteLine($"✅ Conexão fallback bem-sucedida");
                        return connString;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Falha na conexão fallback: {ex.Message}");
                }
            }

            throw new InvalidOperationException("Nenhuma conexão com SQL Server funcionou!");
        }

        // ✅ TESTE DE CONEXÃO MELHORADO
        public static bool TestarConexao()
        {
            try
            {
                var connectionString = GetWorkingConnectionString();

                // Testa se o banco existe e está acessível
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Verifica se o banco específico existe
                    using (var command = new SqlCommand(
                        "SELECT COUNT(*) FROM sys.databases WHERE name = 'CJ3027333PR2'", connection))
                    {
                        var result = (int)command.ExecuteScalar();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Teste de conexão falhou: {ex.Message}");
                return false;
            }
        }

        // ✅ MÉTODO PARA CRIAR BANCO SE NÃO EXISTIR
        public static void CriarBancoSeNaoExistir()
        {
            try
            {
                // Primeiro conecta sem especificar o banco
                var masterConnectionString = GetWorkingConnectionString().Replace("CJ3027333PR2", "master");

                using (var connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();

                    // Verifica se o banco existe
                    using (var checkCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM sys.databases WHERE name = 'CJ3027333PR2'", connection))
                    {
                        var exists = (int)checkCommand.ExecuteScalar() > 0;

                        if (!exists)
                        {
                            // Cria o banco
                            using (var createCommand = new SqlCommand(
                                "CREATE DATABASE [CJ3027333PR2]", connection))
                            {
                                createCommand.ExecuteNonQuery();
                                Console.WriteLine("✅ Banco CJ3027333PR2 criado com sucesso!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("✅ Banco CJ3027333PR2 já existe");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar banco: {ex.Message}", ex);
            }
        }

        // ✅ MÉTODO PARA GARANTIR QUE O BANCO ESTÁ PRONTO
        public static void GarantirBancoCriado()
        {
            try
            {
                CriarBancoSeNaoExistir();

                // Agora usa o contexto com o banco criado
                using (var context = new ApplicationDbContext())
                {
                    // Força a criação das tabelas se não existirem
                    if (!context.Database.Exists())
                    {
                        context.Database.Create();
                        Console.WriteLine("✅ Tabelas criadas com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("✅ Banco e tabelas já existem");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar/validar banco: {ex.Message}\n\n" +
                    "Verifique se o SQL Server está instalado e rodando.",
                    "Erro de Banco de Dados",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODOS DE LOG (MANTIDOS)
        public void AdicionarLog(string nivel, string mensagem, int? usuarioId = null,
            string formulario = "", string metodo = "", string stackTrace = null,
            string exceptionType = null, string innerException = null)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var log = new SistemaLog
                    {
                        Nivel = nivel,
                        Mensagem = mensagem,
                        StackTrace = stackTrace,
                        UsuarioId = usuarioId,
                        Formulario = formulario,
                        Metodo = metodo,
                        ExceptionType = exceptionType,
                        InnerException = innerException,
                        DataHora = DateTime.Now
                    };

                    context.SistemaLogs.Add(log);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao salvar log no banco: {ex.Message}");
            }
        }

        public List<SistemaLog> ObterLogs(int quantidade = 1000, string nivel = null)
        {
            using (var context = new ApplicationDbContext())
            {
                IQueryable<SistemaLog> query = context.SistemaLogs
                    .Include(l => l.Usuario)
                    .OrderByDescending(l => l.DataHora);

                if (!string.IsNullOrEmpty(nivel))
                {
                    query = query.Where(l => l.Nivel == nivel);
                }

                return query.Take(quantidade).ToList();
            }
        }
    }
}