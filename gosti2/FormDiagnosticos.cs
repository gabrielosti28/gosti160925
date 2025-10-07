using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using gosti2.Tools;
using System.Configuration;
using gosti2.Data;
using System.Drawing;
using System.Linq;

namespace gosti2
{
    public partial class FormDiagnosticos : Form
    {
        private string GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
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

        // ✅ CORRIGIDO: Adicionado parâmetro opcional
        private void CarregarLogs(string nivelFiltro = null)
        {
            try
            {
                // ✅ CORRIGIDO: DatabaseManager como instância
                var databaseManager = new DatabaseManager();
                var logs = databaseManager.ObterLogs(500, nivelFiltro);

                dataGridViewLogs.DataSource = logs.Select(l => new
                {
                    l.DataHora,
                    l.Nivel,
                    l.Mensagem,
                    l.Formulario,
                    l.Metodo,
                    Usuario = l.Usuario?.NomeUsuario ?? "Sistema",
                    l.ExceptionType
                }).ToList();

                // ✅ CORRIGIDO: Pattern matching compatível com C# 7.3
                foreach (DataGridViewRow row in dataGridViewLogs.Rows)
                {
                    var nivel = row.Cells["Nivel"].Value?.ToString();
                    row.DefaultCellStyle.BackColor = ObterCorNivel(nivel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar logs: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CORRIGIDO: Compatível com C# 7.3
        private Color ObterCorNivel(string nivel)
        {
            if (nivel == "ERROR" || nivel == "FATAL")
                return Color.LightPink;
            else if (nivel == "WARN")
                return Color.LightYellow;
            else if (nivel == "INFO")
                return Color.LightBlue;
            else if (nivel == "DEBUG")
                return Color.LightGray;
            else
                return Color.White;
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

        // ... (os métodos Exportar, btnFiltrar, btnTestarConexao permanecem iguais) ...

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            try
            {
                DiagnosticContext.LogarInfo("Testando conexão com banco de dados");

                // ✅ ASSUMINDO que TestarConexao é estático
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
            // ... código existente ...
        }
    }

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