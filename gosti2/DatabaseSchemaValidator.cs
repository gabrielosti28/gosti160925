using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gosti2.Models;

namespace gosti2.Data
{
    public static class DatabaseSchemaValidator
    {
        public static bool ValidarEsquema()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var connection = context.Database.Connection as SqlConnection;
                    if (connection == null)
                        throw new InvalidOperationException("Conexão não é SqlConnection");

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var erros = new StringBuilder();

                    // Verificar cada tabela e coluna
                    erros.AppendLine(VerificarTabelaUsuarios(connection));
                    erros.AppendLine(VerificarTabelaLivros(connection));
                    erros.AppendLine(VerificarTabelaComentarios(connection));
                    erros.AppendLine(VerificarTabelaLikesDislikes(connection));

                    string resultadoErros = erros.ToString();
                    if (!string.IsNullOrWhiteSpace(resultadoErros))
                    {
                        var resultado = MessageBox.Show($"Foram encontradas diferenças no banco de dados:\n\n{resultadoErros}\nDeseja corrigir automaticamente?",
                            "Diferenças no Banco", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (resultado == DialogResult.Yes)
                        {
                            CorrigirEsquema(resultadoErros);
                            return true;
                        }
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao validar esquema: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static string VerificarTabelaUsuarios(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "Usuarios"))
            {
                erros.AppendLine("❌ Tabela 'Usuarios' não existe");
                return erros.ToString();
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "UserId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Nome", Tipo = "nvarchar", Nulo = "NO", Tamanho = 100 },
                new { Nome = "Email", Tipo = "nvarchar", Nulo = "NO", Tamanho = 100 },
                new { Nome = "Senha", Tipo = "nvarchar", Nulo = "NO", Tamanho = 100 },
                new { Nome = "DataNascimento", Tipo = "nvarchar", Nulo = "NO", Tamanho = 10 },
                new { Nome = "FotoPerfil", Tipo = "varbinary", Nulo = "YES", Tamanho = 0 },
                new { Nome = "Bio", Tipo = "nvarchar", Nulo = "YES", Tamanho = 500 },
                new { Nome = "DataCadastro", Tipo = "datetime", Nulo = "NO", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Usuarios", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        private static string VerificarTabelaLivros(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "Livros"))
            {
                erros.AppendLine("❌ Tabela 'Livros' não existe");
                return erros.ToString();
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "LivroId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Titulo", Tipo = "nvarchar", Nulo = "NO", Tamanho = 200 },
                new { Nome = "Autor", Tipo = "nvarchar", Nulo = "NO", Tamanho = 100 },
                new { Nome = "Genero", Tipo = "nvarchar", Nulo = "NO", Tamanho = 50 },
                new { Nome = "Descricao", Tipo = "nvarchar", Nulo = "YES", Tamanho = 1000 },
                new { Nome = "Capa", Tipo = "varbinary", Nulo = "YES", Tamanho = 0 },
                new { Nome = "DataAdicao", Tipo = "datetime", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Favorito", Tipo = "bit", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Lido", Tipo = "bit", Nulo = "NO", Tamanho = 0 },
                new { Nome = "UsuarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Livros", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        private static string VerificarTabelaComentarios(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "Comentarios"))
            {
                erros.AppendLine("❌ Tabela 'Comentarios' não existe");
                return erros.ToString();
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "ComentarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Texto", Tipo = "nvarchar", Nulo = "NO", Tamanho = 2000 },
                new { Nome = "DataComentario", Tipo = "datetime", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Likes", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Dislikes", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "LivroId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "UsuarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Comentarios", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        private static string VerificarTabelaLikesDislikes(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "LikesDislikes"))
            {
                erros.AppendLine("❌ Tabela 'LikesDislikes' não existe");
                return erros.ToString();
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "LikeDislikeId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "LivroId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "UsuarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "IsLike", Tipo = "bit", Nulo = "NO", Tamanho = 0 },
                new { Nome = "DataAcao", Tipo = "datetime", Nulo = "NO", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "LikesDislikes", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        private static string VerificarColuna(SqlConnection connection, string tabela, string coluna, string tipoEsperado, string nuloEsperado, int tamanhoEsperado)
        {
            var query = $@"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH 
                          FROM INFORMATION_SCHEMA.COLUMNS 
                          WHERE TABLE_NAME = '{tabela}' AND COLUMN_NAME = '{coluna}'";

            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                    return $"❌ Coluna '{coluna}' não existe na tabela '{tabela}'";

                var dataType = reader["DATA_TYPE"].ToString();
                var isNullable = reader["IS_NULLABLE"].ToString();
                var maxLength = reader["CHARACTER_MAXIMUM_LENGTH"] as int?;

                if (!dataType.Equals(tipoEsperado, StringComparison.OrdinalIgnoreCase))
                    return $"❌ Coluna '{coluna}': Tipo esperado '{tipoEsperado}', encontrado '{dataType}'";

                if (!isNullable.Equals(nuloEsperado, StringComparison.OrdinalIgnoreCase))
                    return $"❌ Coluna '{coluna}': Nulabilidade esperada '{nuloEsperado}', encontrada '{isNullable}'";

                if (tamanhoEsperado > 0 && maxLength != tamanhoEsperado)
                    return $"❌ Coluna '{coluna}': Tamanho esperado {tamanhoEsperado}, encontrado {maxLength}";

                return string.Empty;
            }
        }

        private static bool TabelaExiste(SqlConnection connection, string tabela)
        {
            var query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tabela}'";
            using (var cmd = new SqlCommand(query, connection))
            {
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private static void CorrigirEsquema(string erros)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var scriptCorrecao = GerarScriptCorrecao(erros);

                    if (!string.IsNullOrEmpty(scriptCorrecao))
                    {
                        context.Database.ExecuteSqlCommand(scriptCorrecao);
                        MessageBox.Show("Esquema do banco corrigido com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao corrigir esquema: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string GerarScriptCorrecao(string erros)
        {
            var script = new StringBuilder();

            foreach (var linha in erros.Split('\n'))
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;

                if (linha.Contains("não existe"))
                {
                    if (linha.Contains("Tabela"))
                    {
                        var tabela = linha.Split('\'')[1];
                        script.AppendLine(GerarCreateTable(tabela));
                    }
                    else if (linha.Contains("Coluna"))
                    {
                        var partes = linha.Split('\'');
                        if (partes.Length >= 4)
                        {
                            var tabela = partes[3];
                            var coluna = partes[1];
                            script.AppendLine($"ALTER TABLE {tabela} ADD {coluna} {ObterTipoColuna(coluna)};");
                        }
                    }
                }
            }

            return script.ToString();
        }

        private static string GerarCreateTable(string tabela)
        {
            switch (tabela.ToLower())
            {
                case "usuarios":
                    return @"CREATE TABLE Usuarios (
                        UserId INT IDENTITY(1,1) PRIMARY KEY,
                        Nome NVARCHAR(100) NOT NULL,
                        Email NVARCHAR(100) NOT NULL UNIQUE,
                        Senha NVARCHAR(100) NOT NULL,
                        DataNascimento NVARCHAR(10) NOT NULL,
                        FotoPerfil VARBINARY(MAX),
                        Bio NVARCHAR(500),
                        DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
                    )";

                case "livros":
                    return @"CREATE TABLE Livros (
                        LivroId INT IDENTITY(1,1) PRIMARY KEY,
                        Titulo NVARCHAR(200) NOT NULL,
                        Autor NVARCHAR(100) NOT NULL,
                        Genero NVARCHAR(50) NOT NULL,
                        Descricao NVARCHAR(1000),
                        Capa VARBINARY(MAX),
                        DataAdicao DATETIME NOT NULL DEFAULT GETDATE(),
                        Favorito BIT NOT NULL DEFAULT 0,
                        Lido BIT NOT NULL DEFAULT 0,
                        UsuarioId INT NOT NULL,
                        FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId) ON DELETE CASCADE
                    )";

                case "comentarios":
                    return @"CREATE TABLE Comentarios (
                        ComentarioId INT IDENTITY(1,1) PRIMARY KEY,
                        Texto NVARCHAR(2000) NOT NULL,
                        DataComentario DATETIME NOT NULL DEFAULT GETDATE(),
                        Likes INT NOT NULL DEFAULT 0,
                        Dislikes INT NOT NULL DEFAULT 0,
                        LivroId INT NOT NULL,
                        UsuarioId INT NOT NULL,
                        FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
                        FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId)
                    )";

                case "likesdislikes":
                    return @"CREATE TABLE LikesDislikes (
                        LikeDislikeId INT IDENTITY(1,1) PRIMARY KEY,
                        LivroId INT NOT NULL,
                        UsuarioId INT NOT NULL,
                        IsLike BIT NOT NULL,
                        DataAcao DATETIME NOT NULL DEFAULT GETDATE(),
                        FOREIGN KEY (LivroId) REFERENCES Livros(LivroId) ON DELETE CASCADE,
                        FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UserId),
                        UNIQUE (LivroId, UsuarioId)
                    )";

                default:
                    return string.Empty;
            }
        }

