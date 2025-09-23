using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;

namespace gosti2.Data
{
    public static class DatabaseManager
    {
        public static bool TestarConexao()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return context.Database.Exists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na conexão: {ex.Message}\n\nVerifique:\n1. SQL Server está rodando\n2. Banco CJ3027333PR2 existe\n3. Servidor acessível",
                    "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private static bool VerificarTabelasPrincipais()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Verifica se a tabela principal existe
                    return context.Database.SqlQuery<int?>(
                        "SELECT COUNT(*) FROM sys.tables WHERE name = 'Usuarios'").FirstOrDefault() > 0;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void GarantirBancoCriado()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Cria o banco se não existir (usando Entity Framework)
                    context.Database.CreateIfNotExists();

                    // Substitua por uma verificação básica
                    if (!VerificarTabelasPrincipais())
                    {
                        MessageBox.Show("Schema do banco precisa ser atualizado. Execute a migração.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Console.WriteLine("✅ Banco validado com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar/validar banco: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODO NOVO: Verificar e criar tabelas específicas se necessário
        public static void VerificarTabelasEspecificas()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Scripts de fallback caso alguma tabela específica falte
                    var scriptsFallback = new[]
                    {
                        // Script para SchemaVersion (controle de migrações)
                        @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SchemaVersion')
                        CREATE TABLE SchemaVersion (
                            VersionId INT IDENTITY(1,1) PRIMARY KEY,
                            VersionNumber VARCHAR(20) NOT NULL,
                            Description NVARCHAR(500) NOT NULL,
                            AppliedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
                            ScriptName NVARCHAR(255) NOT NULL
                        )",

                        // Script para CategoriaTiers (importante para o sistema de tiers)
                        @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CategoriaTiers')
                        BEGIN
                            CREATE TABLE CategoriaTiers (
                                CategoriaTierId INT IDENTITY(1,1) PRIMARY KEY,
                                Nome NVARCHAR(50) NOT NULL,
                                Descricao NVARCHAR(255) NULL,
                                Nivel INT NOT NULL DEFAULT 1,
                                Cor NVARCHAR(20) NULL DEFAULT '#000000'
                            )
                            
                            -- Inserir dados padrão
                            INSERT INTO CategoriaTiers (Nome, Descricao, Nivel, Cor) VALUES
                            ('Iniciante', 'Leitor iniciante', 1, '#4CAF50'),
                            ('Intermediário', 'Leitor frequente', 2, '#2196F3'),
                            ('Avançado', 'Leitor experiente', 3, '#FF9800'),
                            ('Expert', 'Crítico literário', 4, '#F44336')
                        END"
                    };

                    foreach (var script in scriptsFallback)
                    {
                        context.Database.ExecuteSqlCommand(script);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Aviso na verificação de tabelas: {ex.Message}");
                // Não mostra MessageBox para não assustar o usuário
            }
        }

        // ✅ MÉTODO MELHORADO: Migração segura
        public static void ExecutarMigrationSegura()
        {
            try
            {
                // Usar inicializador mais seguro
                Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

                using (var context = new ApplicationDbContext())
                {
                    // Força a criação do banco/seeds se necessário
                    context.Database.Initialize(true);

                    // Verifica tabelas específicas
                    VerificarTabelasEspecificas();

                    MessageBox.Show("Banco migrado/validado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na migração: {ex.Message}\n\nDica: Verifique se o SQL Server está acessível.",
                    "Erro na Migração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODO NOVO: Reset completo (apenas para desenvolvimento)
        public static void ResetarBancoDesenvolvimento()
        {
            if (MessageBox.Show("Isso apagará TODOS os dados. Continuar?",
                "Reset do Banco", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
                    using (var context = new ApplicationDbContext())
                    {
                        context.Database.Initialize(true);
                    }
                    MessageBox.Show("Banco resetado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao resetar banco: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}