using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

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
            // Configurações básicas
            txtDataNascimento.Mask = "00/00/0000";
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;

            // Foco no primeiro campo
            txtNomeUsuario.Focus();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            RealizarCadastro();
        }

        private bool ValidarCampos()
        {
            // Validação direta e simples
            if (string.IsNullOrWhiteSpace(txtNomeUsuario.Text) || txtNomeUsuario.Text.Length < 3)
            {
                MessageBox.Show("Nome deve ter pelo menos 3 caracteres", "Aviso");
                txtNomeUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email inválido", "Aviso");
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                MessageBox.Show("Senha deve ter pelo menos 6 caracteres", "Aviso");
                txtSenha.Focus();
                return false;
            }

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("Senhas não coincidem", "Aviso");
                txtConfirmarSenha.Focus();
                return false;
            }

            // Validação de data
            if (!DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
            {
                MessageBox.Show("Data de nascimento inválida", "Aviso");
                txtDataNascimento.Focus();
                return false;
            }

            // Verificar se é maior de 13 anos
            int idade = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade))
                idade--;

            if (idade < 13)
            {
                MessageBox.Show("É necessário ter pelo menos 13 anos", "Aviso");
                txtDataNascimento.Focus();
                return false;
            }

            return true;
        }

        private void RealizarCadastro()
        {
            try
            {
                DateTime dataNascimento = DateTime.Parse(txtDataNascimento.Text);

                // CORREÇÃO: Usando txtNomeUsuario (nome correto do controle)
                if (AppManager.CadastrarUsuario(
                    txtNomeUsuario.Text.Trim(),
                    txtEmail.Text.Trim().ToLower(),
                    txtSenha.Text, // Senha em texto puro - o manager vai criptografar
                    dataNascimento))
                {
                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtDataNascimento_Enter(object sender, EventArgs e)
        {
            // Remove a máscara quando o campo recebe foco para facilitar a digitação
            if (txtDataNascimento.Text == "  /  /")
            {
                txtDataNascimento.Text = "";
            }
        }

        private void txtDataNascimento_Leave(object sender, EventArgs e)
        {
            // Restaura a máscara se estiver vazio
            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text))
            {
                txtDataNascimento.Text = "  /  /";
            }
        }

        private void btnMostrarSenha_MouseDown(object sender, MouseEventArgs e)
        {
            // Mostrar senha enquanto o botão está pressionado
            txtSenha.UseSystemPasswordChar = false;
            txtConfirmarSenha.UseSystemPasswordChar = false;
        }

        private void btnMostrarSenha_MouseUp(object sender, MouseEventArgs e)
        {
            // Ocultar senha quando soltar o botão
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;
        }

        
    }
}