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

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Validação básica de campos vazios
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Preencha email e senha.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar cursor de espera
            this.Cursor = Cursors.WaitCursor;
            btnEntrar.Enabled = false;

            try
            {
                // Realiza login através do AppManager
                if (AppManager.Login(txtEmail.Text.Trim(), txtSenha.Text))
                {
                    MessageBox.Show($"Bem-vindo, {AppManager.UsuarioLogado.NomeUsuario}!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                // Se retornar false, o AppManager já exibiu a mensagem de erro
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao fazer login: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restaurar cursor e botão
                this.Cursor = Cursors.Default;
                btnEntrar.Enabled = true;
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            // Abre o formulário de cadastro
            using (var formCadastro = new FormCadastro())
            {
                if (formCadastro.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Cadastro realizado! Faça login.", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do aplicativo?", "Confirmar Saída",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            // Alterna visibilidade da senha
            txtSenha.PasswordChar = chkMostrarSenha.Checked ? '\0' : '•';
        }

        private void linkEsqueciSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de recuperação de senha em desenvolvimento.",
                "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Eventos de teclado para melhor UX
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

        // Validação em tempo real
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtEmail,
                string.IsNullOrWhiteSpace(txtEmail.Text) ? "Email é obrigatório" : "");
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(txtSenha,
                string.IsNullOrWhiteSpace(txtSenha.Text) ? "Senha é obrigatória" : "");
        }
    }
}