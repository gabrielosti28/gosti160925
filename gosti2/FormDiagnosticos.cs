using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using gosti2.Tools;
using System.Configuration;
using gosti2.Data;

namespace gosti2
{
    public partial class FormDiagnosticos : Form
    {
        // ✅ MÉTODO ALTERNATIVO PARA OBTER CONNECTION STRING
        private string GetConnectionString()
        {
            // Tenta pegar do App.config
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback para a string padrão do seu projeto
                connectionString = "Server=localhost;Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;";
            }
            return connectionString;
        }

        public FormDiagnosticos()
        {
            InitializeComponent();
            DiagnosticContext.FormularioAtual = "FormDiagnosticos";
            DiagnosticContext.MetodoAtual = "FormDiagnosticos";
        }

        private void FormDiagnosticos_Load(object sender, EventArgs e)
        {
            CarregarLogs();
            DiagnosticContext.LogarInfo("Formulário de diagnósticos aberto");
        }

        private void CarregarLogs(string filtroNivel = "TODOS")
        {
            try
            {
                // ✅ USA O MÉTODO LOCAL GetConnectionString()
                using (var conexao = new SqlConnection(GetConnectionString()))
                {
                    conexao.Open();

                    var query = "SELECT TOP 100 LogId, Nivel, Mensagem, Formulario, Metodo, DataHora " +
                               "FROM SistemaLogs ";

                    var comando = new SqlCommand();
                    comando.Connection = conexao;

                    if (filtroNivel != "TODOS")
                    {
                        query += "WHERE Nivel = @Nivel ";
                        comando.Parameters.AddWithValue("@Nivel", filtroNivel);
                    }

                    query += "ORDER BY DataHora DESC";
                    comando.CommandText = query;

                    var logs = new List<LogEntry>();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new LogEntry
                            {
                                LogId = reader.GetInt64(reader.GetOrdinal("LogId")),
                                Nivel = reader.GetString(reader.GetOrdinal("Nivel")),
                                Mensagem = reader.GetString(reader.GetOrdinal("Mensagem")),
                                Formulario = reader.IsDBNull(reader.GetOrdinal("Formulario")) ? null : reader.GetString(reader.GetOrdinal("Formulario")),
                                Metodo = reader.IsDBNull(reader.GetOrdinal("Metodo")) ? null : reader.GetString(reader.GetOrdinal("Metodo")),
                                DataHora = reader.GetDateTime(reader.GetOrdinal("DataHora"))
                            });
                        }
                    }

