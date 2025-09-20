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

        public static void GarantirBancoCriado()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Cria o banco se não existir
                    if (!context.Database.Exists())
                    {
                        context.Database.Create();
                        MessageBox.Show("Banco de dados criado com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Verifica e atualiza as tabelas
                    VerificarEAtualizarTabelas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar banco: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void VerificarEAtualizarTabelas()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Executa scripts SQL para garantir que todas as tabelas existam
                    var scripts = new[]
                    {
                        @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' AND xtype='U')
                          CREATE TABLE Usuarios (
                              UserId INT IDENTITY(1,1) PRIMARY KEY,
                              Nome NVARCHAR(100) NOT NULL,
                              Email NVARCHAR(100) NOT NULL UNIQUE,
                              Senha NVARCHAR(100) NOT NULL,
                              DataNascimento NVARCHAR(10) NOT NULL,
                              FotoPerfil VARBINARY(MAX),
                              Bio NVARCHAR(500),
                              DataCadastro DATETIME NOT NULL
                          )",

                        @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Livros' AND xtype='U')
                          CREATE TABLE Livros (
                              LivroId INT IDENTITY(1,1) PRIMARY KEY,
                              Titulo NVARCHAR(200) NOT NULL,
                              Autor NVARCHAR(100) NOT NULL,
                              Genero NVARCHAR(50) NOT NULL,
                              Descricao NVARCHAR(1000),
                              Capa VARBINARY(MAX),
                              DataAdicao DATETIME NOT NULL,
                              Favorito BIT NOT NULL DEFAULT 0,
                              Lido BIT NOT NULL DEFAULT 0,
                              UsuarioId INT NOT NULL,
                              FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId) ON DELETE CASCADE
                          )",

                        @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Comentarios' AND xtype='U')
                          CREATE TABLE Comentarios (
                              ComentarioId INT IDENTITY(1,1) PRIMARY KEY,
                              Texto NVARCHAR(2000) NOT NULL,
                              DataComentario DATETIME NOT NULL,
                              Likes INT NOT NULL DEFAULT 0,
                              Dislikes INT NOT NULL DEFAULT 0,
                              LivroId INT NOT NULL,
                              UsuarioId INT NOT NULL,
                              FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
                              FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId)
                          )",

                        @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LikesDislikes' AND xtype='U')
                          CREATE TABLE LikesDislikes (
                              LikeDislikeId INT IDENTITY(1,1) PRIMARY KEY,
                              LivroId INT NOT NULL,
                              UsuarioId INT NOT NULL,
                              IsLike BIT NOT NULL,
                              DataAcao DATETIME NOT NULL,
                              FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
                              FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId),
                              UNIQUE (LivroId, UsuarioId)
                          )"
                    };

                    foreach (var script in scripts)
                    {
                        context.Database.ExecuteSqlCommand(script);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar tabelas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExecutarMigration()
        {
            try
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

                using (var context = new ApplicationDbContext())
                {
                    context.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro na migration: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}