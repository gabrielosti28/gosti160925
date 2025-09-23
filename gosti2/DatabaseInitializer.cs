using System;
using System.Data.Entity;
using gosti2.Models;
using gosti2.Data;
using System.Linq;

namespace gosti2.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize()
        {
            try
            {
                // ✅ POLÍTICA SEGURA: Não apaga dados existentes
                Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

                using (var context = new ApplicationDbContext())
                {
                    // ✅ DEIXA O ENTITY FRAMEWORK CRIAR AS TABELAS AUTOMATICAMENTE
                    context.Database.Initialize(false);

                    // ✅ VERIFICAR E CRIAR DADOS INICIAIS (SE NECESSÁRIO)
                    VerificarECriarDadosIniciais(context);

                    Console.WriteLine("✅ Banco inicializado/verificado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro na inicialização do banco: {ex.Message}");
                // Não throw - permite que a aplicação continue com funcionalidade limitada
            }
        }

        private static void VerificarECriarDadosIniciais(ApplicationDbContext context)
        {
            try
            {
                // ✅ VERIFICAR SE A TABELA CategoriaTiers EXISTE E TEM DADOS
                if (context.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.tables WHERE name = 'CategoriaTiers'").FirstOrDefault() > 0)
                {
                    // ✅ INSERIR DADOS INICIAIS DE CATEGORIA TIERS (SE TABELA ESTIVER VAZIA)
                    if (!context.Database.SqlQuery<int>("SELECT COUNT(*) FROM CategoriaTiers").Any())
                    {
                        context.Database.ExecuteSqlCommand(@"
                            INSERT INTO CategoriaTiers (Nome, Descricao, Nivel, Cor) VALUES
                            ('Iniciante', 'Leitor iniciante', 1, '#4CAF50'),
                            ('Intermediário', 'Leitor frequente', 2, '#2196F3'),
                            ('Avançado', 'Leitor experiente', 3, '#FF9800'),
                            ('Expert', 'Crítico literário', 4, '#F44336')
                        ");
                        Console.WriteLine("✅ Dados iniciais de CategoriaTiers inseridos.");
                    }
                }

                // ✅ VERIFICAR SE A TABELA SchemaVersion EXISTE E INSERIR VERSÃO INICIAL
                if (context.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.tables WHERE name = 'SchemaVersion'").FirstOrDefault() > 0)
                {
                    if (!context.Database.SqlQuery<int>("SELECT COUNT(*) FROM SchemaVersion").Any())
                    {
                        context.Database.ExecuteSqlCommand(@"
                            INSERT INTO SchemaVersion (VersionNumber, Description, ScriptName)
                            VALUES ('2.0.0', 'Schema inicial criado via DatabaseInitializer', 'DatabaseInitializer.cs')
                        ");
                        Console.WriteLine("✅ Versão inicial registrada no SchemaVersion.");
                    }
                }

                // ✅ VERIFICAR USUÁRIO ADMIN PADRÃO (OPCIONAL - PARA TESTES)
                if (context.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.tables WHERE name = 'Usuarios'").FirstOrDefault() > 0)
                {
                    if (!context.Database.SqlQuery<int>("SELECT COUNT(*) FROM Usuarios").Any())
                    {
                        Console.WriteLine("ℹ️  Tabela Usuarios vazia. Crie o primeiro usuário via interface.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Aviso na criação de dados iniciais: {ex.Message}");
                // Não quebra o fluxo principal
            }
        }

        // ✅ MÉTODO PARA DESENVOLVIMENTO (NÃO USAR EM PRODUÇÃO)
        public static void ResetarBancoParaDesenvolvimento()
        {
            if (System.Windows.Forms.MessageBox.Show(
                "🚨 ISSO APAGARÁ TODOS OS DADOS! 🚨\n\nContinuar apenas para desenvolvimento?",
                "Reset de Banco",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
                    using (var context = new ApplicationDbContext())
                    {
                        context.Database.Initialize(true);
                        Console.WriteLine("✅ Banco resetado para desenvolvimento.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao resetar banco: {ex.Message}");
                }
            }
        }

        // ✅ MÉTODO PARA VERIFICAR ESTADO DO BANCO
        public static string VerificarEstadoBanco()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var tabelas = context.Database.SqlQuery<string>(
                        "SELECT name FROM sys.tables ORDER BY name").ToList();

                    var estado = new System.Text.StringBuilder();
                    estado.AppendLine("📊 ESTADO DO BANCO:");
                    estado.AppendLine("===================");
                    estado.AppendLine($"• Tabelas existentes: {tabelas.Count}");

                    foreach (var tabela in tabelas)
                    {
                        var registros = context.Database.SqlQuery<int>($"SELECT COUNT(*) FROM {tabela}").FirstOrDefault();
                        estado.AppendLine($"• {tabela}: {registros} registros");
                    }

                    return estado.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"❌ Erro ao verificar estado: {ex.Message}";
            }
        }
    }
}