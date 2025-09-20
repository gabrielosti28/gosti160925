using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using gosti2.Data; // ✅ ADICIONE ESTE USING
using gosti2.Models; // ✅ E ESTE TAMBÉM

namespace gosti2.Tools
{
    public static class DatabaseExporter
    {
        public static string ExportarEstrutura()
        {
            var estrutura = new StringBuilder();

            try
            {
                // ✅ CORRIGIDO: Agora reconhece ApplicationDbContext
                using (var context = new ApplicationDbContext())
                {
                    var connection = context.Database.Connection as SqlConnection;
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    // Exportar tabelas
                    estrutura.AppendLine("-- TABELAS EXISTENTES");
                    ExportarTabelas(connection, estrutura);

                    // Exportar colunas
                    estrutura.AppendLine("\n-- COLUNAS POR TABELA");
                    ExportarColunas(connection, estrutura);

                    MessageBox.Show("Estrutura exportada com sucesso! Copie o texto e envie para análise.",
                        "Exportação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar estrutura: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return estrutura.ToString();
        }

        private static void ExportarTabelas(SqlConnection connection, StringBuilder estrutura)
        {
            var query = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
                         WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME";

            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    estrutura.AppendLine($"• {reader["TABLE_NAME"]}");
                }
            }
        }

        private static void ExportarColunas(SqlConnection connection, StringBuilder estrutura)
        {
            var query = @"SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, 
                         IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH
                         FROM INFORMATION_SCHEMA.COLUMNS 
                         ORDER BY TABLE_NAME, ORDINAL_POSITION";

            using (var cmd = new SqlCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                string currentTable = null;

                while (reader.Read())
                {
                    var table = reader["TABLE_NAME"].ToString();
                    if (table != currentTable)
                    {
                        estrutura.AppendLine($"\n📊 {table}:");
                        currentTable = table;
                    }

                    estrutura.AppendLine($"   {reader["COLUMN_NAME"]} ({reader["DATA_TYPE"]}" +
                        $"{(reader["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value ? $"({reader["CHARACTER_MAXIMUM_LENGTH"]})" : "")})" +
                        $" - {(reader["IS_NULLABLE"].ToString() == "YES" ? "NULL" : "NOT NULL")}");
                }
            }
        }

        private static void ExportarIndices(SqlConnection connection, StringBuilder estrutura)
        {
            // Implementação simplificada
            estrutura.AppendLine("-- Exportação de índices não implementada");
        }
    }
}