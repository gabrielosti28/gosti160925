using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace gosti2
{
    public partial class FormLivroAberto : Form
    {
        private int _livroId;
        private Livro _livro;
        private int _usuarioLogadoId;

        public FormLivroAberto(int livroId)
        {
            InitializeComponent();
            _livroId = livroId;
            _usuarioLogadoId = UsuarioManager.UsuarioLogado?.UserId ?? 0;
            CarregarLivro();
            CarregarComentarios();
            CarregarLikesDislikes();
        }

        private void CarregarLivro()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    _livro = context.Livros
                        .Include(l => l.Usuario)
                        .FirstOrDefault(l => l.LivroId == _livroId);

                    if (_livro != null)
                    {
                        lblTitulo.Text = _livro.Titulo;
                        lblAutor.Text = $"Autor: {_livro.Autor}";
                        lblGenero.Text = $"Gênero: {_livro.CategoriaId}";
                        lblAdicionadoPor.Text = $"Adicionado por: {_livro.Usuario.Nome}";
                        txtDescricao.Text = _livro.Descricao;

                        // Carregar imagem ampliada
                        if (_livro.Capa != null && _livro.Capa.Length > 0)
                        {
                            using (var ms = new System.IO.MemoryStream(_livro.Capa))
                            {
                                pictureBoxCapa.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            pictureBoxCapa.Image = Properties.Resources.default_book_cover;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarComentarios()
        {
            try
            {
                flowLayoutPanelComentarios.Controls.Clear();

                using (var context = new ApplicationDbContext())
                {
                    var comentarios = context.Comentarios
                        .Include(c => c.Usuario)
                        .Where(c => c.LivroId == _livroId)
                        .OrderByDescending(c => c.DataComentario)
                        .ToList();

                    foreach (var comentario in comentarios)
                    {
                        AdicionarComentarioPanel(comentario);
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
                Height = 120,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackColor = Color.White
            };

            var lblUsuario = new Label
            {
                Text = comentario.Usuario.Nome,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
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
                Size = new Size(panel.Width - 20, 50),
                BackColor = Color.White
            };

            var panelLikes = new Panel
            {
                Location = new Point(10, 90),
                Size = new Size(200, 25)
            };

            var btnLike = new Button
            {
                Text = $"👍 {comentario.Likes}",
                Tag = comentario.ComentarioId,
                Size = new Size(60, 25),
                Location = new Point(0, 0),
                Font = new Font("Segoe UI", 8)
            };
            btnLike.Click += BtnLikeComentario_Click;

            var btnDislike = new Button
            {
                Text = $"👎 {comentario.Dislikes}",
                Tag = comentario.ComentarioId,
                Size = new Size(60, 25),
                Location = new Point(70, 0),
                Font = new Font("Segoe UI", 8)
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

                    // Verificar se o usuário já votou
                    var userVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (userVote != null)
                    {
                        btnLikeLivro.Enabled = false;
                        btnDislikeLivro.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar likes: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnComentar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNovoComentario.Text))
            {
                MessageBox.Show("Digite um comentário antes de enviar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = new Comentario
                    {
                        Texto = txtNovoComentario.Text,
                        LivroId = _livroId,
                        UsuarioId = _usuarioLogadoId,
                        DataComentario = DateTime.Now
                    };

                    context.Comentarios.Add(comentario);
                    context.SaveChanges();

                    txtNovoComentario.Clear();
                    CarregarComentarios();

                    MessageBox.Show("Comentário adicionado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLikeLivro_Click(object sender, EventArgs e)
        {
            RegistrarVotoLivro(true);
        }

        private void btnDislikeLivro_Click(object sender, EventArgs e)
        {
            RegistrarVotoLivro(false);
        }

        private void RegistrarVotoLivro(bool isLike)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Verifica se já votou
                    var existingVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (existingVote != null)
                    {
                        MessageBox.Show("Você já votou neste livro.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var vote = new LikeDislike
                    {
                        LivroId = _livroId,
                        UsuarioId = _usuarioLogadoId,
                        IsLike = isLike,
                        DataAcao = DateTime.Now
                    };

                    context.LikesDislikes.Add(vote);
                    context.SaveChanges();

                    btnLikeLivro.Enabled = false;
                    btnDislikeLivro.Enabled = false;
                    CarregarLikesDislikes();

                    MessageBox.Show($"Você deu {(isLike ? "like" : "dislike")} no livro!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLikeComentario_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int comentarioId = (int)button.Tag;
            RegistrarVotoComentario(comentarioId, true);
        }

        private void BtnDislikeComentario_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show($"Erro ao votar no comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}