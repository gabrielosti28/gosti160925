using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormConfiguracaoBanco : Form
    {
        private bool conexaoTestada = false;

        public FormConfiguracaoBanco()
        {
            InitializeComponent();
            CarregarConfiguracoesAtuais();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar AutoComplete para servidores comuns
            var servidoresComuns = new string[]
            {
                @".\SQLEXPRESS",
                @"(localdb)\MSSQLLocalDB",
                @".",
                @"localhost",
                @"localhost\SQLEXPRESS"
            };

            txtServidor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtServidor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(servidoresComuns);
            txtServidor.AutoCompleteCustomSource = autoComplete;

            // ToolTips para melhor UX
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtServidor, "Exemplos:\n• .\\SQLEXPRESS (SQL Express)\n• . (Servidor local padrão)\n• (localdb)\\MSSQLLocalDB (LocalDB)\n• NomeServidor (Servidor remoto)");
            toolTip.SetToolTip(txtBanco, "Nome do banco de dados. Use CJ3027333PR2 para produção.");
            toolTip.SetToolTip(rbWindowsAuth, "Recomendado para desenvolvimento local. Usa suas credenciais do Windows.");
            toolTip.SetToolTip(rbSqlAuth, "Use quando o SQL Server exigir usuário e senha específicos.");

            // Eventos para validação em tempo real
            txtServidor.TextChanged += (s, e) => ValidarFormulario();
            txtBanco.TextChanged += (s, e) => ValidarFormulario();
            txtUsuario.TextChanged += (s, e) => ValidarFormulario();
            txtSenha.TextChanged += (s, e) => ValidarFormulario();
        }

        private void CarregarConfiguracoesAtuais()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

                if (!string.IsNullOrEmpty(connectionString))
                {
                    // Extrair valores da connection string existente
                    var builder = new SqlConnectionStringBuilder(connectionString);

                    txtServidor.Text = builder.DataSource;
                    txtBanco.Text = builder.InitialCatalog;

                    // Determinar tipo de autenticação
                    if (builder.IntegratedSecurity)
                    {
                        rbWindowsAuth.Checked = true;
                        txtUsuario.Enabled = false;
                        txtSenha.Enabled = false;
                    }
                    else
                    {
                        rbSqlAuth.Checked = true;
                        txtUsuario.Text = builder.UserID;
                        txtSenha.Text = builder.Password;
                    }
                }
                else
                {
                    // Valores padrão
                    txtServidor.Text = @".\SQLEXPRESS";
                    txtBanco.Text = "CJ3027333PR2";
                    rbWindowsAuth.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configurações: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Valores padrão de fallback
                txtServidor.Text = @".\SQLEXPRESS";
                txtBanco.Text = "CJ3027333PR2";
                rbWindowsAuth.Checked = true;
            }
        }

        private string ConstruirConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServidor.Text.Trim(),
                InitialCatalog = txtBanco.Text.Trim(),
                MultipleActiveResultSets = true,
                ConnectTimeout = 30,
                ApplicationName = "BookConnect"
            };

            if (rbWindowsAuth.Checked)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = txtUsuario.Text.Trim();
                builder.Password = txtSenha.Text;
                builder.IntegratedSecurity = false;
            }

            return builder.ConnectionString;
        }

        private bool ValidarFormulario()
        {
            bool servidorValido = !string.IsNullOrWhiteSpace(txtServidor.Text);
            bool bancoValido = !string.IsNullOrWhiteSpace(txtBanco.Text);
            bool autenticacaoValida = true;

            if (rbSqlAuth.Checked)
            {
                autenticacaoValida = !string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                                   !string.IsNullOrWhiteSpace(txtSenha.Text);
            }

            btnTestarConexao.Enabled = servidorValido && bancoValido && autenticacaoValida;
            btnSalvar.Enabled = conexaoTestada && servidorValido && bancoValido && autenticacaoValida;

            return servidorValido && bancoValido && autenticacaoValida;
        }

        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cursor = Cursors.WaitCursor;
            btnTestarConexao.Enabled = false;

            try
            {
                string connectionString = ConstruirConnectionString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Teste adicional: verificar se pode criar/alterar banco
                    string sqlVersion = "SELECT @@VERSION";
                    using (var command = new SqlCommand(sqlVersion, connection))
                    {
                        var version = command.ExecuteScalar()?.ToString();
                    }

                    conexaoTestada = true;
                    btnSalvar.Enabled = true;

                    MessageBox.Show($"✅ Conexão bem-sucedida!\n\n" +
                                  $"Servidor: {txtServidor.Text}\n" +
                                  $"Banco: {txtBanco.Text}\n" +
                                  $"Autenticação: {(rbWindowsAuth.Checked ? "Windows" : "SQL Server")}\n\n" +
                                  $"A conexão está funcionando perfeitamente.",
                                  "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException sqlEx)
            {
                conexaoTestada = false;
                btnSalvar.Enabled = false;

                string mensagemErro = ObterMensagemErroSql(sqlEx);
                MessageBox.Show($"❌ Falha na conexão:\n\n{mensagemErro}",
                    "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                conexaoTestada = false;
                btnSalvar.Enabled = false;

                MessageBox.Show($"❌ Erro inesperado: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnTestarConexao.Enabled = true;
            }
        }

        private string ObterMensagemErroSql(SqlException ex)
        {
            switch (ex.Number)
            {
                case -1:  // Cannot open database
                    return $"Não foi possível conectar ao servidor '{txtServidor.Text}'.\n" +
                           "Verifique:\n" +
                           "• O SQL Server está instalado e rodando\n" +
                           "• O nome do servidor está correto\n" +
                           "• O firewall permite conexões";

                case 4060: // Cannot open database
                    return $"O banco de dados '{txtBanco.Text}' não existe no servidor.\n" +
                           "Verifique se o nome do banco está correto ou crie o banco primeiro.";

                case 18456: // Login failed
                    return "Falha no login. Verifique:\n" +
                           "• Usuário e senha (Autenticação SQL)\n" +
                           "• Permissões do usuário do Windows";

                case 53: // Network related error
                    return "Erro de rede. Verifique:\n" +
                           "• O SQL Server está rodando\n" +
                           "• O nome do servidor está correto\n" +
                           "• A rede está disponível";

                default:
                    return $"{ex.Message}\n\nCódigo do erro: {ex.Number}";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!conexaoTestada)
            {
                MessageBox.Show("Teste a conexão antes de salvar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string novaConnectionString = ConstruirConnectionString();

                // Atualizar ou adicionar connection string
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

                // Testar se o Entity Framework funciona com a nova conexão
                TestarEntityFramework();

                MessageBox.Show("✅ Configurações salvas com sucesso!\n\n" +
                              "As configurações de banco de dados foram atualizadas.\n" +
                              "O aplicativo será reiniciado para aplicar as mudanças.",
                              "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao salvar configurações: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestarEntityFramework()
        {
            try
            {
                // Teste rápido para verificar se o Entity Framework funciona
                using (var context = new ApplicationDbContext())
                {
                    // Tenta abrir a conexão (não precisa fazer query)
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Configuração salva, mas o Entity Framework encontrou um problema: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do aplicativo?\n\n" +
                              "É necessário configurar o banco de dados para usar o BookConnect.",
                              "Confirmar Saída",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void linkAjuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("📋 Configuração do SQL Server:\n\n" +
                          "🔹 **Servidores Comuns:**\n" +
                          "• . (servidor local padrão)\n" +
                          "• .\\SQLEXPRESS (SQL Express)\n" +
                          "• (localdb)\\MSSQLLocalDB (LocalDB)\n" +
                          "• NomeServidor (servidor remoto)\n\n" +

                          "🔹 **Autenticação:**\n" +
                          "• Windows: Usa suas credenciais do Windows (recomendado)\n" +
                          "• SQL Server: Requer usuário e senha específicos\n\n" +

                          "🔹 **Banco de Dados:**\n" +
                          "• CJ3027333PR2 (banco principal)\n" +
                          "• Ou qualquer nome para testes\n\n" +

                          "💡 **Dica:** Use 'Testar Conexão' antes de salvar!",
                          "Ajuda - Configuração do Banco",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rbWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtUsuario.Enabled = !rbWindowsAuth.Checked;
            txtSenha.Enabled = !rbWindowsAuth.Checked;
            ValidarFormulario();
        }

        private void rbSqlAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtUsuario.Enabled = rbSqlAuth.Checked;
            txtSenha.Enabled = rbSqlAuth.Checked;
            ValidarFormulario();
        }

        private void btnDetectarServidores_Click(object sender, EventArgs e)
        {
            // Implementação para detectar servidores SQL na rede
            // (pode ser adicionada posteriormente)
            MessageBox.Show("Funcionalidade de detecção automática de servidores será implementada em breve.",
                "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}