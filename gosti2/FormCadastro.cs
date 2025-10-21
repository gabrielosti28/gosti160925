using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormCadastro : Form
    {
        private byte[] fotoPerfilBytes = null;

        public FormCadastro()
        {
            InitializeComponent();
            ConfigurarInterface();
        }

        private void ConfigurarInterface()
        {
            txtDataNascimento.Mask = "00/00/0000";
            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;
            txtNomeUsuario.Focus();

            // Imagem padrão (cinza)
            pictureBoxFotoPerfil.BackColor = Color.LightGray;
        }

        private void btnSelecionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Selecionar Foto de Perfil";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Carrega a imagem
                        Image imagemOriginal = Image.FromFile(openFileDialog.FileName);

                        // Exibe no PictureBox
                        pictureBoxFotoPerfil.Image = imagemOriginal;

                        // Converte para bytes para salvar no banco
                        using (MemoryStream ms = new MemoryStream())
                        {
                            imagemOriginal.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            fotoPerfilBytes = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao carregar imagem: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("As senhas não coincidem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarSenha.Focus();
                return;
            }

            if (!DateTime.TryParse(txtDataNascimento.Text, out DateTime dataNascimento))
            {
                MessageBox.Show("Data de nascimento inválida.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            btnCadastrar.Enabled = false;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Verificar se email já existe
                    if (db.Usuarios.Any(u => u.Email == txtEmail.Text.Trim().ToLower()))
                    {
                        MessageBox.Show("Este email já está cadastrado.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Verificar se nome de usuário já existe
                    if (db.Usuarios.Any(u => u.NomeUsuario == txtNomeUsuario.Text.Trim()))
                    {
                        MessageBox.Show("Este nome de usuário já existe.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validar idade (mínimo 13 anos)
                    int idade = DateTime.Today.Year - dataNascimento.Year;
                    if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

                    if (idade < 13)
                    {
                        MessageBox.Show("É necessário ter pelo menos 13 anos para se cadastrar.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Criar novo usuário COM FOTO
                    var usuario = new Usuario
                    {
                        NomeUsuario = txtNomeUsuario.Text.Trim(),
                        Email = txtEmail.Text.ToLower().Trim(),
                        Senha = CriptografarSenha(txtSenha.Text),
                        DataNascimento = dataNascimento,
                        Bio = txtBio.Text.Trim(),
                        FotoPerfil = fotoPerfilBytes, // ADICIONA A FOTO
                        DataCadastro = DateTime.Now,
                        Ativo = true
                    };

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();

                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar usuário: {ex.Message}", "Erro",
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
            if (string.IsNullOrWhiteSpace(txtNomeUsuario.Text) || txtNomeUsuario.Text.Length < 3)
            {
                MessageBox.Show("Nome de usuário deve ter pelo menos 3 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email inválido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text) || txtSenha.Text.Length < 6)
            {
                MessageBox.Show("Senha deve ter pelo menos 6 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDataNascimento.Text) || txtDataNascimento.Text == "  /  /")
            {
                MessageBox.Show("Data de nascimento é obrigatória.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataNascimento.Focus();
                return false;
            }

            return true;
        }

        private string CriptografarSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(senha + "BookConnect2024");
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
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
    }
}