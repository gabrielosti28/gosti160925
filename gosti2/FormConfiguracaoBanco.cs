using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gosti2
{
    public partial class FormConfiguracaoBanco : Form
    {
        public FormConfiguracaoBanco()
        {
            InitializeComponent();
            CarregarConfiguracoesAtuais();
        }

        private void CarregarConfiguracoesAtuais()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionString = config.ConnectionStrings.ConnectionStrings["DefaultConnection"];
                if (connectionString != null)
                {
                    txtServidor.Text = "SQLEXPRESS";
                    //txtServidor.Text = ExtrairValor(connectionString.ConnectionString, "Data Source");
                    txtBanco.Text = "CJ3027333PR2";
                }
                else
                {
                    // ⭐⭐ ALTERAÇÃO AQUI - Já coloca o valor correto!
                    txtServidor.Text = @"SQLEXPRESS";
                    txtBanco.Text = "CJ3027333PR2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configurações: {ex.Message}");
                // ⭐⭐ ALTERAÇÃO AQUI - Já coloca o valor correto!
                txtServidor.Text = @".\SQLEXPRESS";
                txtBanco.Text = "CJ3027333PR2";
            }
        }

        private string ExtrairValor(string connectionString, string chave)
        {
            if (string.IsNullOrEmpty(connectionString))
                return "";

            var partes = connectionString.Split(';');
            foreach (var parte in partes)
            {
                if (parte.StartsWith(chave + "="))
                {
                    return parte.Substring(chave.Length + 1);
                }
            }
            return "";
        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            string novaConnectionString =
                $"Data Source={txtServidor.Text};" +
                $"Initial Catalog={txtBanco.Text};" +
               $"user id= {txtUsuario.Text};" +
                    $"password= {txtSenha.Text};";
            //"Integrated Security=True;" +
            //"MultipleActiveResultSets=True";

            try
            {
                using (var connection = new SqlConnection(novaConnectionString))
                {
                    connection.Open();
                    MessageBox.Show("✅ Conexão bem-sucedida!\n\nBanco: " + txtBanco.Text +
                                  "\nServidor: " + txtServidor.Text,
                                  "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Falha na conexão: {ex.Message}\n\n" +
                              "Verifique:\n" +
                              "• SQL Server está instalado e rodando\n" +
                              "• Nome do servidor está correto\n" +
                              "• Banco de dados existe",
                              "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Garante que o nome do banco seja CJ3027333PR2
                if (!txtBanco.Text.Equals("CJ3027333PR2", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("O nome do banco deve ser: CJ3027333PR2",
                                  "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBanco.Text = "CJ3027333PR2";
                    return;
                }

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string novaConnectionString =
                    $"Data Source={txtServidor.Text};" +
                    $"Initial Catalog={txtBanco.Text};" +
                    $"user id= {txtUsuario.Text};" +
                    $"password= {txtSenha.Text};";
                   // "Integrated Security=True;" +
                   // "MultipleActiveResultSets=True";

                if (config.ConnectionStrings.ConnectionStrings["DefaultConnection"] != null)
                {
                    config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString = novaConnectionString;
                }
                else
                {
                    config.ConnectionStrings.ConnectionStrings.Add(
                        new ConnectionStringSettings("DefaultConnection", novaConnectionString, "System.Data.SqlClient"));
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                MessageBox.Show("✅ Configurações salvas com sucesso!\n\n" +
                              "Reinicie o aplicativo para aplicar as mudanças.",
                              "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao salvar configurações: {ex.Message}",
                              "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar e sair do aplicativo?",
                              "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void linkAjuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("📋 Configuração do SQL Server:\n\n" +
                          "• Servidor Local: .\n" +
                          "• SQL Express: .\\SQLEXPRESS\n" +
                          "• Servidor Remoto: NOME_DO_SERVIDOR\n\n" +
                          "• Banco: CJ3027333PR2 (fixo)\n\n" +
                          "💡 Dica: Use '.' para servidor local",
                          "Ajuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormConfiguracaoBanco_Load(object sender, EventArgs e)
        {

        }
    }
}