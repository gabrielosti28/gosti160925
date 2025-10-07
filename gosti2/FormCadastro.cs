using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
            ConfigurarValidacoes();
        }

        private void ConfigurarValidacoes()
        {
            txtDataNascimento.Mask = "00/00/0000";
            txtNomeUsuario.MaxLength = 100;
            txtEmail.MaxLength = 100;
            txtSenha.MaxLength = 255;
            txtConfirmarSenha.MaxLength = 255;
            txtNomeUsuario.Focus();
        }

        private bool ValidarCampos()
        {
            return ValidarNomeUsuario() && ValidarEmail() && ValidarSenha() &&
                   ValidarConfirmacaoSenha() && ValidarDataNascimento();
        }

        private bool ValidarNomeUsuario()
        {
            string nome = txtNomeUsuario.Text.Trim();
            bool valido = !string.IsNullOrEmpty(nome) && nome.Length >= 3 &&
                         Regex.IsMatch(nome, @"^[a-zA-Z0-9_]+$");

            if (!valido) MostrarErro(txtNomeUsuario, "Nome inválido");
            else LimparErro(txtNomeUsuario);

            return valido;
        }

        private bool ValidarEmail()
        {
            string email = txtEmail.Text.Trim().ToLower();
            bool valido = !string.IsNullOrEmpty(email) &&
                         Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (!valido) MostrarErro(txtEmail, "Email inválido");
            else LimparErro(txtEmail);

            return valido;
        }

        private bool ValidarSenha()
        {
            bool valido = !string.IsNullOrEmpty(txtSenha.Text) && txtSenha.Text.Length >= 6;

            if (!valido) MostrarErro(txtSenha, "Mínimo 6 caracteres");
            else LimparErro(txtSenha);

            return valido;
        }

        private bool ValidarConfirmacaoSenha()
        {
            bool valido = txtSenha.Text == txtConfirmarSenha.Text;

            if (!valido) MostrarErro(txtConfirmarSenha, "Senhas não coincidem");
            else LimparErro(txtConfirmarSenha);

            return valido;
        }

        private bool ValidarDataNascimento()
        {
            string dataTexto = txtDataNascimento.Text;

            if (string.IsNullOrEmpty(dataTexto) || dataTexto.Replace("/", "").Length != 8)
            {
                MostrarErro(txtDataNascimento, "Data incompleta");
                return false;
            }

            if (DateTime.TryParseExact(dataTexto, "dd/MM/yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime dataNascimento))
            {
                int idade = DateTime.Today.Year - dataNascimento.Year;
                if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

                if (idade >= 13)
                {
                    LimparErro(txtDataNascimento);
                    return true;
                }
            }

            MostrarErro(txtDataNascimento, "Mínimo 13 anos");
            return false;
        }

        private void MostrarErro(Control controle, string mensagem)
        {
            errorProvider.SetError(controle, mensagem);
        }

        private void LimparErro(Control controle)
        {
            errorProvider.SetError(controle, "");
        }

        private string CriptografarSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha + "BookConnect2024");
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void btnCadastrar2_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                RealizarCadastro();
            }
        }

        private void RealizarCadastro()
        {
            try
            {
                var usuario = new Usuario
                {
                    NomeUsuario = txtNomeUsuario.Text.Trim(),
                    Email = txtEmail.Text.Trim().ToLower(),
                    Senha = CriptografarSenha(txtSenha.Text),
                    DataNascimento = DateTime.ParseExact(txtDataNascimento.Text, "dd/MM/yyyy", null),
                    Bio = string.IsNullOrWhiteSpace(txtBio.Text) ? null : txtBio.Text.Trim(),
                    DataCadastro = DateTime.Now,
                    Ativo = true
                };

                using (var context = new ApplicationDbContext())
                {
                    // Verifica se já existe
                    if (context.Usuarios.Any(u => u.Email == usuario.Email || u.NomeUsuario == usuario.NomeUsuario))
                    {
                        MessageBox.Show("Usuário ou email já cadastrado", "Aviso");
                        return;
                    }

                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                }

                MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro");
            }
        }

        private void btnSair2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}