using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;
using System.Reflection; // Para MethodBase.GetCurrentMethod() se for usar

namespace gosti2
{
    public partial class FormCadastro : Form
    {
        private BackgroundWorker validacaoWorker;

        public FormCadastro()
        {
            InitializeComponent();
            ConfigurarValidacoes();
            ConfigurarBackgroundWorker();
        }

        private void ConfigurarValidacoes()
        {
            // Configuração da máscara de data
            txtDataNascimento.Mask = "00/00/0000";
            //txtDataNascimento.PlaceholderText = "dd/mm/aaaa";
            txtDataNascimento.ValidatingType = typeof(DateTime);

            // Configuração de limites de caracteres
            txtNomeUsuario.MaxLength = 100;
            txtEmail.MaxLength = 100;
            txtBio.MaxLength = 500;
            txtSenha.MaxLength = 255;
            txtConfirmarSenha.MaxLength = 255;

            // ToolTips para melhor UX
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtNomeUsuario, "3-100 caracteres. Letras, números e underline permitidos.");
            toolTip.SetToolTip(txtSenha, "Mínimo 6 caracteres. Use letras, números e caracteres especiais.");
            toolTip.SetToolTip(txtDataNascimento, "Você deve ter pelo menos 13 anos.");

            // Foco inicial
            txtNomeUsuario.Focus();
        }

        private void ConfigurarBackgroundWorker()
        {
            validacaoWorker = new BackgroundWorker();
            validacaoWorker.DoWork += ValidacaoWorker_DoWork;
            validacaoWorker.RunWorkerCompleted += ValidacaoWorker_RunWorkerCompleted;
        }

        #region Validações Síncronas e Assíncronas

        private bool ValidarCampos()
        {
            // Validação síncrona - campos básicos
            if (!ValidarNomeUsuario() || !ValidarEmail() || !ValidarSenha() ||
                !ValidarConfirmacaoSenha() || !ValidarDataNascimento())
            {
                return false;
            }

            // Validação assíncrona - verificação no banco
            if (validacaoWorker.IsBusy)
            {
                MessageBox.Show("Validação em andamento. Aguarde...", "Aguarde",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var dadosValidacao = new DadosValidacao
            {
                NomeUsuario = txtNomeUsuario.Text.Trim(),
                Email = txtEmail.Text.Trim().ToLower()
            };

            btnCadastrar2.Enabled = false;
            btnCadastrar2.Text = "Validando...";
            validacaoWorker.RunWorkerAsync(dadosValidacao);

            return false; // Retorna false temporariamente até a validação assíncrona completar
        }

        // Classe auxiliar para evitar problemas com dynamic
        private class DadosValidacao
        {
            public string NomeUsuario { get; set; }
            public string Email { get; set; }
        }

        private bool ValidarNomeUsuario()
        {
            var nomeUsuario = txtNomeUsuario.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeUsuario) || nomeUsuario.Length < 3)
            {
                MostrarErro(txtNomeUsuario, "Nome de usuário deve ter pelo menos 3 caracteres.");
                return false;
            }

            if (!Regex.IsMatch(nomeUsuario, @"^[a-zA-Z0-9_]+$"))
            {
                MostrarErro(txtNomeUsuario, "Use apenas letras, números e underline (_).");
                return false;
            }

            if (nomeUsuario.Length > 100)
            {
                MostrarErro(txtNomeUsuario, "Nome de usuário muito longo (máx. 100 caracteres).");
                return false;
            }

            LimparErro(txtNomeUsuario);
            return true;
        }

        private bool ValidarEmail()
        {
            var email = txtEmail.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(email))
            {
                MostrarErro(txtEmail, "Email é obrigatório.");
                return false;
            }

