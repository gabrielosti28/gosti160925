using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormLogin : Form
    {
        private BackgroundWorker loginWorker;
        private bool loginEmAndamento = false;
        private string arquivoConfiguracao = "config_usuario.txt";

        public FormLogin()
        {
            InitializeComponent();
            ConfigurarBackgroundWorker();
            ConfigurarValidacoes();
        }

        private void ConfigurarBackgroundWorker()
        {
            loginWorker = new BackgroundWorker();
            loginWorker.DoWork += LoginWorker_DoWork;
            loginWorker.RunWorkerCompleted += LoginWorker_RunWorkerCompleted;
        }

        private void ConfigurarValidacoes()
        {
            // ToolTips para melhor UX
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtEmail, "Digite seu email cadastrado");
            toolTip.SetToolTip(txtSenha, "Digite sua senha");
            toolTip.SetToolTip(chkLembrarUsuario, "Mantenha-me conectado neste computador");

            // Placeholder behavior
            txtEmail.Enter += (s, e) => {
                if (txtEmail.Text == "seu@email.com")
                    txtEmail.Text = "";
            };
            txtEmail.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                    txtEmail.Text = "seu@email.com";
            };

            // Carregar email lembrado se existir
            CarregarUsuarioLembrado();
        }

        private void CarregarUsuarioLembrado()
        {
            try
            {
                // Solução alternativa: usar arquivo de texto para armazenar configurações
                if (File.Exists(arquivoConfiguracao))
                {
                    var linhas = File.ReadAllLines(arquivoConfiguracao);
                    if (linhas.Length >= 2)
                    {
                        string emailSalvo = linhas[0];
                        bool lembrarUsuario = bool.Parse(linhas[1]);

                        if (!string.IsNullOrEmpty(emailSalvo) && lembrarUsuario)
                        {
                            txtEmail.Text = emailSalvo;
                            chkLembrarUsuario.Checked = true;
                            txtSenha.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar usuário lembrado: {ex.Message}");
            }
        }

        private void SalvarUsuarioLembrado()
        {
            try
            {
                string emailParaSalvar = chkLembrarUsuario.Checked ? txtEmail.Text.Trim() : "";
                string lembrarUsuario = chkLembrarUsuario.Checked.ToString();

                // Salvar em arquivo de texto
                File.WriteAllLines(arquivoConfiguracao, new[] { emailParaSalvar, lembrarUsuario });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar usuário lembrado: {ex.Message}");
            }
        }

        private bool ValidarCampos()
        {
            // Validação de email
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || txtEmail.Text == "seu@email.com")
            {
                MostrarErro(txtEmail, "Email é obrigatório");
                return false;
            }

            if (!ValidarEmail(txtEmail.Text.Trim()))
            {
                MostrarErro(txtEmail, "Formato de email inválido");
                return false;
            }

            // Validação de senha
            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MostrarErro(txtSenha, "Senha é obrigatória");
                return false;
            }

            LimparErros();
            return true;
        }

        private bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, emailRegex);
            }
            catch
            {
                return false;
            }
        }

        private void MostrarErro(Control controle, string mensagem)
        {
            errorProvider.SetError(controle, mensagem);
            controle.Focus();
        }

        private void LimparErros()
        {
            errorProvider.SetError(txtEmail, "");
            errorProvider.SetError(txtSenha, "");
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (loginEmAndamento)
                return;

            if (!ValidarCampos())
                return;

            // Preparar interface para login
            loginEmAndamento = true;
            btnEntrar.Enabled = false;
            btnCadastro.Enabled = false;
            btnSair.Enabled = false;
            btnEntrar.Text = "⏳ Conectando...";
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;

            // Executar login em background - usando classe tipada em vez de dynamic
            var credenciais = new CredenciaisLogin
            {
                Email = txtEmail.Text.Trim(),
                Senha = txtSenha.Text
            };
            loginWorker.RunWorkerAsync(credenciais);
        }

        // Classe para credenciais de login (evita problemas com dynamic)
        private class CredenciaisLogin
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        // Classe para resultado do login
        private class ResultadoLogin
        {
            public bool Sucesso { get; set; }
            public string Erro { get; set; }
        }

        private void LoginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var credenciais = e.Argument as CredenciaisLogin;

            try
            {
                // Simular um pequeno delay para melhor UX
                System.Threading.Thread.Sleep(500);

                //bool loginSucesso = UsuarioManager.Login(credencias.Email, credenciais.Senha);
                //e.Result = new ResultadoLogin { Sucesso = loginSucesso, Erro = null };
            }
            catch (Exception ex)
            {
                // ✅ ADICIONE ESTE LOG DE ERRO AQUI:
                var dbManager = new DatabaseManager();
                dbManager.AdicionarLog("ERROR", ex.Message,
                    null, "FormLogin", "LoginWorker_DoWork",
                    ex.StackTrace, ex.GetType().Name, ex.InnerException?.Message);

                e.Result = new ResultadoLogin { Sucesso = false, Erro = ex.Message };
            }
        }

        private void LoginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Restaurar interface
            loginEmAndamento = false;
            btnEntrar.Enabled = true;
            btnCadastro.Enabled = true;
            btnSair.Enabled = true;
            btnEntrar.Text = "✅ Entrar";
            progressBar.Visible = false;

            if (e.Error != null)
            {
                MessageBox.Show($"❌ Erro inesperado: {e.Error.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultado = e.Result as ResultadoLogin;

            if (resultado.Sucesso)
            {
                // Salvar preferência de "lembrar usuário"
                SalvarUsuarioLembrado();

                // Login bem-sucedido

                string nomeUsuario = UsuarioManager.UsuarioLogado?.NomeUsuario ?? "Usuário";

                // ✅ ADICIONE ESTE LOG AQUI:
                var dbManager = new DatabaseManager();
                dbManager.AdicionarLog("INFO", $"Login realizado: {nomeUsuario}",
                    UsuarioManager.UsuarioLogado?.UsuarioId, "FormLogin", "btnEntrar_Click");

                MessageBox.Show($"✅ Bem-vindo de volta, {nomeUsuario}!",
                    "Login Bem-sucedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Login falhou
                string mensagemErro = resultado.Erro ?? "Email ou senha incorretos!";

                MessageBox.Show($"❌ Falha no login: {mensagemErro}\n\n" +
                    "Verifique suas credenciais e tente novamente.",
                    "Falha no Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtSenha.Focus();
                txtSenha.SelectAll();
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            if (loginEmAndamento)
                return;

            this.Hide();
            using (var formCadastro = new FormCadastro())
            {
                var resultado = formCadastro.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    // Preencher automaticamente o email do novo cadastro
                    // Usando reflexão para acessar a propriedade se existir
                    var emailProperty = formCadastro.GetType().GetProperty("EmailCadastrado");
                    if (emailProperty != null)
                    {
                        var emailCadastrado = emailProperty.GetValue(formCadastro) as string;
                        if (!string.IsNullOrEmpty(emailCadastrado))
                        {
                            txtEmail.Text = emailCadastrado;
                            txtSenha.Focus();
                        }
                    }

                    MessageBox.Show("📝 Cadastro realizado com sucesso!\n\nAgora você pode fazer login.",
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (loginEmAndamento)
                return;

            if (MessageBox.Show("Deseja realmente sair do BookConnect?",
                "Confirmar Saída", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void linkEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || txtEmail.Text == "seu@email.com")
            {
                MessageBox.Show("🔒 Para recuperar sua senha, primeiro digite seu email no campo acima.",
                    "Email Necessário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }

            if (!ValidarEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("🔒 Por favor, digite um email válido para recuperar sua senha.",
                    "Email Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Simular envio de email de recuperação
            MessageBox.Show($"📧 Um link de recuperação de senha foi enviado para:\n\n{txtEmail.Text.Trim()}\n\n" +
                "Verifique sua caixa de entrada e siga as instruções para redefinir sua senha.",
                "Recuperação de Senha Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !loginEmAndamento)
            {
                if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    txtSenha.Focus();
                }
                else
                {
                    btnEntrar.PerformClick();
                }
                e.Handled = true;
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !loginEmAndamento)
            {
                btnEntrar.PerformClick();
                e.Handled = true;
            }
        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // Validação em tempo real do email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && txtEmail.Text != "seu@email.com")
            {
                if (ValidarEmail(txtEmail.Text.Trim()))
                {
                    errorProvider.SetError(txtEmail, "");
                }
                else if (txtEmail.Text.Contains("@"))
                {
                    errorProvider.SetError(txtEmail, "Formato de email inválido");
                }
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            // Limpar erro da senha quando o usuário começar a digitar
            if (!string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                errorProvider.SetError(txtSenha, "");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Garantir que o background worker seja parado corretamente
            if (loginWorker.IsBusy)
            {
                loginWorker.CancelAsync();
            }
            base.OnFormClosing(e);
        }
    }
}