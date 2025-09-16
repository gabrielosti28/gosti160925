using System;
using System.Windows.Forms;

namespace gosti2
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("❌ Por favor, preencha todos os campos!",
                              "Campos Obrigatórios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("❌ Por favor, informe um email válido.",
                              "Email Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Feedback visual de carregamento
            btnEntrar.Text = "⏳ Entrando...";
            btnEntrar.Enabled = false;
            Application.DoEvents();

            try
            {
                if (UsuarioManager.Login(txtEmail.Text, txtSenha.Text))
                {
                    MessageBox.Show($"✅ Bem-vindo de volta, {UsuarioManager.UsuarioLogado.Nome}!",
                                  "Login Bem-sucedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ Email ou senha incorretos!\n\nVerifique suas credenciais e tente novamente.",
                                  "Falha no Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenha.Focus();
                    txtSenha.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao realizar login: {ex.Message}",
                              "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEntrar.Text = "✅ Entrar";
                btnEntrar.Enabled = true;
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var formCadastro = new FormCadastro())
            {
                if (formCadastro.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("📝 Cadastro realizado! Agora você pode fazer login.",
                                  "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void linkEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("🔒 Funcionalidade em desenvolvimento!\n\nEm breve você poderá recuperar sua senha por email.",
                          "Recuperação de Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSenha.Focus();
                e.Handled = true;
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnEntrar.PerformClick();
                e.Handled = true;
            }
        }

        private void btnEntrar_Click_1(object sender, EventArgs e)
        {
            btnEntrar_Click(sender, e);
        }
    }
}