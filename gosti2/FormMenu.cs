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
            // CORREÇÃO: Verifica direto do AppManager
            AtualizarInterfaceUsuario();
        }

        private void AtualizarInterfaceUsuario()
        {
            var usuarioLogado = AppManager.UsuarioLogado;

            if (usuarioLogado != null)
            {
                lblBoasVindas.Text = $"🌟 Olá, {usuarioLogado.NomeUsuario}!";
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
                if (AppManager.UsuarioLogado != null)
                {
                    // Usuário já logado - vai direto para o principal
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
                    // Usuário não logado - mostra tela de login
                    this.Hide();
                    using (var formLogin = new FormLogin())
                    {
                        if (formLogin.ShowDialog() == DialogResult.OK)
                        {
                            // Login bem-sucedido
                            VerificarUsuarioLogado();
                            MessageBox.Show($"✅ Bem-vindo de volta, {AppManager.UsuarioLogado.NomeUsuario}!", "Sucesso");

                            // Vai automaticamente para o sistema principal
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
                MessageBox.Show($"❌ Erro: {ex.Message}", "Erro");
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
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // CORREÇÃO: Logout pelo AppManager
                if (AppManager.UsuarioLogado != null)
                {
                    AppManager.Logout();
                }
                this.Close();
            }
        }
    }
}