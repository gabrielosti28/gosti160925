using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormMeuPerfil : Form
    {
        private int _usuarioId;
        private bool _modoVisualizacao;

        // Construtor para ver o próprio perfil
        public FormMeuPerfil()
        {
            InitializeComponent();
            btnEnviarMensagem.Visible = false;

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            _usuarioId = AppManager.UsuarioLogado.UsuarioId;
            _modoVisualizacao = false;
            CarregarDadosPerfil();
        }

        // Construtor para visualizar perfil de outro usuário
        public FormMeuPerfil(int usuarioId)
        {
            InitializeComponent();

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            _usuarioId = usuarioId;
            _modoVisualizacao = (usuarioId != AppManager.UsuarioLogado.UsuarioId);

            if (_modoVisualizacao)
            {
                DesabilitarEdicao();
            }

            CarregarDadosPerfil();
        }

        private void DesabilitarEdicao()
        {
            // Desabilita botões de edição
            btnAlterarFoto.Visible = false;
            panelPersonalizacao.Visible = false;
            // Mostra botão de mensagem para outros perfis
            btnEnviarMensagem.Visible = true;
            // Ajusta título
            lblTitulo.Text = "👤 Perfil do Usuário";
        }

        private void CarregarDadosPerfil()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var usuario = db.Usuarios.Find(_usuarioId);

                    if (usuario == null)
                    {
                        MessageBox.Show("Usuário não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // FOTO DO PERFIL
                    CarregarFotoPerfil(usuario);

                    // NOME DO USUÁRIO
                    lblNomeUsuario.Text = usuario.NomeUsuario;

                    // BIO
                    if (string.IsNullOrWhiteSpace(usuario.Bio))
                    {
                        txtBio.Text = "📝 Este usuário ainda não adicionou uma biografia...";
                    }
                    else
                    {
                        txtBio.Text = usuario.Bio;
                    }

                    // ESTATÍSTICAS
                    CarregarEstatisticas();

                    // ÚLTIMOS 3 LIVROS
                    CarregarUltimosLivros();

                    // ÚLTIMA TIER LIST
                    CarregarUltimaTierList();

                    // APLICAR COR DO PERFIL
                    AplicarCorPerfil(usuario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar perfil: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CarregarFotoPerfil(Usuario usuario)
        {
            try
            {
                if (usuario.FotoPerfil != null && usuario.FotoPerfil.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(usuario.FotoPerfil))
                    {
                        pictureBoxPerfil.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxPerfil.BackColor = Color.LightGray;
                }
            }
            catch
            {
                pictureBoxPerfil.BackColor = Color.LightGray;
            }
        }

        private void CarregarEstatisticas()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var usuario = db.Usuarios.Find(_usuarioId);

                    // DATA DO ÚLTIMO LOGIN
                    if (usuario.UltimoLogin.HasValue)
                    {
                        lblDataLogin.Text = $"📅 Último login: {usuario.UltimoLogin.Value:dd/MM/yyyy HH:mm}";
                    }
                    else
                    {
                        lblDataLogin.Text = "📅 Último login: Não disponível";
                    }

                    // TOTAL DE LIVROS
                    int totalLivros = db.Livros.Count(l => l.UsuarioId == _usuarioId);
                    lblTotalLivros.Text = $"📚 Livros adicionados: {totalLivros}";

                    // TOTAL DE COMENTÁRIOS
                    int totalComentarios = db.Comentarios.Count(c => c.UsuarioId == _usuarioId);
                    lblTotalComentarios.Text = $"💬 Comentários: {totalComentarios}";

                    // TOTAL DE TIER LISTS
                    int totalTiers = db.Set<CategoriaTier>().Count(t => t.UsuarioId == _usuarioId);
                    lblTotalTierLists.Text = $"⭐ Tier Lists: {totalTiers}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar estatísticas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarUltimosLivros()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var ultimosLivros = db.Livros
                        .Where(l => l.UsuarioId == _usuarioId)
                        .OrderByDescending(l => l.DataAdicao)
                        .Take(3)
                        .ToList();

                    // LIVRO 1
                    if (ultimosLivros.Count > 0)
                    {
                        lblLivro1.Text = ultimosLivros[0].Titulo;
                        CarregarCapaLivro(picLivro1, ultimosLivros[0].Capa);
                    }
                    else
                    {
                        lblLivro1.Text = "Nenhum livro adicionado";
                    }

                    // LIVRO 2
                    if (ultimosLivros.Count > 1)
                    {
                        lblLivro2.Text = ultimosLivros[1].Titulo;
                        CarregarCapaLivro(picLivro2, ultimosLivros[1].Capa);
                    }
                    else
                    {
                        lblLivro2.Text = "Nenhum livro adicionado";
                    }

                    // LIVRO 3
                    if (ultimosLivros.Count > 2)
                    {
                        lblLivro3.Text = ultimosLivros[2].Titulo;
                        CarregarCapaLivro(picLivro3, ultimosLivros[2].Capa);
                    }
                    else
                    {
                        lblLivro3.Text = "Nenhum livro adicionado";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar últimos livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarCapaLivro(PictureBox pictureBox, byte[] capa)
        {
            try
            {
                if (capa != null && capa.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(capa))
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBox.BackColor = Color.LightGray;
                }
            }
            catch
            {
                pictureBox.BackColor = Color.LightGray;
            }
        }

        private void CarregarUltimaTierList()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var ultimaTier = db.Set<CategoriaTier>()
                        .Include(t => t.Livro1)
                        .Include(t => t.Livro2)
                        .Include(t => t.Livro3)
                        .Include(t => t.Livro4)
                        .Include(t => t.Livro5)
                        .Where(t => t.UsuarioId == _usuarioId)
                        .OrderByDescending(t => t.DataCriacao)
                        .FirstOrDefault();

                    // Limpa o painel antes de adicionar
                    panelTierContent.Controls.Clear();

                    if (ultimaTier != null)
                    {
                        // Atualiza o título com o nome da tier list
                        lblTituloUltimaTier.Text = $"⭐ Última Tier List: {ultimaTier.NomeTier}";

                        // Cria mini painéis para cada livro da tier
                        int x = 10;
                        AdicionarMiniLivro(panelTierContent, ultimaTier.Livro1, x, 10);
                        AdicionarMiniLivro(panelTierContent, ultimaTier.Livro2, x + 80, 10);
                        AdicionarMiniLivro(panelTierContent, ultimaTier.Livro3, x + 160, 10);
                        AdicionarMiniLivro(panelTierContent, ultimaTier.Livro4, x + 240, 10);
                        AdicionarMiniLivro(panelTierContent, ultimaTier.Livro5, x + 320, 10);
                    }
                    else
                    {
                        lblSemTier.Text = "Nenhuma tier list criada ainda";
                        panelTierContent.Controls.Add(lblSemTier);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tier list: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdicionarMiniLivro(Panel painelPai, Livro livro, int x, int y)
        {
            var picBox = new PictureBox
            {
                Location = new Point(x, y),
                Size = new Size(70, 90),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            if (livro != null && livro.Capa != null && livro.Capa.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(livro.Capa))
                    {
                        picBox.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    picBox.BackColor = Color.LightGray;
                }
            }

            painelPai.Controls.Add(picBox);
        }

        private void AplicarCorPerfil(Usuario usuario)
        {
            try
            {
                if (!string.IsNullOrEmpty(usuario.CorPerfil))
                {
                    string[] rgb = usuario.CorPerfil.Split(',');
                    if (rgb.Length == 3)
                    {
                        Color cor = Color.FromArgb(
                            int.Parse(rgb[0]),
                            int.Parse(rgb[1]),
                            int.Parse(rgb[2])
                        );

                        panelTopo.BackColor = cor;
                    }
                }
            }
            catch
            {
                // Mantém a cor padrão em caso de erro
            }
        }

        private void btnAlterarFoto_Click(object sender, EventArgs e)
        {
            // Só permite alterar se for o próprio perfil
            if (_modoVisualizacao)
                return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Selecionar Foto de Perfil";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image imagemOriginal = Image.FromFile(openFileDialog.FileName);
                        pictureBoxPerfil.Image = imagemOriginal;

                        // Salva no banco
                        using (MemoryStream ms = new MemoryStream())
                        {
                            imagemOriginal.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] fotoBytes = ms.ToArray();

                            using (var db = new ApplicationDbContext())
                            {
                                var usuario = db.Usuarios.Find(AppManager.UsuarioLogado.UsuarioId);
                                if (usuario != null)
                                {
                                    usuario.FotoPerfil = fotoBytes;
                                    db.SaveChanges();

                                    // Atualiza o usuário logado em memória
                                    AppManager.UsuarioLogado.FotoPerfil = fotoBytes;

                                    MessageBox.Show("Foto de perfil atualizada com sucesso!", "Sucesso",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao alterar foto: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AdicionarBotaoEditarBio()
        {
            // Cria botão para editar bio
            var btnEditarBio = new Button
            {
                Text = "✏️ Editar Bio",
                Size = new Size(100, 25),
                Location = new Point(txtBio.Right - 100, txtBio.Bottom + 5),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = !_modoVisualizacao // Só visível no próprio perfil
            };
            btnEditarBio.FlatAppearance.BorderSize = 0;
            btnEditarBio.Click += btnEditarBio_Click;

            panelCentral.Controls.Add(btnEditarBio);
        }

        // 2. ADICIONE este evento handler:
        private void btnEditarBio_Click(object sender, EventArgs e)
        {
            if (_modoVisualizacao)
                return;

            // Abre diálogo para editar biografia
            using (var formEditar = new Form())
            {
                formEditar.Text = "Editar Biografia";
                formEditar.Size = new Size(500, 300);
                formEditar.StartPosition = FormStartPosition.CenterParent;
                formEditar.FormBorderStyle = FormBorderStyle.FixedDialog;
                formEditar.MaximizeBox = false;
                formEditar.MinimizeBox = false;

                var lblTitulo = new Label
                {
                    Text = "✏️ Editar sua biografia",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(20, 20),
                    AutoSize = true
                };
                formEditar.Controls.Add(lblTitulo);

                var txtNovaBio = new TextBox
                {
                    Text = txtBio.Text == "📝 Este usuário ainda não adicionou uma biografia..." ? "" : txtBio.Text,
                    Location = new Point(20, 60),
                    Size = new Size(440, 120),
                    Multiline = true,
                    Font = new Font("Segoe UI", 10),
                    ScrollBars = ScrollBars.Vertical,
                    MaxLength = 500
                };
                formEditar.Controls.Add(txtNovaBio);

                var lblContador = new Label
                {
                    Text = $"{txtNovaBio.Text.Length}/500 caracteres",
                    Location = new Point(20, 185),
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 8),
                    AutoSize = true
                };
                formEditar.Controls.Add(lblContador);

                txtNovaBio.TextChanged += (s, ev) =>
                {
                    lblContador.Text = $"{txtNovaBio.Text.Length}/500 caracteres";
                    lblContador.ForeColor = txtNovaBio.Text.Length > 500 ? Color.Red : Color.Gray;
                };

                var btnSalvar = new Button
                {
                    Text = "💾 Salvar",
                    Location = new Point(250, 210),
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(60, 179, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btnSalvar.FlatAppearance.BorderSize = 0;
                btnSalvar.Click += (s, ev) =>
                {
                    if (txtNovaBio.Text.Length > 500)
                    {
                        MessageBox.Show("Biografia muito longa (máximo 500 caracteres).", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        using (var db = new ApplicationDbContext())
                        {
                            var usuario = db.Usuarios.Find(AppManager.UsuarioLogado.UsuarioId);
                            if (usuario != null)
                            {
                                usuario.Bio = string.IsNullOrWhiteSpace(txtNovaBio.Text) ? null : txtNovaBio.Text.Trim();
                                db.SaveChanges();

                                // Atualiza o usuário logado em memória
                                AppManager.UsuarioLogado.Bio = usuario.Bio;

                                // Atualiza a interface
                                txtBio.Text = string.IsNullOrWhiteSpace(usuario.Bio)
                                    ? "📝 Este usuário ainda não adicionou uma biografia..."
                                    : usuario.Bio;

                                MessageBox.Show("Biografia atualizada com sucesso!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                formEditar.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar biografia: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                formEditar.Controls.Add(btnSalvar);

                var btnCancelar = new Button
                {
                    Text = "❌ Cancelar",
                    Location = new Point(360, 210),
                    Size = new Size(100, 35),
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btnCancelar.FlatAppearance.BorderSize = 0;
                btnCancelar.Click += (s, ev) => formEditar.Close();
                formEditar.Controls.Add(btnCancelar);

                formEditar.ShowDialog();
            }
        }


        // BOTÕES DE CORES
        private void btnCorAzul_Click(object sender, EventArgs e)
        {
            if (!_modoVisualizacao)
                AlterarCorPerfil("70,130,180", Color.FromArgb(70, 130, 180));
        }

        private void btnCorVerde_Click(object sender, EventArgs e)
        {
            if (!_modoVisualizacao)
                AlterarCorPerfil("60,179,113", Color.FromArgb(60, 179, 113));
        }

        private void btnCorRoxo_Click(object sender, EventArgs e)
        {
            if (!_modoVisualizacao)
                AlterarCorPerfil("138,43,226", Color.FromArgb(138, 43, 226));
        }

        private void btnCorLaranja_Click(object sender, EventArgs e)
        {
            if (!_modoVisualizacao)
                AlterarCorPerfil("255,140,0", Color.FromArgb(255, 140, 0));
        }

        private void btnCorVermelho_Click(object sender, EventArgs e)
        {
            if (!_modoVisualizacao)
                AlterarCorPerfil("220,53,69", Color.FromArgb(220, 53, 69));
        }

        private void AlterarCorPerfil(string corRGB, Color cor)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var usuario = db.Usuarios.Find(AppManager.UsuarioLogado.UsuarioId);
                    if (usuario != null)
                    {
                        usuario.CorPerfil = corRGB;
                        db.SaveChanges();

                        // Atualiza o usuário logado em memória
                        AppManager.UsuarioLogado.CorPerfil = corRGB;

                        // Aplica a cor imediatamente
                        panelTopo.BackColor = cor;

                        MessageBox.Show("Cor do perfil alterada com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao alterar cor: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        // ADICIONAR este método no FormMeuPerfil.cs existente:

        private void btnEnviarMensagem_Click(object sender, EventArgs e)
        {
            if (_modoVisualizacao && _usuarioId != AppManager.UsuarioLogado.UsuarioId)
            {
                // Abre diálogo para enviar mensagem
                using (var formEnviar = new Form())
                {
                    formEnviar.Text = "Enviar Mensagem";
                    formEnviar.Size = new Size(500, 300);
                    formEnviar.StartPosition = FormStartPosition.CenterParent;
                    formEnviar.FormBorderStyle = FormBorderStyle.FixedDialog;
                    formEnviar.MaximizeBox = false;
                    formEnviar.MinimizeBox = false;

                    var lblTitulo = new Label
                    {
                        Text = $"📧 Enviar mensagem para {lblNomeUsuario.Text}",
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Location = new Point(20, 20),
                        AutoSize = true
                    };
                    formEnviar.Controls.Add(lblTitulo);

                    var txtMensagem = new TextBox
                    {
                        Location = new Point(20, 60),
                        Size = new Size(440, 120),
                        Multiline = true,
                        Font = new Font("Segoe UI", 10),
                        ScrollBars = ScrollBars.Vertical
                    };
                    formEnviar.Controls.Add(txtMensagem);

                    var btnEnviar = new Button
                    {
                        Text = "📤 Enviar",
                        Location = new Point(250, 200),
                        Size = new Size(100, 35),
                        BackColor = Color.FromArgb(60, 179, 113),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };
                    btnEnviar.FlatAppearance.BorderSize = 0;
                    btnEnviar.Click += (s, ev) =>
                    {
                        if (string.IsNullOrWhiteSpace(txtMensagem.Text))
                        {
                            MessageBox.Show("Digite uma mensagem.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (MensagemPrivadaManager.EnviarMensagem(_usuarioId, txtMensagem.Text))
                        {
                            MessageBox.Show("Mensagem enviada com sucesso!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formEnviar.DialogResult = DialogResult.OK;
                            formEnviar.Close();
                        }
                    };
                    formEnviar.Controls.Add(btnEnviar);

                    var btnCancelar = new Button
                    {
                        Text = "❌ Cancelar",
                        Location = new Point(360, 200),
                        Size = new Size(100, 35),
                        BackColor = Color.Gray,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };
                    btnCancelar.FlatAppearance.BorderSize = 0;
                    btnCancelar.Click += (s, ev) =>
                    {
                        formEnviar.DialogResult = DialogResult.Cancel;
                        formEnviar.Close();
                    };
                    formEnviar.Controls.Add(btnCancelar);

                    formEnviar.ShowDialog();
                }
            }
        }



        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTopo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalTierLists_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria botão para editar bio
            var btnEditarBio = new Button
            {
                Text = "✏️ Editar Bio",
                Size = new Size(100, 25),
                Location = new Point(txtBio.Right - 100, txtBio.Bottom + 5),
                BackColor = Color.FromArgb(70, 130, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = !_modoVisualizacao // Só visível no próprio perfil
            };
            btnEditarBio.FlatAppearance.BorderSize = 0;
            btnEditarBio.Click += btnEditarBio_Click;

            panelCentral.Controls.Add(btnEditarBio);
        }
    }
}