            // Regex mais robusta para validação de email
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailRegex))
            {
                MostrarErro(txtEmail, "Formato de email inválido.");
                return false;
            }

            LimparErro(txtEmail);
            return true;
        }

        private bool ValidarSenha()
        {
            var senha = txtSenha.Text;

            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            {
                MostrarErro(txtSenha, "Senha deve ter pelo menos 6 caracteres.");
                return false;
            }

            // Validação de força da senha
            var forcaSenha = CalcularForcaSenha(senha);
            if (forcaSenha < 2)
            {
                MostrarErro(txtSenha, "Senha muito fraca. Use letras maiúsculas, minúsculas, números e símbolos.");
                return false;
            }

            AtualizarIndicadorForcaSenha(forcaSenha);
            LimparErro(txtSenha);
            return true;
        }

        private bool ValidarConfirmacaoSenha()
        {
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MostrarErro(txtConfirmarSenha, "As senhas não coincidem.");
                return false;
            }

            LimparErro(txtConfirmarSenha);
            return true;
        }

        private bool ValidarDataNascimento()
        {
            var dataTexto = txtDataNascimento.Text;

            if (string.IsNullOrWhiteSpace(dataTexto) || dataTexto.Replace("/", "").Length != 8)
            {
                MostrarErro(txtDataNascimento, "Data de nascimento incompleta.");
                return false;
            }

            if (!DateTime.TryParseExact(dataTexto, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime dataNascimento))
            {
                MostrarErro(txtDataNascimento, "Data de nascimento inválida.");
                return false;
            }

            var idade = CalcularIdade(dataNascimento);
            if (idade < 13)
            {
                MostrarErro(txtDataNascimento, "Você deve ter pelo menos 13 anos.");
                return false;
            }

            if (idade > 120)
            {
                MostrarErro(txtDataNascimento, "Data de nascimento inválida.");
                return false;
            }

            LimparErro(txtDataNascimento);
            return true;
        }

        #endregion

        #region Métodos Auxiliares

        private void MostrarErro(Control controle, string mensagem)
        {
            errorProvider.SetError(controle, mensagem);
            controle.Focus();
        }

        private void LimparErro(Control controle)
        {
            errorProvider.SetError(controle, string.Empty);
        }

        private int CalcularForcaSenha(string senha)
        {
            int forca = 0;

            if (senha.Length >= 8) forca++;
            if (Regex.IsMatch(senha, @"[a-z]")) forca++;
            if (Regex.IsMatch(senha, @"[A-Z]")) forca++;
            if (Regex.IsMatch(senha, @"[0-9]")) forca++;
            if (Regex.IsMatch(senha, @"[^a-zA-Z0-9]")) forca++;

            return forca;
        }

        private void AtualizarIndicadorForcaSenha(int forca)
        {
            // Implementação visual da força da senha
            if (panelForcaSenha != null && lblForcaSenha != null)
            {
                Color cor;
                string texto;

                switch (forca)
                {
                    case 0:
                    case 1:
                        cor = Color.Red;
                        texto = "Muito Fraca";
                        break;
                    case 2:
                        cor = Color.Orange;
                        texto = "Fraca";
                        break;
                    case 3:
                        cor = Color.Yellow;
                        texto = "Média";
                        break;
                    case 4:
                        cor = Color.LightGreen;
                        texto = "Forte";
                        break;
                    case 5:
                        cor = Color.Green;
                        texto = "Muito Forte";
                        break;
                    default:
                        cor = Color.Gray;
                        texto = "Força da senha";
                        break;
                }

                panelForcaSenha.BackColor = cor;
                lblForcaSenha.Text = texto;
                lblForcaSenha.ForeColor = forca < 3 ? Color.White : Color.Black;
            }
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        private string CriptografarSenha(string senha)
        {
            // Implementação alternativa usando SHA256 (sem dependência externa)
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha + "BookConnect_Salt_2024");
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerificarSenha(string senha, string hash)
        {
            // Para verificar a senha, criptografa a senha fornecida e compara com o hash
            var hashFornecido = CriptografarSenha(senha);
            return hashFornecido == hash;
        }

        #endregion

        #region Event Handlers

        private void btnCadastrar2_Click(object sender, EventArgs e)
        {
            // A validação completa é feita no método ValidarCampos
            // que chama a validação assíncrona se necessário
            ValidarCampos();
        }

        private void ValidacaoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var dados = e.Argument as DadosValidacao;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Corrigido: usando dados tipados em vez de dynamic
                    var emailExiste = context.Usuarios.Any(u => u.Email == dados.Email);
                    var usuarioExiste = context.Usuarios.Any(u => u.NomeUsuario == dados.NomeUsuario);

                    e.Result = new ResultadoValidacao
                    {
                        EmailExiste = emailExiste,
                        UsuarioExiste = usuarioExiste
                    };
                }
            }
            catch (Exception ex)
            {
                // ✅ ADICIONE ESTE LOG DE ERRO AQUI:
                var dbManager = new DatabaseManager();
                dbManager.AdicionarLog("ERROR", ex.Message,
                    null, "FormCadastro", "ValidacaoWorker_DoWork",
                    ex.StackTrace, ex.GetType().Name, ex.InnerException?.Message);

                e.Result = new ResultadoValidacao { Error = ex.Message };
            }
        }

        // Classe para resultado da validação
        private class ResultadoValidacao
        {
            public bool EmailExiste { get; set; }
            public bool UsuarioExiste { get; set; }
            public string Error { get; set; }
        }

        private void ValidacaoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCadastrar2.Enabled = true;
            btnCadastrar2.Text = "Cadastrar";

            if (e.Error != null)
            {
                MessageBox.Show($"Erro na validação: {e.Error.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultado = e.Result as ResultadoValidacao;

            if (!string.IsNullOrEmpty(resultado.Error))
            {
                MessageBox.Show($"Erro no banco de dados: {resultado.Error}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.EmailExiste)
            {
                MostrarErro(txtEmail, "Este email já está cadastrado.");
                return;
            }

            if (resultado.UsuarioExiste)
            {
                MostrarErro(txtNomeUsuario, "Este nome de usuário já está em uso.");
                return;
            }

            // Todas as validações passaram - proceder com o cadastro
            RealizarCadastro();
        }

        private void RealizarCadastro()
        {
            try
            {
                var novoUsuario = new Usuario
                {
                    NomeUsuario = txtNomeUsuario.Text.Trim(),
                    Email = txtEmail.Text.Trim().ToLower(),
                    Senha = CriptografarSenha(txtSenha.Text),
                    DataNascimento = DateTime.ParseExact(txtDataNascimento.Text, "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Bio = string.IsNullOrWhiteSpace(txtBio.Text) ? null : txtBio.Text.Trim(),
                    DataCadastro = DateTime.Now,
                    UltimoLogin = null,
                    Ativo = true,
                    Website = null,
                    Localizacao = null,
                    FotoPerfil = null
                };

                using (var context = new ApplicationDbContext())
                {
                    context.Usuarios.Add(novoUsuario);
                    context.SaveChanges();
                }

                // ✅ ADICIONE ESTE LOG AQUI (ANTES do MessageBox):
                var dbManager = new DatabaseManager();
                dbManager.AdicionarLog("INFO", $"Novo usuário cadastrado: {novoUsuario.NomeUsuario}",
                    novoUsuario.UsuarioId, "FormCadastro", "RealizarCadastro");

                MessageBox.Show("Cadastro realizado com sucesso!\n\nAgora você pode fazer login em sua conta.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // ✅ ADICIONE ESTE LOG DE ERRO AQUI:
                var dbManager = new DatabaseManager();
                dbManager.AdicionarLog("ERROR", ex.Message,
                    null, "FormCadastro", "RealizarCadastro",
                    ex.StackTrace, ex.GetType().Name, ex.InnerException?.Message);

                MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnCadastrar2.Enabled = true;
                btnCadastrar2.Text = "Cadastrar";
            }
        }

        private void btnSair2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void txtDataNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }

        private void txtNomeUsuario_TextChanged(object sender, EventArgs e)
        {
            // Validação em tempo real
            if (txtNomeUsuario.Text.Length > 0)
            {
                ValidarNomeUsuario();
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.Length > 0)
            {
                ValidarEmail();
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            if (txtSenha.Text.Length > 0)
            {
                ValidarSenha();
            }
        }

        private void txtConfirmarSenha_TextChanged(object sender, EventArgs e)
        {
            if (txtConfirmarSenha.Text.Length > 0)
            {
                ValidarConfirmacaoSenha();
            }
        }

        private void txtDataNascimento_TextChanged(object sender, EventArgs e)
        {
            if (txtDataNascimento.Text.Replace("/", "").Length == 8)
            {
                ValidarDataNascimento();
            }
        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
            txtConfirmarSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
        }

        //private void txtBio_TextChanged(object sender, EventArgs e)
        //{
        //    // Contador de caracteres para a bio
        //    if (lblContadorBio != null)
        //    {
        //        int caracteresRestantes = 500 - txtBio.Text.Length;
        //        lblContadorBio.Text = $"{caracteresRestantes} caracteres restantes";
        //        lblContadorBio.ForeColor = caracteresRestantes < 50 ? Color.Red : Color.Gray;
        //    }
        //}

        #endregion
    }
}