                    dataGridViewLogs.DataSource = logs;
                    lblLogs.Text = $"Logs do Sistema: ({logs.Count} registros)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar logs: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DiagnosticContext.LogarErro("Falha ao carregar logs no formulário", ex);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            DiagnosticContext.LogarInfo("Atualizando lista de logs");
            CarregarLogs(cmbNivelFiltro.SelectedItem?.ToString() ?? "TODOS");
        }

        private void btnLimparLogs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja limpar TODOS os logs?\n\nEsta ação não pode ser desfeita.",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // ✅ USA O MÉTODO LOCAL GetConnectionString()
                    using (var conexao = new SqlConnection(GetConnectionString()))
                    {
                        conexao.Open();
                        var comando = new SqlCommand("DELETE FROM SistemaLogs", conexao);
                        comando.ExecuteNonQuery();

                        DiagnosticContext.LogarInfo("Logs do sistema limpos pelo usuário");
                    }

                    CarregarLogs();
                    MessageBox.Show("Logs limpos com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao limpar logs: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DiagnosticContext.LogarErro("Falha ao limpar logs", ex);
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Arquivo CSV (*.csv)|*.csv|Arquivo Texto (*.txt)|*.txt";
                    saveDialog.Title = "Exportar Logs";
                    saveDialog.FileName = $"logs_sistema_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        List<LogEntry> logs;

                        // ✅ USA O MÉTODO LOCAL GetConnectionString()
                        using (var conexao = new SqlConnection(GetConnectionString()))
                        {
                            conexao.Open();
                            var comando = new SqlCommand(
                                "SELECT LogId, Nivel, Mensagem, Formulario, Metodo, DataHora " +
                                "FROM SistemaLogs ORDER BY DataHora DESC",
                                conexao);

                            using (var reader = comando.ExecuteReader())
                            {
                                logs = new List<LogEntry>();
                                while (reader.Read())
                                {
                                    logs.Add(new LogEntry
                                    {
                                        LogId = reader.GetInt64(0),
                                        Nivel = reader.GetString(1),
                                        Mensagem = reader.GetString(2),
                                        Formulario = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        Metodo = reader.IsDBNull(4) ? null : reader.GetString(4),
                                        DataHora = reader.GetDateTime(5)
                                    });
                                }
                            }
                        }

                        if (Path.GetExtension(saveDialog.FileName).ToLower() == ".csv")
                        {
                            ExportarParaCSV(logs, saveDialog.FileName);
                        }
                        else
                        {
                            ExportarParaTXT(logs, saveDialog.FileName);
                        }

                        MessageBox.Show($"Logs exportados com sucesso!\n\n{saveDialog.FileName}",
                            "Exportação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DiagnosticContext.LogarInfo($"Logs exportados para: {saveDialog.FileName}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao exportar logs: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DiagnosticContext.LogarErro("Falha ao exportar logs", ex);
            }
        }

        private void ExportarParaCSV(List<LogEntry> logs, string caminho)
        {
            using (var writer = new StreamWriter(caminho))
            {
                writer.WriteLine("DataHora;Nivel;Formulario;Metodo;Mensagem");
                foreach (var log in logs)
                {
                    var linha = $"\"{log.DataHora:yyyy-MM-dd HH:mm:ss}\";" +
                               $"\"{log.Nivel}\";" +
                               $"\"{log.Formulario ?? "N/A"}\";" +
                               $"\"{log.Metodo ?? "N/A"}\";" +
                               $"\"{log.Mensagem.Replace("\"", "\"\"")}\"";
                    writer.WriteLine(linha);
                }
            }
        }

        private void ExportarParaTXT(List<LogEntry> logs, string caminho)
        {
            using (var writer = new StreamWriter(caminho))
            {
                writer.WriteLine("=== LOGS DO SISTEMA ===");
                writer.WriteLine($"Exportado em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Total de registros: {logs.Count}");
                writer.WriteLine(new string('=', 50));
                writer.WriteLine();

                foreach (var log in logs)
                {
                    writer.WriteLine($"[{log.DataHora:yyyy-MM-dd HH:mm:ss}] {log.Nivel}");
                    writer.WriteLine($"Formulário: {log.Formulario ?? "N/A"}");
                    writer.WriteLine($"Método: {log.Metodo ?? "N/A"}");
                    writer.WriteLine($"Mensagem: {log.Mensagem}");
                    writer.WriteLine(new string('-', 80));
                }
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            var nivel = cmbNivelFiltro.SelectedItem?.ToString() ?? "TODOS";
            DiagnosticContext.LogarInfo($"Filtrando logs por nível: {nivel}");
            CarregarLogs(nivel);
        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            try
            {
                DiagnosticContext.LogarInfo("Testando conexão com banco de dados");

                var sucesso = DatabaseManager.TestarConexao();

                if (sucesso)
                {
                    MessageBox.Show("✅ Conexão com o banco de dados estabelecida com sucesso!",
                        "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DiagnosticContext.LogarInfo("Teste de conexão com banco: SUCESSO");
                }
                else
                {
                    MessageBox.Show("❌ Falha na conexão com o banco de dados!",
                        "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DiagnosticContext.LogarErro("Teste de conexão com banco: FALHA",
                        new Exception("DatabaseManager.TestarConexao() retornou false"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao testar conexão: {ex.Message}",
                    "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DiagnosticContext.LogarErro("Erro durante teste de conexão", ex);
            }
        }

        private void dataGridViewLogs_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewLogs.CurrentRow?.DataBoundItem is LogEntry log)
            {
                txtDetalhes.Text = $"=== DETALHES DO LOG ===\r\n" +
                                 $"\r\n" +
                                 $"📅 Data/Hora: {log.DataHora:dd/MM/yyyy HH:mm:ss}\r\n" +
                                 $"🔴 Nível: {log.Nivel}\r\n" +
                                 $"📋 Formulário: {log.Formulario ?? "N/A"}\r\n" +
                                 $"⚙️  Método: {log.Metodo ?? "N/A"}\r\n" +
                                 $"\r\n" +
                                 $"📝 Mensagem:\r\n" +
                                 $"{log.Mensagem}\r\n" +
                                 $"\r\n" +
                                 $"ID do Log: {log.LogId}";
            }
        }
    }

    // Classe auxiliar para binding
    public class LogEntry
    {
        public long LogId { get; set; }
        public string Nivel { get; set; }
        public string Mensagem { get; set; }
        public string Formulario { get; set; }
        public string Metodo { get; set; }
        public DateTime DataHora { get; set; }
    }
}