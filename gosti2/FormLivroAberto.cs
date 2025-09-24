using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using gosti2.Models;
using gosti2.Data;

namespace gosti2
{
    public partial class FormLivroAberto : Form
    {
        private int _livroId;
        private Livro _livro;
        private int _usuarioLogadoId;

        public FormLivroAberto(int livroId, int usuarioLogadoId = 0)
        {
            InitializeComponent();
            _livroId = livroId;
            _usuarioLogadoId = usuarioLogadoId;

            // ✅ CONFIGURAÇÕES DE UI
            ConfigureUI();

            CarregarLivro();
            CarregarComentarios();
            CarregarLikesDislikes();
            VerificarVotoUsuario();
        }

        private void ConfigureUI()
        {
            // ✅ CONFIGURAÇÕES DE ESTILO
            flowLayoutPanelComentarios.AutoScroll = true;
            flowLayoutPanelComentarios.WrapContents = false;

            // ✅ CONFIGURAR BOTÕES
            btnLikeLivro.BackColor = Color.LightGray;
            btnDislikeLivro.BackColor = Color.LightGray;

            // ✅ EVENTO DE REDIMENSIONAMENTO
            this.Resize += (s, e) => AjustarTamanhoPainelComentarios();
        }

        private void AjustarTamanhoPainelComentarios()
        {
            foreach (Control control in flowLayoutPanelComentarios.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = flowLayoutPanelComentarios.ClientSize.Width - 25;
                }
            }
        }

        private void CarregarLivro()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // ✅ CORREÇÃO: Include correto e compatível
                    _livro = context.Livros
                        .Include(l => l.Usuario) // ✅ CORRETO: Lambda expression
                        .FirstOrDefault(l => l.LivroId == _livroId);

