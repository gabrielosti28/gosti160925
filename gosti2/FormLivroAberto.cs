using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using gosti2.Models;
using gosti2.Data;
using System.Collections.Generic;

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

            // CORREÇÃO: Removido DiagnosticContext completamente
            ConfigureUI();
            CarregarLivro();
            CarregarComentarios();
            CarregarLikesDislikes();
            VerificarVotoUsuario();
        }

        private void ConfigureUI()
        {
            // Configurações básicas de UI
            flowLayoutPanelComentarios.AutoScroll = true;

            // Placeholder simples para comentário
            txtNovoComentario.Text = "Digite seu comentário aqui...";
            txtNovoComentario.ForeColor = Color.Gray;

            txtNovoComentario.Enter += (s, e) =>
            {
                if (txtNovoComentario.Text == "Digite seu comentário aqui...")
                {
                    txtNovoComentario.Text = "";
                    txtNovoComentario.ForeColor = Color.Black;
                }
            };

            txtNovoComentario.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNovoComentario.Text))
                {
                    txtNovoComentario.Text = "Digite seu comentário aqui...";
                    txtNovoComentario.ForeColor = Color.Gray;
                }
            };
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
                        lblAdicionadoPor.Text = $"Adicionado por: {_livro.Usuario?.NomeUsuario ?? "Usuário desconhecido"}";
                        txtDescricao.Text = _livro.Descricao ?? "Sem descrição";

                        CarregarCapaLivro(_livro.Capa);
                    }
                    else
                    {
                        MessageBox.Show("Livro não encontrado.", "Erro");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livro: {ex.Message}", "Erro");
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
                        pictureBoxCapa.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxCapa.Image = CriarCapaPadrao();
                }
            }
            catch (Exception)
            {
                pictureBoxCapa.Image = CriarCapaPadrao();
            }
        }

        private Bitmap CriarCapaPadrao()
        {
            var bmp = new Bitmap(200, 300);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                using (var font = new Font("Arial", 12, FontStyle.Bold))
                {
                    g.DrawString("📚", font, Brushes.Black, new PointF(80, 120));
                    g.DrawString("Sem Capa", new Font("Arial", 10), Brushes.Black, new PointF(60, 180));
                }
            }
            return bmp;
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

                    if (comentarios.Any())
                    {
                        foreach (var comentario in comentarios)
                        {
                            AdicionarComentarioPanel(comentario);
                        }
                    }
                    else
                    {
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
                MessageBox.Show($"Erro ao carregar comentários: {ex.Message}", "Erro");
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
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Cabeçalho do comentário
            var lblUsuario = new Label
            {
                Text = comentario.Usuario?.NomeUsuario ?? "Usuário desconhecido",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(70, 130, 180)
            };

            var lblData = new Label
            {
                Text = comentario.DataComentario.ToString("dd/MM/yyyy HH:mm"),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(panel.Width - 120, 10),
                AutoSize = true
            };

            // Texto do comentário
            var txtComentario = new TextBox
            {
                Text = comentario.Texto,
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Location = new Point(10, 35),
                Size = new Size(panel.Width - 30, 50),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9),
                ScrollBars = ScrollBars.Vertical
            };

            // Botões de like/dislike
            var btnLike = new Button
            {
                Text = $"👍 {comentario.Likes}",
                Tag = comentario.ComentarioId,
                Size = new Size(60, 25),
                Location = new Point(10, 90),
                Font = new Font("Segoe UI", 8),
                FlatStyle = FlatStyle.Flat
            };
            btnLike.Click += BtnLikeComentario_Click;

            var btnDislike = new Button
            {
                Text = $"👎 {comentario.Dislikes}",
                Tag = comentario.ComentarioId,
                Size = new Size(60, 25),
                Location = new Point(80, 90),
                Font = new Font("Segoe UI", 8),
                FlatStyle = FlatStyle.Flat
            };
            btnDislike.Click += BtnDislikeComentario_Click;

            // Adicionar controles ao painel
            panel.Controls.Add(lblUsuario);
            panel.Controls.Add(lblData);
            panel.Controls.Add(txtComentario);
            panel.Controls.Add(btnLike);
            panel.Controls.Add(btnDislike);

            flowLayoutPanelComentarios.Controls.Add(panel);
        }

        private void btnComentar_Click(object sender, EventArgs e)
        {
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("Você precisa estar logado para comentar.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNovoComentario.ForeColor == Color.Gray ||
                string.IsNullOrWhiteSpace(txtNovoComentario.Text) ||
                txtNovoComentario.Text == "Digite seu comentário aqui...")
            {
                MessageBox.Show("Digite um comentário antes de enviar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNovoComentario.Text.Length < 5)
            {
                MessageBox.Show("Comentário muito curto. Mínimo 5 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AppManager.AdicionarComentario(_livroId, txtNovoComentario.Text.Trim()))
            {
                txtNovoComentario.Clear();
                CarregarComentarios();
                MessageBox.Show("Comentário adicionado!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLikeLivro_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("Você precisa estar logado para curtir.", "Acesso Negado");
                return;
            }
            RegistrarVotoLivro(true);
        }

        private void btnDislikeLivro_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("Você precisa estar logado para não curtir.", "Acesso Negado");
                return;
            }
            RegistrarVotoLivro(false);
        }

        private void RegistrarVotoLivro(bool isLike)
        {
            if (AppManager.VotarLivro(_livroId, isLike))
            {
                CarregarLikesDislikes();
                VerificarVotoUsuario();
            }
        }

        private void BtnLikeComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("Você precisa estar logado para curtir comentários.", "Acesso Negado");
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
                MessageBox.Show("Você precisa estar logado para não curtir comentários.", "Acesso Negado");
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
                MessageBox.Show($"Erro ao votar no comentário: {ex.Message}", "Erro");
            }
        }

        private void CarregarLikesDislikes()
        {
            var (likes, dislikes, votoUsuario) = AppManager.ObterVotosLivro(_livroId);

            lblLikes.Text = $"👍 {likes}";
            lblDislikes.Text = $"👎 {dislikes}";
        }

        private void VerificarVotoUsuario()
        {
            if (!AppManager.EstaLogado) return;

            var (likes, dislikes, votoUsuario) = AppManager.ObterVotosLivro(_livroId);

            // Resetar cores
            btnLikeLivro.BackColor = Color.FromArgb(248, 249, 250);
            btnDislikeLivro.BackColor = Color.FromArgb(248, 249, 250);
            btnLikeLivro.Text = "👍";
            btnDislikeLivro.Text = "👎";

            if (votoUsuario.HasValue)
            {
                if (votoUsuario.Value)
                {
                    btnLikeLivro.BackColor = Color.LightGreen;
                    btnLikeLivro.Text = "👍 Você curtiu";
                }
                else
                {
                    btnDislikeLivro.BackColor = Color.LightCoral;
                    btnDislikeLivro.Text = "👎 Você não curtiu";
                }
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
                if (!string.IsNullOrWhiteSpace(txtNovoComentario.Text) && txtNovoComentario.ForeColor != Color.Gray)
                {
                    btnComentar.PerformClick();
                }
                e.Handled = true;
            }
        }

        private void FormLivroAberto_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Limpeza segura de recursos
            if (pictureBoxCapa.Image != null)
            {
                pictureBoxCapa.Image.Dispose();
            }
        }
    }
}