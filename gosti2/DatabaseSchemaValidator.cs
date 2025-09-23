using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                    var avisos = new StringBuilder();

                    Console.WriteLine("🔍 Validando esquema do banco...");

                    // ✅ VERIFICAR TODAS AS TABELAS DO NOSSO SCHEMA
                    erros.AppendLine(VerificarTabelaUsuarios(connection));
                    erros.AppendLine(VerificarTabelaLivros(connection));
                    erros.AppendLine(VerificarTabelaComentarios(connection));
                    erros.AppendLine(VerificarTabelaLikesDislikes(connection));
                    erros.AppendLine(VerificarTabelaAvaliacoes(connection));
                    erros.AppendLine(VerificarTabelaCategoriaTiers(connection));
                    erros.AppendLine(VerificarTabelaMensagens(connection));

                    string resultadoErros = erros.ToString().Trim();
                    string resultadoAvisos = avisos.ToString().Trim();

                    // ✅ SE HOUVER ERROS CRÍTICOS
                    if (!string.IsNullOrWhiteSpace(resultadoErros))
                    {
                        var mensagem = new StringBuilder();
                        mensagem.AppendLine("Foram encontradas incompatibilidades no banco de dados:\n");

                        if (!string.IsNullOrWhiteSpace(resultadoErros))
                            mensagem.AppendLine("❌ ERROS CRÍTICOS:\n" + resultadoErros);

                        if (!string.IsNullOrWhiteSpace(resultadoAvisos))
                            mensagem.AppendLine("⚠️  AVISOS:\n" + resultadoAvisos);

                        var resultado = MessageBox.Show(
                            mensagem.ToString() +
                            "\nDeseja tentar corrigir automaticamente?",
                            "Incompatibilidades no Banco",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (resultado == DialogResult.Yes)
                        {
                            return CorrigirEsquema(connection, resultadoErros);
                        }
                        return false;
                    }

                    // ✅ SE SÓ HOUVER AVISOS
                    if (!string.IsNullOrWhiteSpace(resultadoAvisos))
                    {
                        MessageBox.Show(
                            "Avisos no esquema do banco:\n\n" + resultadoAvisos +
                            "\n\nA aplicação pode funcionar com limitações.",
                            "Avisos no Banco",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    Console.WriteLine("✅ Esquema do banco validado com sucesso!");
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

        // ✅ TABELA USUARIOS (COMPATÍVEL COM NOSSO SQL)
        private static string VerificarTabelaUsuarios(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "Usuarios"))
            {
                erros.AppendLine("❌ Tabela 'Usuarios' não existe");
                return erros.ToString();
            }

            // ✅ COLUNAS COMPATIVEIS COM NOSSO SCHEMA SQL
            var colunasEsperadas = new[]
            {
                new { Nome = "UsuarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "NomeUsuario", Tipo = "nvarchar", Nulo = "NO", Tamanho = 100 }, // ✅ CORRIGIDO
                new { Nome = "Email", Tipo = "nvarchar", Nulo = "NO", Tamanho = 100 },
                new { Nome = "Senha", Tipo = "nvarchar", Nulo = "NO", Tamanho = 255 },
                new { Nome = "DataNascimento", Tipo = "date", Nulo = "NO", Tamanho = 0 }, // ✅ CORRIGIDO
                new { Nome = "FotoPerfil", Tipo = "varbinary", Nulo = "YES", Tamanho = -1 },
                new { Nome = "Bio", Tipo = "nvarchar", Nulo = "YES", Tamanho = 500 },
                new { Nome = "DataCadastro", Tipo = "datetime2", Nulo = "NO", Tamanho = 0 }, // ✅ CORRIGIDO
                new { Nome = "UltimoLogin", Tipo = "datetime2", Nulo = "YES", Tamanho = 0 },
                new { Nome = "Ativo", Tipo = "bit", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Website", Tipo = "nvarchar", Nulo = "YES", Tamanho = 255 },
                new { Nome = "Localizacao", Tipo = "nvarchar", Nulo = "YES", Tamanho = 100 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Usuarios", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        // ✅ TABELA LIVROS (COMPATÍVEL)
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
                new { Nome = "Capa", Tipo = "varbinary", Nulo = "YES", Tamanho = -1 },
                new { Nome = "DataAdicao", Tipo = "datetime2", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Favorito", Tipo = "bit", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Lido", Tipo = "bit", Nulo = "NO", Tamanho = 0 },
                new { Nome = "UsuarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "ISBN", Tipo = "nvarchar", Nulo = "YES", Tamanho = 20 },
                new { Nome = "AnoPublicacao", Tipo = "int", Nulo = "YES", Tamanho = 0 },
                new { Nome = "Editora", Tipo = "nvarchar", Nulo = "YES", Tamanho = 100 },
                new { Nome = "Paginas", Tipo = "int", Nulo = "YES", Tamanho = 0 },
                new { Nome = "CategoriaTierId", Tipo = "int", Nulo = "YES", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Livros", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        // ✅ TABELAS ADICIONAIS DO NOSSO SCHEMA
        private static string VerificarTabelaAvaliacoes(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "Avaliacoes"))
            {
                erros.AppendLine("⚠️  Tabela 'Avaliacoes' não existe (funcionalidade opcional)");
                return erros.ToString(); // ✅ AVISO, NÃO ERRO
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "AvaliacaoId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "LivroId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "UsuarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Nota", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Comentario", Tipo = "nvarchar", Nulo = "YES", Tamanho = 1000 },
                new { Nome = "DataAvaliacao", Tipo = "datetime2", Nulo = "NO", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Avaliacoes", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        private static string VerificarTabelaCategoriaTiers(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "CategoriaTiers"))
            {
                erros.AppendLine("⚠️  Tabela 'CategoriaTiers' não existe (sistema de tiers)");
                return erros.ToString();
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "CategoriaTierId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Nome", Tipo = "nvarchar", Nulo = "NO", Tamanho = 50 },
                new { Nome = "Descricao", Tipo = "nvarchar", Nulo = "YES", Tamanho = 255 },
                new { Nome = "Nivel", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Cor", Tipo = "nvarchar", Nulo = "YES", Tamanho = 20 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "CategoriaTiers", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        private static string VerificarTabelaMensagens(SqlConnection connection)
        {
            var erros = new StringBuilder();

            if (!TabelaExiste(connection, "Mensagens"))
            {
                erros.AppendLine("⚠️  Tabela 'Mensagens' não existe (sistema de chat)");
                return erros.ToString();
            }

            var colunasEsperadas = new[]
            {
                new { Nome = "MensagemId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "RemetenteId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "DestinatarioId", Tipo = "int", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Texto", Tipo = "nvarchar", Nulo = "NO", Tamanho = 2000 },
                new { Nome = "DataEnvio", Tipo = "datetime2", Nulo = "NO", Tamanho = 0 },
                new { Nome = "Lida", Tipo = "bit", Nulo = "NO", Tamanho = 0 }
            };

            foreach (var coluna in colunasEsperadas)
            {
                string erro = VerificarColuna(connection, "Mensagens", coluna.Nome, coluna.Tipo, coluna.Nulo, coluna.Tamanho);
                if (!string.IsNullOrEmpty(erro))
                    erros.AppendLine(erro);
            }

            return erros.ToString();
        }

        // ✅ MÉTODOS AUXILIARES (MANTIDOS - JÁ ESTAVAM CORRETOS)
        private static string VerificarColuna(SqlConnection connection, string tabela, string coluna, string tipoEsperado, string nuloEsperado, int tamanhoEsperado)
        {
            var query = @"SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH 
                         FROM INFORMATION_SCHEMA.COLUMNS 
                         WHERE TABLE_NAME = @Tabela AND COLUMN_NAME = @Coluna";

            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Tabela", tabela);
                cmd.Parameters.AddWithValue("@Coluna", coluna);

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
        }

        private static bool TabelaExiste(SqlConnection connection, string tabela)
        {
            var query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @Tabela";
            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Tabela", tabela);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private static bool CorrigirEsquema(SqlConnection connection, string erros)
        {
            try
            {
                MessageBox.Show("A correção automática do esquema será implementada em versões futuras.\n\n" +
                              "Por favor, execute o script SQL completo novamente para recriar o banco.",
                              "Correção Manual Necessária",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao corrigir esquema: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}