                    if (_livro != null)
                    {
                        lblTitulo.Text = _livro.Titulo;
                        lblAutor.Text = $"Autor: {_livro.Autor}";
                        lblGenero.Text = $"Gênero: {_livro.Genero}";

                        // ✅ CORREÇÃO: NomeUsuario (não Nome)
                        lblAdicionadoPor.Text = $"Adicionado por: {_livro.Usuario?.NomeUsuario ?? "Usuário desconhecido"}";

                        txtDescricao.Text = _livro.Descricao ?? "Sem descrição";

                        // ✅ CARREGAR CAPA COM TRATAMENTO SEGURO
                        CarregarCapaLivro(_livro.Capa);

                        // ✅ EXIBIR INFORMAÇÕES ADICIONAIS
                        ExibirInformacoesAdicionais(_livro);
                    }
                    else
                    {
                        MessageBox.Show("Livro não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarCapaLivro(byte[] capa)
        {
            try
            {
                if (capa != null && capa.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream(capa))
                    {
                        // ✅ EVITAR MEMORY LEAK: Dispose da imagem anterior
                        if (pictureBoxCapa.Image != null)
                        {
                            var oldImage = pictureBoxCapa.Image;
                            pictureBoxCapa.Image = null;
                            oldImage.Dispose();
                        }

                        pictureBoxCapa.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    // ✅ IMAGEM PADRÃO EMBUTIDA OU DE RECURSO
                    pictureBoxCapa.Image = Properties.Resources.default_book_cover
                                         ?? CreateDefaultBookCover();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar capa: {ex.Message}");
                pictureBoxCapa.Image = CreateDefaultBookCover();
            }
        }

        private Bitmap CreateDefaultBookCover()
        {
            // ✅ CAPA PADRÃO DINÂMICA
            var bmp = new Bitmap(200, 300);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.SteelBlue);
                using (var font = new Font("Arial", 16, FontStyle.Bold))
                {
                    g.DrawString("📚", font, Brushes.White, new PointF(70, 120));
                }
                g.DrawString("Sem Capa", new Font("Arial", 10), Brushes.White, new PointF(60, 180));
            }
            return bmp;
        }

        private void ExibirInformacoesAdicionais(Livro livro)
        {
            // ✅ EXIBIR INFORMAÇÕES EXTRAS SE DISPONÍVEIS
            var infoAdicional = new System.Text.StringBuilder();

            if (livro.AnoPublicacao.HasValue)
                infoAdicional.AppendLine($"Ano: {livro.AnoPublicacao}");

            if (!string.IsNullOrEmpty(livro.ISBN))
                infoAdicional.AppendLine($"ISBN: {livro.ISBN}");

            if (!string.IsNullOrEmpty(livro.Editora))
                infoAdicional.AppendLine($"Editora: {livro.Editora}");

            if (livro.Paginas.HasValue)
                infoAdicional.AppendLine($"Páginas: {livro.Paginas}");

            if (infoAdicional.Length > 0)
            {
                lblInfoAdicional.Text = infoAdicional.ToString();
                lblInfoAdicional.Visible = true;
            }
        }

        private void CarregarComentarios()
        {
            try
            {
                flowLayoutPanelComentarios.Controls.Clear();

                using (var context = new ApplicationDbContext())
                {
                    // ✅ CORREÇÃO: Carregamento eficiente com Includes
                    var comentarios = context.Comentarios
                        .Include(c => c.Usuario) // ✅ CORRETO
                        .Where(c => c.LivroId == _livroId)
                        .OrderByDescending(c => c.DataComentario)
                        .ToList();

                    if (comentarios.Any())
                    {
                        foreach (var comentario in comentarios)
                        {
                            AdicionarComentarioPanel(comentario);
                        }
                    }
                    else
                    {
                        // ✅ MENSAGEM QUANDO NÃO HÁ COMENTÁRIOS
                        var lblSemComentarios = new Label
                        {
                            Text = "📝 Seja o primeiro a comentar sobre este livro!",
                            Font = new Font("Segoe UI", 10, FontStyle.Italic),
                            ForeColor = Color.Gray,
                            AutoSize = true,
                            Margin = new Padding(10)
                        };
                        flowLayoutPanelComentarios.Controls.Add(lblSemComentarios);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar comentários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdicionarComentarioPanel(Comentario comentario)
        {
            var panel = new Panel
            {
                Width = flowLayoutPanelComentarios.Width - 25,
                Height = 140,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // ✅ CORREÇÃO: NomeUsuario (não Nome)
            var lblUsuario = new Label
            {
                Text = comentario.Usuario?.NomeUsuario ?? "Usuário desconhecido",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = Color.SteelBlue
            };

            var lblData = new Label
            {
                Text = comentario.DataComentario.ToString("dd/MM/yyyy HH:mm"),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(panel.Width - 150, 10),
                AutoSize = true
            };

            var txtComentario = new TextBox
            {
                Text = comentario.Texto,
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Location = new Point(10, 35),
                Size = new Size(panel.Width - 30, 60),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9),
                ScrollBars = ScrollBars.Vertical
            };

            var panelLikes = new Panel
            {
                Location = new Point(10, 100),
                Size = new Size(200, 30)
            };

            var btnLike = new Button
            {
                Text = $"👍 {comentario.Likes}",
                Tag = comentario.ComentarioId,
                Size = new Size(70, 25),
                Location = new Point(0, 0),
                Font = new Font("Segoe UI", 8),
                BackColor = comentario.Likes > 0 ? Color.LightGreen : Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLike.Click += BtnLikeComentario_Click;

            var btnDislike = new Button
            {
                Text = $"👎 {comentario.Dislikes}",
                Tag = comentario.ComentarioId,
                Size = new Size(70, 25),
                Location = new Point(80, 0),
                Font = new Font("Segoe UI", 8),
                BackColor = comentario.Dislikes > 0 ? Color.LightCoral : Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDislike.Click += BtnDislikeComentario_Click;

            panelLikes.Controls.Add(btnLike);
            panelLikes.Controls.Add(btnDislike);

            panel.Controls.Add(lblUsuario);
            panel.Controls.Add(lblData);
            panel.Controls.Add(txtComentario);
            panel.Controls.Add(panelLikes);

            flowLayoutPanelComentarios.Controls.Add(panel);
        }

        private void CarregarLikesDislikes()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var likes = context.LikesDislikes.Count(ld => ld.LivroId == _livroId && ld.IsLike);
                    var dislikes = context.LikesDislikes.Count(ld => ld.LivroId == _livroId && !ld.IsLike);

                    lblLikes.Text = $"👍 {likes}";
                    lblDislikes.Text = $"👎 {dislikes}";

                    // ✅ EXIBIR PORCENTAGEM SE HOUVER VOTOS
                    var total = likes + dislikes;
                    if (total > 0)
                    {
                        var percentual = (likes * 100) / total;
                        lblAprovacao.Text = $"{percentual}% de aprovação";
                        lblAprovacao.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar likes: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarVotoUsuario()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var userVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (userVote != null)
                    {
                        btnLikeLivro.Enabled = false;
                        btnDislikeLivro.Enabled = false;

                        if (userVote.IsLike)
                        {
                            btnLikeLivro.BackColor = Color.ForestGreen;
                            btnLikeLivro.ForeColor = Color.White;
                            btnLikeLivro.Text = "👍 Você curtiu";
                        }
                        else
                        {
                            btnDislikeLivro.BackColor = Color.IndianRed;
                            btnDislikeLivro.ForeColor = Color.White;
                            btnDislikeLivro.Text = "👎 Você não curtiu";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnComentar_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para comentar.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNovoComentario.Text))
            {
                MessageBox.Show("📝 Digite um comentário antes de enviar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNovoComentario.Focus();
                return;
            }

            if (txtNovoComentario.Text.Length > 2000)
            {
                MessageBox.Show("❌ Comentário muito longo. Máximo 2000 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = new Comentario
                    {
                        Texto = txtNovoComentario.Text.Trim(),
                        LivroId = _livroId,
                        UsuarioId = _usuarioLogadoId,
                        DataComentario = DateTime.Now,
                        Likes = 0,
                        Dislikes = 0
                    };

                    context.Comentarios.Add(comentario);
                    context.SaveChanges();

                    txtNovoComentario.Clear();
                    CarregarComentarios();

                    MessageBox.Show("✅ Comentário adicionado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao adicionar comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLikeLivro_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para curtir.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RegistrarVotoLivro(true);
        }

        private void btnDislikeLivro_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para não curtir.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RegistrarVotoLivro(false);
        }

        private void RegistrarVotoLivro(bool isLike)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (existingVote != null)
                    {
                        existingVote.IsLike = isLike;
                        existingVote.DataAcao = DateTime.Now;
                    }
                    else
                    {
                        var vote = new LikeDislike
                        {
                            LivroId = _livroId,
                            UsuarioId = _usuarioLogadoId,
                            IsLike = isLike,
                            DataAcao = DateTime.Now
                        };
                        context.LikesDislikes.Add(vote);
                    }

                    context.SaveChanges();
                    CarregarLikesDislikes();
                    VerificarVotoUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao registrar voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLikeComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para curtir comentários.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var button = (Button)sender;
            int comentarioId = (int)button.Tag;
            RegistrarVotoComentario(comentarioId, true);
        }

        private void BtnDislikeComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para não curtir comentários.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var button = (Button)sender;
            int comentarioId = (int)button.Tag;
            RegistrarVotoComentario(comentarioId, false);
        }

        private void RegistrarVotoComentario(int comentarioId, bool isLike)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = context.Comentarios.Find(comentarioId);
                    if (comentario != null)
                    {
                        if (isLike)
                            comentario.Likes++;
                        else
                            comentario.Dislikes++;

                        context.SaveChanges();
                        CarregarComentarios();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao votar no comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNovoComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && ModifierKeys != Keys.Shift)
            {
                if (!string.IsNullOrWhiteSpace(txtNovoComentario.Text))
                {
                    btnComentar.PerformClick();
                }
                e.Handled = true;
            }
        }

        private void FormLivroAberto_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ✅ LIMPEZA SEGURA DE RECURSOS
            if (pictureBoxCapa.Image != null)
            {
                var image = pictureBoxCapa.Image;
                pictureBoxCapa.Image = null;
                image.Dispose();
            }
        }

        // ✅ NOVO: CONTADOR DE CARACTERES
        private void txtNovoComentario_TextChanged(object sender, EventArgs e)
        {
            int count = txtNovoComentario.Text.Length;
            int max = 2000;
            lblContadorCaracteres.Text = $"{count}/{max}";

            if (count > max)
            {
                lblContadorCaracteres.ForeColor = Color.Red;
            }
            else if (count > max * 0.8)
            {
                lblContadorCaracteres.ForeColor = Color.Orange;
            }
            else
            {
                lblContadorCaracteres.ForeColor = Color.Gray;
            }
        }
    }
}