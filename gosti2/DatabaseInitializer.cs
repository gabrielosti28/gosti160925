using System;
using System.Data.Entity;

namespace gosti2
{
    public class DatabaseInitializer
    {
        public static void Initialize()
        {
            try
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

                using (var context = new ApplicationDbContext())
                {
                    context.Database.Initialize(false);
                    context.Database.ExecuteSqlCommand(@"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comentarios')
                        BEGIN
                            CREATE TABLE Comentarios (
                                ComentarioId INT IDENTITY(1,1) PRIMARY KEY,
                                Texto NVARCHAR(MAX) NOT NULL,
                                DataComentario DATETIME NOT NULL DEFAULT GETDATE(),
                                LivroId INT NOT NULL,
                                UsuarioId INT NOT NULL,
                                Likes INT NOT NULL DEFAULT 0,
                                Dislikes INT NOT NULL DEFAULT 0,
                                CONSTRAINT FK_Comentarios_Livros FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
                                CONSTRAINT FK_Comentarios_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId)
                            );
                        END

                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LikesDislikes')
                        BEGIN
                            CREATE TABLE LikesDislikes (
                                LikeDislikeId INT IDENTITY(1,1) PRIMARY KEY,
                                LivroId INT NOT NULL,
                                UsuarioId INT NOT NULL,
                                IsLike BIT NOT NULL,
                                DataAcao DATETIME NOT NULL DEFAULT GETDATE(),
                                CONSTRAINT FK_LikesDislikes_Livros FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
                                CONSTRAINT FK_LikesDislikes_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId),
                                CONSTRAINT UK_LivroUsuario UNIQUE (LivroId, UsuarioId)
                            );
                        END
                    ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na inicialização do banco: {ex.Message}");
            }
        }
    }
}