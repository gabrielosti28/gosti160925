using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2.Tools
{
    public static class DatabaseExporter
    {

        public static string ExportarEstruturaCompleta()
        {
           
            var estrutura = new StringBuilder();
            estrutura.AppendLine("// ✅ ESTRUTURA DO BANCO - REDE SOCIAL DE LIVROS");
            estrutura.AppendLine("// Exportado em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            estrutura.AppendLine("// Banco: CJ3027333PR2");
            estrutura.AppendLine("");

           

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var connection = context.Database.Connection as SqlConnection;
                    if (connection == null)
                    {
                        MessageBox.Show("Não foi possível obter conexão com o banco.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return estrutura.ToString();
                    }

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    // ✅ EXPORTAÇÃO COMPLETA E ORGANIZADA
                    ExportarTabelasComDetalhes(connection, estrutura);
                    ExportarChavesPrimariasEFK(connection, estrutura);
                    ExportarIndices(connection, estrutura);
                    ExportarDadosEstatisticos(connection, estrutura);

                    // ✅ SALVAR EM ARQUIVO AUTOMATICAMENTE
                    SalvarEmArquivo(estrutura.ToString());

                    MessageBox.Show("Estrutura exportada com sucesso!\n\nArquivo salvo como: estrutura_banco.txt",
                        "Exportação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar estrutura: {ex.Message}\n\nVerifique a conexão com o banco.",
                    "Erro na Exportação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return estrutura.ToString();
        }

        private static void ExportarTabelasComDetalhes(SqlConnection connection, StringBuilder estrutura)
        {
            estrutura.AppendLine("🎯 TABELAS DO SISTEMA");
            estrutura.AppendLine("=====================");

            var query = @"
                SELECT 
                    t.TABLE_NAME,
                    CAST(obj.create_date AS DATE) as DataCriacao,
                    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_NAME = t.TABLE_NAME) as QtdColunas,
                    (SELECT SUM(p.rows) FROM sys.partitions p 
                     WHERE p.index_id IN (0,1) AND p.object_id = obj.object_id) as QtdRegistros
                FROM INFORMATION_SCHEMA.TABLES t
                INNER JOIN sys.objects obj ON obj.name = t.TABLE_NAME AND obj.type = 'U'
                WHERE t.TABLE_TYPE = 'BASE TABLE' 
                ORDER BY t.TABLE_NAME";

            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    estrutura.AppendLine($"\n📊 {reader["TABLE_NAME"]}");
                    estrutura.AppendLine($"   📅 Criada em: {reader["DataCriacao"]}");
                    estrutura.AppendLine($"   🔢 Colunas: {reader["QtdColunas"]}");
                    estrutura.AppendLine($"   📈 Registros: {reader["QtdRegistros"]}");

                    // Exportar colunas desta tabela
                    ExportarColunasDaTabela(connection, reader["TABLE_NAME"].ToString(), estrutura);
                }
            }
        }

        private static void ExportarColunasDaTabela(SqlConnection connection, string tableName, StringBuilder estrutura)
        {
            var query = @"
                SELECT 
                    COLUMN_NAME,
                    DATA_TYPE,
                    IS_NULLABLE,
                    CHARACTER_MAXIMUM_LENGTH,
                    COLUMN_DEFAULT,
                    CASE WHEN COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 
                         THEN 'IDENTITY(1,1)' ELSE '' END as IdentityInfo
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_NAME = @TableName
                ORDER BY ORDINAL_POSITION";

            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tipo = reader["DATA_TYPE"].ToString();
                        var tamanho = reader["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value ?
                            $"({reader["CHARACTER_MAXIMUM_LENGTH"]})" : "";
                        var identity = reader["IdentityInfo"].ToString();
                        var nulo = reader["IS_NULLABLE"].ToString() == "YES" ? "NULL" : "NOT NULL";
                        var padrao = reader["COLUMN_DEFAULT"] != DBNull.Value ?
                            $" DEFAULT {reader["COLUMN_DEFAULT"]}" : "";

                        estrutura.AppendLine($"   ├─ {reader["COLUMN_NAME"]} {tipo}{tamanho} {identity} {nulo}{padrao}");
                    }
                }
            }
        }

        private static void ExportarChavesPrimariasEFK(SqlConnection connection, StringBuilder estrutura)
        {
            estrutura.AppendLine("\n🔑 CHAVES PRIMÁRIAS E ESTRANGEIRAS");
            estrutura.AppendLine("==================================");

            // Chaves Primárias
            var queryPK = @"
                SELECT 
                    t.TABLE_NAME,
                    c.COLUMN_NAME
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE c 
                    ON t.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                WHERE t.CONSTRAINT_TYPE = 'PRIMARY KEY'
                ORDER BY t.TABLE_NAME";

            using (var cmd = new SqlCommand(queryPK, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    estrutura.AppendLine($"   🗝️  {reader["TABLE_NAME"]}.{reader["COLUMN_NAME"]} (PRIMARY KEY)");
                }
            }

            // Foreign Keys
            var queryFK = @"
                SELECT 
                    fk.TABLE_NAME as TabelaOrigem,
                    fk.COLUMN_NAME as ColunaOrigem,
                    pk.TABLE_NAME as TabelaDestino,
                    pk.COLUMN_NAME as ColunaDestino
                FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE fk 
                    ON rc.CONSTRAINT_NAME = fk.CONSTRAINT_NAME
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE pk 
                    ON rc.UNIQUE_CONSTRAINT_NAME = pk.CONSTRAINT_NAME
                ORDER BY fk.TABLE_NAME";

            using (var cmd = new SqlCommand(queryFK, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    estrutura.AppendLine($"   🔗 {reader["TabelaOrigem"]}.{reader["ColunaOrigem"]} → {reader["TabelaDestino"]}.{reader["ColunaDestino"]}");
                }
            }
        }

        private static void ExportarIndices(SqlConnection connection, StringBuilder estrutura)
        {
            estrutura.AppendLine("\n⚡ ÍNDICES DE PERFORMANCE");
            estrutura.AppendLine("=========================");

            var query = @"
                SELECT 
                    t.name as Tabela,
                    i.name as Indice,
                    i.type_desc as Tipo,
                    c.name as Coluna
                FROM sys.indexes i
                INNER JOIN sys.tables t ON i.object_id = t.object_id
                INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                WHERE i.is_primary_key = 0 AND i.is_unique_constraint = 0
                ORDER BY t.name, i.name";

            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                string tabelaAtual = null;

                while (reader.Read())
                {
                    var tabela = reader["Tabela"].ToString();
                    if (tabela != tabelaAtual)
                    {
                        estrutura.AppendLine($"\n   📋 {tabela}:");
                        tabelaAtual = tabela;
                    }

                    estrutura.AppendLine($"      ├─ {reader["Indice"]} ({reader["Tipo"]}) → {reader["Coluna"]}");
                }
            }
        }

        private static void ExportarDadosEstatisticos(SqlConnection connection, StringBuilder estrutura)
        {
            estrutura.AppendLine("\n📊 ESTATÍSTICAS DO BANCO");
            estrutura.AppendLine("========================");

            var queries = new[]
            {
                new { Descricao = "Total de usuários", Query = "SELECT COUNT(*) FROM Usuarios" },
                new { Descricao = "Total de livros", Query = "SELECT COUNT(*) FROM Livros" },
                new { Descricao = "Total de avaliações", Query = "SELECT COUNT(*) FROM Avaliacoes" },
                new { Descricao = "Total de comentários", Query = "SELECT COUNT(*) FROM Comentarios" },
                new { Descricao = "Usuários ativos", Query = "SELECT COUNT(*) FROM Usuarios WHERE Ativo = 1" },
                new { Descricao = "Livros lidos", Query = "SELECT COUNT(*) FROM Livros WHERE Lido = 1" },
                new { Descricao = "Livros favoritos", Query = "SELECT COUNT(*) FROM Livros WHERE Favorito = 1" }
            };

            foreach (var item in queries)
            {
                try
                {
                    using (var cmd = new SqlCommand(item.Query, connection))
                    {
                        var resultado = cmd.ExecuteScalar();
                        estrutura.AppendLine($"   📈 {item.Descricao}: {resultado}");
                    }
                }
                catch
                {
                    estrutura.AppendLine($"   ⚠️  {item.Descricao}: Tabela não disponível");
                }
            }
        }

        private static void SalvarEmArquivo(string conteudo)
        {
            try
            {
                var caminho = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"estrutura_banco_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

                System.IO.File.WriteAllText(caminho, conteudo, Encoding.UTF8);

                Console.WriteLine($"✅ Arquivo salvo em: {caminho}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Não foi possível salvar arquivo: {ex.Message}");
            }
        }

        // ✅ MÉTODO SIMPLIFICADO PARA USO RÁPIDO
        public static string ExportarResumo()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var connection = context.Database.Connection as SqlConnection;
                    if (connection == null) return "Erro: Sem conexão";

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var resumo = new StringBuilder();
                    resumo.AppendLine("📋 RESUMO DO BANCO - REDE SOCIAL DE LIVROS");
                    resumo.AppendLine("==========================================");

                    var query = @"
                        SELECT 
                            TABLE_NAME,
                            (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_NAME = t.TABLE_NAME) as Colunas,
                            (SELECT SUM(p.rows) FROM sys.partitions p 
                             WHERE p.index_id IN (0,1) AND p.object_id = OBJECT_ID(t.TABLE_NAME)) as Registros
                        FROM INFORMATION_SCHEMA.TABLES t
                        WHERE t.TABLE_TYPE = 'BASE TABLE'
                        ORDER BY TABLE_NAME";

                    using (var cmd = new SqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resumo.AppendLine($"• {reader["TABLE_NAME"]} ({reader["Colunas"]} colunas, {reader["Registros"]} registros)");
                        }
                    }

                    return resumo.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao gerar resumo: {ex.Message}";
            }
        }
    }
}