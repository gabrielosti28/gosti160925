using System;
using System.Drawing;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormCadastro : Form
    {
        public FormCadastro()
        {
            InitializeComponent();
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            // Configurações de máscara e password
            txtDataNascimento.Mask = "00/00/0000";
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;

            // Foco no primeiro campo
            txtNomeUsuario.Focus();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Validações básicas
            if (!ValidarCampos())
                return;

            // Confirmar senhas
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("As senhas não coincidem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarSenha.Focus();
                return;
            }

            // Validar data de nascimento
            if (!DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
            {
                MessageBox.Show("Data de nascimento inválida.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return;
            }

            // Cursor de espera
            this.Cursor = Cursors.WaitCursor;
            btnCadastrar.Enabled = false;

            try
            {
                // Realiza cadastro através do AppManager
                if (AppManager.CadastrarUsuario(
                    txtNomeUsuario.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtSenha.Text,
                    dataNascimento,
                    txtBio.Text.Trim()))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado ao cadastrar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnCadastrar.Enabled = true;
            }
        }

        private bool ValidarCampos()
        {
            // Validar nome
            if (string.IsNullOrWhiteSpace(txtNomeUsuario.Text) || txtNomeUsuario.Text.Length < 3)
            {
                MessageBox.Show("Nome de usuário deve ter pelo menos 3 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeUsuario.Focus();
                return false;
            }

            // Validar email
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email inválido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validar senha
            if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                MessageBox.Show("Senha deve ter pelo menos 6 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }

            // Validar data
            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text) || txtDataNascimento.Text == "  /  /")
            {
                MessageBox.Show("Data de nascimento é obrigatória.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return false;
            }

            return true;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar o cadastro?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Eventos para melhorar UX com a máscara de data
        private void txtDataNascimento_Enter(object sender, EventArgs e)
        {
            if (txtDataNascimento.Text == "  /  /")
            {
                txtDataNascimento.Text = "";
            }
        }

        private void txtDataNascimento_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
            {
                txtDataNascimento.Text = "  /  /";
            }
        }

        // Mostrar/ocultar senha
        private void btnMostrarSenha_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenha.UseSystemPasswordChar = false;
            txtConfirmarSenha.UseSystemPasswordChar = false;
        }

        private void btnMostrarSenha_MouseUp(object sender, MouseEventArgs e)
        {
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;
        }
    }
}