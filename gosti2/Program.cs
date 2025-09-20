using System;
using System.Windows.Forms;
using gosti;
using gosti2.Data;
using gosti2.Models;
using System.Data.SqlClient;
using gosti2.Tools;

namespace gosti2
{
    namespace gosti2
    {
        static class Program
        {
            [STAThread]
            static void Main()
            {
                //gosti2.Tools.ReferenceVerifier.VerificarTodasReferencias();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // ✅ PRIMEIRO: Verificar todas as referências
                ReferenceVerifier.VerificarTodasReferencias();

                // ✅ DEPOIS: Continuar com a inicialização normal
                if (!DatabaseSchemaValidator.ValidarEsquema())
                {
                    MessageBox.Show("O banco de dados precisa ser corrigido para continuar.",
                        "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // ✅ PRIMEIRO: Validar o esquema do banco
                    if (!DatabaseSchemaValidator.ValidarEsquema())
                    {
                        MessageBox.Show("O banco de dados precisa ser corrigido para continuar.",
                            "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ✅ DEPOIS: Garantir que o banco existe e está atualizado
                    DatabaseManager.GarantirBancoCriado();

                    // ✅ SÓ ENTÃO: Testar conexão
                    if (!DatabaseManager.TestarConexao())
                    {
                        using (var formConfig = new FormConfiguracaoBanco())
                        {
                            if (formConfig.ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }

                    // ✅ TELA DE BOAS-VINDAS
                    using (var formMain = new FormMain())
                    {
                        if (formMain.ShowDialog() == DialogResult.OK)
                        {
                            ExecutarAplicacao();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro inicial: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private static void ExecutarAplicacao()
            {
                // ... implementação existente ...
            }
        }
    }
}
//USE[CJ3027333PR2]
//GO

//-- Corrigir tabelas existentes
//IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
//BEGIN
//    EXEC sp_rename 'Users', 'Usuarios';
//END

//IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Livroes')
//BEGIN
//    EXEC sp_rename 'Livroes', 'Livros';
//END

//-- Garantir que todas as colunas necessárias existam
//IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Livros' AND COLUMN_NAME = 'Genero')
//BEGIN
//    ALTER TABLE Livros ADD Genero NVARCHAR(50) NOT NULL DEFAULT 'Geral';
//END

//-- Criar tabelas que podem estar faltando
//IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'LikesDislikes')
//BEGIN
//    CREATE TABLE LikesDislikes (
//        LikeDislikeId INT IDENTITY(1,1) PRIMARY KEY,
//        LivroId INT NOT NULL,
//        UsuarioId INT NOT NULL,
//        IsLike BIT NOT NULL,
//        DataAcao DATETIME NOT NULL DEFAULT GETDATE(),
//        FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
//        FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId),
//        CONSTRAINT UK_LivroUsuario UNIQUE (LivroId, UsuarioId)
//    );
//END

//-- Criar índices para performance
//IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Usuarios_Email')
//BEGIN
//    CREATE UNIQUE INDEX IX_Usuarios_Email ON Usuarios(Email);
//END

//IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Comentarios_LivroId')
//BEGIN
//    CREATE INDEX IX_Comentarios_LivroId ON Comentarios(LivroId);
//END