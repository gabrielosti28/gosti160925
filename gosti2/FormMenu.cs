using System;
using System.Drawing;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            ConfigurarInterface();
            VerificarUsuarioLogado();
        }

        private void ConfigurarInterface()
        {
            this.Text = "BookConnect - Menu Principal";
            AtualizarInterfaceUsuario();
        }

        private void VerificarUsuarioLogado()
        {
            AtualizarInterfaceUsuario();
        }

        private void AtualizarInterfaceUsuario()
        {
            if (AppManager.EstaLogado)
            {
                var usuario = AppManager.UsuarioLogado;
                lblBoasVindas.Text = $"🌟 Olá, {usuario.NomeUsuario}!";
                lblInstrucoes.Text = "Você já está conectado. Acesse o sistema principal.";
                btnLogin.Text = "🚀 Continuar para o Sistema";
                btnLogin.BackColor = Color.FromArgb(60, 179, 113);
            }
            else
            {
                lblBoasVindas.Text = "🌟 Bem-vindo ao BookConnect!";
                lblInstrucoes.Text = "Faça login ou crie uma conta para começar.";
                btnLogin.Text = "🔑 Fazer Login";
                btnLogin.BackColor = Color.FromArgb(70, 130, 180);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppManager.EstaLogado)
                {
                    // Usuário já logado - vai direto
                    this.Hide();
                    using (var formMain = new FormPrincipal())
                    {
                        formMain.ShowDialog();
                    }
                    this.Show();
                    VerificarUsuarioLogado();
                }
                else
                {
                    // Login necessário
                    this.Hide();
                    using (var formLogin = new FormLogin())
                    {
                        if (formLogin.ShowDialog() == DialogResult.OK)
                        {
                            VerificarUsuarioLogado();
                            MessageBox.Show($"✅ Bem-vindo, {AppManager.UsuarioLogado.NomeUsuario}!",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            using (var formMain = new FormPrincipal())
                            {
                                formMain.ShowDialog();
                            }
                        }
                    }
                    this.Show();
                    VerificarUsuarioLogado();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppManager.UsuarioLogado != null)
                {
                    MessageBox.Show("📝 Você já está logado!\n\nPara criar nova conta, faça logout primeiro.", "Informação");
                    return;
                }

                this.Hide();
                using (var formCadastro = new FormCadastro())
                {
                    formCadastro.ShowDialog();
                }
                this.Show();
                VerificarUsuarioLogado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro: {ex.Message}", "Erro");
                this.Show();
            }
        }

        private void btnExplorar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                using (var formPrincipal = new FormPrincipal())
                {
                    formPrincipal.ShowDialog();
                }
                this.Show();
                VerificarUsuarioLogado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro: {ex.Message}", "Erro");
                this.Show();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair do BookConnect?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (AppManager.EstaLogado)
                {
                    AppManager.Logout();
                }
                this.Close();
            }
        }
    }
}