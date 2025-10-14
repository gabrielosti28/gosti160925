using System;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // ✅ EVENTOS CORRETOS - como o Designer espera
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha email e senha", "Aviso");
                return;
            }

            // Testa conexão primeiro
            if (!DatabaseService.TestarConexao())
                return;

            if (DatabaseService.Login(txtEmail.Text.Trim(), txtSenha.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Email ou senha incorretos", "Erro");
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            var formCadastro = new FormCadastro();
            if (formCadastro.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Cadastro realizado! Faça login.", "Sucesso");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = chkMostrarSenha.Checked ? '\0' : '•';
        }

        private void linkEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Funcionalidade em desenvolvimento!", "Em Breve");
        }

        // ✅ EVENTOS ADICIONAIS para melhor UX
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // Validação básica em tempo real
            errorProvider.SetError(txtEmail,
                string.IsNullOrWhiteSpace(txtEmail.Text) ? "Email é obrigatório" : "");
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            // Validação básica em tempo real
            errorProvider.SetError(txtSenha,
                string.IsNullOrWhiteSpace(txtSenha.Text) ? "Senha é obrigatória" : "");
        }
    }
}