using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gosti2
{
    public static class DatabaseTester
    {
        public static bool TestarConexao()
        {
            string connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na conexão: {ex.Message}\n\n" +
                              $"String de conexão usada: {connectionString}\n\n" +
                              "Verifique:\n" +
                              "1. SQL Server está rodando\n" +
                              "2. Nome do banco está correto: CJ3027333PR2\n" +
                              "3. Servidor está acessível",
                              "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void CriarBancoSeNaoExistir()
        {
            // Conecta ao master para criar o banco CJ3027333PR2 se não existir
            string masterConnectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString
                .Replace("Initial Catalog=CJ3027333PR2", "Initial Catalog=master");

            try
            {
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();

                    // Verifica se o banco CJ3027333PR2 existe
                    var commandCheck = new SqlCommand(
                        "SELECT COUNT(*) FROM sys.databases WHERE name = 'CJ3027333PR2'", connection);

                    int existe = (int)commandCheck.ExecuteScalar();

                    if (existe == 0)
                    {
                        // Cria o banco CJ3027333PR2
                        var commandCreate = new SqlCommand(
                            "CREATE DATABASE CJ3027333PR2", connection);

                        commandCreate.ExecuteNonQuery();
                        MessageBox.Show("Banco de dados CJ3027333PR2 criado com sucesso!",
                                      "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar banco CJ3027333PR2: {ex.Message}",
                              "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CriarTabelasSeNaoExistirem()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Força a criação das tabelas se o banco existir
                    context.Database.Initialize(true);

                    // Verifica se as tabelas principais foram criadas
                    var command = new SqlCommand(
                        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Users', 'Livros', 'Mensagens')",
                        new SqlConnection(System.Configuration.ConfigurationManager
                            .ConnectionStrings["DefaultConnection"].ConnectionString));

                    command.Connection.Open();
                    int tabelasExistem = (int)command.ExecuteScalar();
                    command.Connection.Close();

                    if (tabelasExistem >= 3)
                    {
                        MessageBox.Show("Tabelas criadas com sucesso no banco CJ3027333PR2!",
                                      "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar tabelas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}