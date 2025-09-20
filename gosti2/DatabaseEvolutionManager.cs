using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace gosti2.Data
{
    public static class DatabaseEvolutionManager
    {
        private const string CurrentVersion = "1.1.0";

        public static void VerificarEAtualizarBanco()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var connection = context.Database.Connection as SqlConnection;
                    if (connection == null) return;

                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    // Verificar versão atual do banco
                    string versaoAtual = ObterVersaoAtual(connection);

                    if (versaoAtual != CurrentVersion)
                    {
                        var resultado = MessageBox.Show(
                            $"Seu banco de dados está na versão {versaoAtual}. " +
                            $"Deseja atualizar para a versão {CurrentVersion}?\n\n" +
                            "✅ Melhorias de performance\n✅ Novos recursos\n✅ Preparado para futuras expansões",
                            "Atualização de Banco", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            ExecutarMigracao(connection, versaoAtual);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na verificação de versão: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string ObterVersaoAtual(SqlConnection connection)
        {
            try
            {
                // Verificar se a tabela de versionamento existe
                var checkTableQuery =
                    "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SchemaVersion'";

                using (var cmd = new SqlCommand(checkTableQuery, connection))
                {
                    if ((int)cmd.ExecuteScalar() == 0)
                    {
                        return "1.0.0"; // Versão anterior ao sistema de versionamento
                    }
                }

                // Obter última versão aplicada
                var versionQuery = "SELECT TOP 1 VersionNumber FROM SchemaVersion ORDER BY AppliedDate DESC";
                using (var cmd = new SqlCommand(versionQuery, connection))
                {
                    return cmd.ExecuteScalar()?.ToString() ?? "1.0.0";
                }
            }
            catch
            {
                return "1.0.0";
            }
        }

        private static void ExecutarMigracao(SqlConnection connection, string versaoAtual)
        {
            try
            {
                // ✅ MIGRAÇÃO DA VERSÃO 1.0.0 PARA 1.1.0
                if (versaoAtual == "1.0.0")
                {
                    ExecutarMigracao_1_0_0_Para_1_1_0(connection);
                }

                // ✅ FUTURAS MIGRAÇÕES AQUI
                // if (versaoAtual == "1.1.0") { ... }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na migração: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private static void ExecutarMigracao_1_0_0_Para_1_1_0(SqlConnection connection)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // ✅ ADICIONAR NOVOS CAMPOS PARA BIO PROFISSIONAL
                    var alterScripts = new[]
                    {
                        // Adicionar campos profissionais à bio
                        @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'BioProfissional')
                        BEGIN
                            ALTER TABLE Usuarios ADD BioProfissional NVARCHAR(1000) NULL;
                        END",

                        // Adicionar campo de especialidade literária
                        @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'Especialidade')
                        BEGIN
                            ALTER TABLE Usuarios ADD Especialidade NVARCHAR(100) NULL;
                        END",

                        // Adicionar redes sociais para perfil profissional
                        @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'Twitter')
                        BEGIN
                            ALTER TABLE Usuarios ADD Twitter NVARCHAR(100) NULL;
                        END",

                        @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'Instagram')
                        BEGIN
                            ALTER TABLE Usuarios ADD Instagram NVARCHAR(100) NULL;
                        END",

                        // Adicionar sistema de avaliação de livros
                        @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_NAME = 'Livros' AND COLUMN_NAME = 'Avaliacao')
                        BEGIN
                            ALTER TABLE Livros ADD Avaliacao DECIMAL(3,2) NULL;
                        END",

                        // Criar tabela de avaliações de usuários
                        @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                          WHERE TABLE_NAME = 'Avaliacoes')
                        BEGIN
                            CREATE TABLE Avaliacoes (
                                AvaliacaoId INT IDENTITY(1,1) PRIMARY KEY,
                                LivroId INT NOT NULL,
                                UsuarioId INT NOT NULL,
                                Nota DECIMAL(2,1) NOT NULL CHECK (Nota >= 0 AND Nota <= 5),
                                Comentario NVARCHAR(500) NULL,
                                DataAvaliacao DATETIME2 NOT NULL DEFAULT GETDATE(),
                                
                                CONSTRAINT FK_Avaliacoes_Livros FOREIGN KEY (LivroId) 
                                    REFERENCES Livros(LivroId) ON DELETE CASCADE,
                                CONSTRAINT FK_Avaliacoes_Usuarios FOREIGN KEY (UsuarioId) 
                                    REFERENCES Usuarios(UsuarioId),
                                CONSTRAINT UK_LivroUsuarioAvaliacao UNIQUE (LivroId, UsuarioId)
                            );
                        END"
                    };

                    foreach (var script in alterScripts)
                    {
                        using (var cmd = new SqlCommand(script, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Registrar nova versão
                    var insertVersion = @"
                        INSERT INTO SchemaVersion (VersionNumber, Description, ScriptName)
                        VALUES ('1.1.0', 'Adição de sistema de bio profissional e avaliações', 'Migration_1.1.0.sql')";

                    using (var cmd = new SqlCommand(insertVersion, connection, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show("✅ Banco atualizado para versão 1.1.0!\n\n" +
                                  "• Bio profissional ampliada\n• Sistema de avaliações\n• Redes sociais",
                                  "Atualização Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}