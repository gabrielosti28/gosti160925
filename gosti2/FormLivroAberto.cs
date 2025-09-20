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

        public FormLivroAberto(int livroId)
        {
            InitializeComponent();
            _livroId = livroId;
            _usuarioLogadoId = UsuarioManager.UsuarioLogado?.UserId ?? 0;

            // Configuração segura do ícone
            try
            {
            }
            catch
            {
                // Se não houver ícone, continua sem
            }

            CarregarLivro();
            CarregarComentarios();
            CarregarLikesDislikes();
            VerificarVotoUsuario();
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
                        lblGenero.Text = $"Gênero: {_livro.Genero}";
                        lblAdicionadoPor.Text = $"Adicionado por: {_livro.Usuario.Nome}";
                        txtDescricao.Text = _livro.Descricao;

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
                Size = new Size(panel.Width - 30, 60),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9)
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
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            btnLike.Click += BtnLikeComentario_Click;

            var btnDislike = new Button
            {
                Text = $"👎 {comentario.Dislikes}",
                Tag = comentario.ComentarioId,
                Size = new Size(70, 25),
                Location = new Point(80, 0),
                Font = new Font("Segoe UI", 8),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
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

                        // Mudar cor para indicar que já votou
                        if (userVote.IsLike)
                        {
                            btnLikeLivro.BackColor = Color.ForestGreen;
                            btnLikeLivro.ForeColor = Color.White;
                        }
                        else
                        {
                            btnDislikeLivro.BackColor = Color.IndianRed;
                            btnDislikeLivro.ForeColor = Color.White;
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
                        Texto = txtNovoComentario.Text.Trim(),
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

        private void txtNovoComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrWhiteSpace(txtNovoComentario.Text))
            {
                btnComentar.PerformClick();
                e.Handled = true;
            }
        }
        // No método CarregarComentarios():
        private void CarregarComentarios()
        {
            try
            {
                flowLayoutPanelComentarios.Controls.Clear();

                using (var context = new ApplicationDbContext())
                {
                    // ✅ CORREÇÃO: Usar Include corretamente
                    var comentarios = context.Comentarios
                        .Include("Usuario") // ← String-based include para evitar erros
                        .Where(c => c.LivroId == _livroId)
                        .OrderByDescending(c => c.DataComentario)
                        .ToList();

                    // Resto do método...
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar comentários: {ex.Message}\n\nDetalhes: {ex.InnerException?.Message}");
            }
        }

        // No método RegistrarVotoLivro():
        private void RegistrarVotoLivro(bool isLike)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // ✅ Verifica se já votou
                    var existingVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (existingVote != null)
                    {
                        // ✅ Atualiza o voto existente
                        existingVote.IsLike = isLike;
                        existingVote.DataAcao = DateTime.Now;
                    }
                    else
                    {
                        // ✅ Cria novo voto
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar voto: {ex.Message}");
            }
        }

    }
}