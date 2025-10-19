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
            AtualizarInterfaceUsuario();
        }

        private void ConfigurarInterface()
        {
            this.Text = "BookConnect - Menu Principal";
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
                lblInstrucoes.Text = "Faça login ou crie uma conta para começar sua jornada literária.";
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
                    // Usuário já logado - vai direto para o principal
                    this.Hide();
                    using (var formPrincipal = new FormPrincipal())
                    {
                        formPrincipal.ShowDialog();
                    }
                    this.Show();
                    AtualizarInterfaceUsuario();
                }
                else
                {
                    // Precisa fazer login
                    this.Hide();
                    using (var formLogin = new FormLogin())
                    {
                        if (formLogin.ShowDialog() == DialogResult.OK)
                        {
                            AtualizarInterfaceUsuario();

                            // Vai automaticamente para o sistema principal
                            using (var formPrincipal = new FormPrincipal())
                            {
                                formPrincipal.ShowDialog();
                            }
                        }
                    }
                    this.Show();
                    AtualizarInterfaceUsuario();
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
                if (AppManager.EstaLogado)
                {
                    MessageBox.Show("📝 Você já está logado!\n\nPara criar nova conta, faça logout primeiro.",
                        "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Hide();
                using (var formCadastro = new FormCadastro())
                {
                    formCadastro.ShowDialog();
                }
                this.Show();
                AtualizarInterfaceUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Application.Exit();
            }
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var infoTela = new infoTela())
            {
                infoTela.ShowDialog();
            }
            this.Show();
            AtualizarInterfaceUsuario();
        }
    }
}