        private static string ObterTipoColuna(string coluna)
        {
            var mapeamento = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"UserId", "INT"},
                {"Nome", "NVARCHAR(100)"},
                {"Email", "NVARCHAR(100)"},
                {"Senha", "NVARCHAR(100)"},
                {"DataNascimento", "NVARCHAR(10)"},
                {"FotoPerfil", "VARBINARY(MAX)"},
                {"Bio", "NVARCHAR(500)"},
                {"DataCadastro", "DATETIME"},
                {"Titulo", "NVARCHAR(200)"},
                {"Autor", "NVARCHAR(100)"},
                {"Genero", "NVARCHAR(50)"},
                {"Descricao", "NVARCHAR(1000)"},
                {"Capa", "VARBINARY(MAX)"},
                {"DataAdicao", "DATETIME"},
                {"Favorito", "BIT"},
                {"Lido", "BIT"},
                {"UsuarioId", "INT"},
                {"ComentarioId", "INT"},
                {"Texto", "NVARCHAR(2000)"},
                {"DataComentario", "DATETIME"},
                {"Likes", "INT"},
                {"Dislikes", "INT"},
                {"LivroId", "INT"},
                {"LikeDislikeId", "INT"},
                {"IsLike", "BIT"},
                {"DataAcao", "DATETIME"}
            };

            return mapeamento.ContainsKey(coluna) ? mapeamento[coluna] : "NVARCHAR(255)";
        }
    }
}