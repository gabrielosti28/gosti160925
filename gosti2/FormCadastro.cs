using System;
using System.Windows.Forms;

namespace gosti2
{
    public partial class FormCadastro : Form
    {
        public FormCadastro()
        {
            InitializeComponent();
            // Já configurado no Designer: txtSenha.PasswordChar = '*';
            // Já configurado no Designer: txtConfirmarSenha.PasswordChar = '*';
        }

        private void btnCadastrar2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var novoUsuario = new Usuario
            {
                Nome = txtNome.Text,
                Email = txtEmail.Text,
                Senha = txtSenha.Text,
                DataNascimento = txtDataNascimento.Text,
                Bio = txtBio.Text
            };

            if (UsuarioManager.CadastrarUsuario(novoUsuario))
            {
                MessageBox.Show("✅ Cadastro realizado com sucesso!\n\nAgora você pode fazer login em sua conta.",
                              "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSair2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("❌ Por favor, informe seu nome completo.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("❌ Por favor, informe um email válido.", "Email Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                MessageBox.Show("❌ A senha deve ter pelo menos 6 caracteres.", "Senha Fraca", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("❌ As senhas não coincidem. Por favor, digite novamente.", "Senhas Diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarSenha.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
            {
                MessageBox.Show("❌ Por favor, informe sua data de nascimento.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return false;
            }

            return true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Mantido para compatibilidade
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Mantido para compatibilidade
        }
    }
}