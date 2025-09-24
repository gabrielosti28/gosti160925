using System;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;

namespace gosti2
{
    public partial class FormCadastro : Form
    {
        public FormCadastro()
        {
            InitializeComponent();

            // ✅ CONFIGURAÇÕES ADICIONAIS
            txtDataNascimento.Mask = "00/00/0000"; // Máscara para data
            txtDataNascimento.PlaceholderText = "dd/mm/aaaa";

            // ✅ FOCO NO PRIMEIRO CAMPO
            txtNomeUsuario.Focus();
        }

        private void btnCadastrar2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;

                // ✅ USUARIO COMPATÍVEL COM SUA CLASSE ATUAL
                var novoUsuario = new Usuario
                {
                    NomeUsuario = txtNomeUsuario.Text.Trim(), // ✅ CORRETO: NomeUsuario
                    Email = txtEmail.Text.Trim().ToLower(),   // ✅ Email em minúsculo
                    Senha = CriptografarSenha(txtSenha.Text), // ✅ Senha criptografada
                    DataNascimento = ConverterData(txtDataNascimento.Text), // ✅ DateTime
                    Bio = string.IsNullOrWhiteSpace(txtBio.Text) ? null : txtBio.Text.Trim(),
                    DataCadastro = DateTime.Now,
                    Ativo = true
                };

                // ✅ CADASTRO USANDO Entity Framework (compatível com seu projeto)
                if (CadastrarUsuario(novoUsuario))
                {
                    MessageBox.Show("✅ Cadastro realizado com sucesso!\n\nAgora você pode fazer login em sua conta.",
                                  "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao cadastrar: {ex.Message}", "Erro",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODO DE CADASTRO COMPATÍVEL COM SEU PROJETO
        private bool CadastrarUsuario(Usuario usuario)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // ✅ VERIFICAR SE EMAIL JÁ EXISTE
                    if (context.Usuarios.Any(u => u.Email == usuario.Email))
                    {
                        MessageBox.Show("❌ Este email já está cadastrado. Use outro email ou recupere sua senha.",
                                      "Email Existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // ✅ VERIFICAR SE NOME DE USUÁRIO JÁ EXISTE
                    if (context.Usuarios.Any(u => u.NomeUsuario == usuario.NomeUsuario))
                    {
                        MessageBox.Show("❌ Este nome de usuário já está em uso. Escolha outro.",
                                      "Nome em Uso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no banco de dados: {ex.Message}");
            }
        }

        // ✅ VALIDAÇÃO ROBUSTA E COMPLETA
        private bool ValidarCampos()
        {
            // ✅ NOME DE USUÁRIO
            if (string.IsNullOrWhiteSpace(txtNomeUsuario.Text) || txtNomeUsuario.Text.Length < 3)
            {
                MessageBox.Show("❌ Nome de usuário deve ter pelo menos 3 caracteres.",
                              "Nome Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeUsuario.Focus();
                return false;
            }

            // ✅ EMAIL
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("❌ Por favor, informe um email válido.",
                              "Email Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // ✅ SENHA
            if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                MessageBox.Show("❌ A senha deve ter pelo menos 6 caracteres.",
                              "Senha Fraca", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }

            // ✅ CONFIRMAÇÃO DE SENHA
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("❌ As senhas não coincidem. Por favor, digite novamente.",
                              "Senhas Diferentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarSenha.Clear();
                txtConfirmarSenha.Focus();
                return false;
            }

            // ✅ DATA DE NASCIMENTO
            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text) || !ValidarDataNascimento(txtDataNascimento.Text))
            {
                MessageBox.Show("❌ Data de nascimento inválida ou formato incorreto (dd/mm/aaaa).",
                              "Data Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return false;
            }

            // ✅ IDADE MÍNIMA (13 anos)
            var dataNascimento = ConverterData(txtDataNascimento.Text);
            if (CalcularIdade(dataNascimento) < 13)
            {
                MessageBox.Show("❌ Você deve ter pelo menos 13 anos para se cadastrar.",
                              "Idade Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return false;
            }

            return true;
        }

        // ✅ MÉTODOS AUXILIARES DE VALIDAÇÃO
        private bool ValidarEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidarDataNascimento(string data)
        {
            try
            {
                DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private DateTime ConverterData(string data)
        {
            return DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        // ✅ CRIPTOGRAFIA BÁSICA DE SENHA (MELHORAR FUTURAMENTE)
        private string CriptografarSenha(string senha)
        {
            // ✅ IMPLEMENTAÇÃO BÁSICA - MELHORAR COM BCrypt/ASP.NET Identity NO FUTURO
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha + "gosti2_salt"); // Salt básico
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void btnSair2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ✅ MELHORIAS DE USABILIDADE
        private void txtDataNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números e barra
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // ✅ VOLTAR PARA LOGIN
            this.DialogResult = DialogResult.Retry; // Ou DialogResult.Cancel
            this.Close();
        }

        // ✅ EVENTOS PARA MELHORAR UX
        private void txtNomeUsuario_TextChanged(object sender, EventArgs e)
        {
            // Limitar tamanho do nome de usuário
            if (txtNomeUsuario.Text.Length > 50)
            {
                txtNomeUsuario.Text = txtNomeUsuario.Text.Substring(0, 50);
                txtNomeUsuario.SelectionStart = 50;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            // Auto-completar domínio comum
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !txtEmail.Text.Contains("@") &&
                !txtEmail.Text.EndsWith(".com"))
            {
                txtEmail.Text += "@gmail.com";
            }
        }
    }
}