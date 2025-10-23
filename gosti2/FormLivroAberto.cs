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
            _usuarioLogadoId = usuarioLogadoId > 0 ? usuarioLogadoId : (AppManager.UsuarioLogado?.UsuarioId ?? 0);

            ConfigureUI();
            CarregarLivro();
            CarregarComentarios();
        }

        private void ConfigureUI()
        {
            flowLayoutPanelComentarios.AutoScroll = true;

            // Placeholder para comentário
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
                _livro = AppManager.ObterLivro(_livroId);

                if (_livro != null)
                {
                    lblTitulo.Text = _livro.Titulo;
                    lblAutor.Text = $"Autor: {_livro.Autor}";
                    lblGenero.Text = $"Gênero: {_livro.Genero}";

                    // LABEL DO USUÁRIO COM CURSOR CLICÁVEL
                    lblAdicionadoPor.Text = $"Adicionado por: {_livro.Usuario?.NomeUsuario ?? "Usuário desconhecido"}";
                    lblAdicionadoPor.Cursor = Cursors.Hand;
                    lblAdicionadoPor.ForeColor = Color.FromArgb(70, 130, 180);
                    lblAdicionadoPor.Font = new Font(lblAdicionadoPor.Font, FontStyle.Underline);

                    // ADICIONA EVENTO DE CLIQUE
                    lblAdicionadoPor.Click += (s, e) => AbrirPerfilUsuario(_livro.UsuarioId);

                    txtDescricao.Text = _livro.Descricao ?? "Sem descrição disponível";

                    CarregarCapaLivro(_livro.Capa);
                }
                else
                {
                    MessageBox.Show("Livro não encontrado.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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

                var comentarios = AppManager.CarregarComentarios(_livroId);

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
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Nome do usuário - CLICÁVEL
            var lblUsuario = new Label
            {
                Text = comentario.Usuario?.NomeUsuario ?? "Usuário desconhecido",
                Font = new Font("Segoe UI", 9, FontStyle.Bold | FontStyle.Underline),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(70, 130, 180),
                Cursor = Cursors.Hand
            };

            // ADICIONA EVENTO DE CLIQUE NO NOME DO USUÁRIO
            lblUsuario.Click += (s, e) => AbrirPerfilUsuario(comentario.UsuarioId);

            // Data do comentário
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

            panel.Controls.Add(lblUsuario);
            panel.Controls.Add(lblData);
            panel.Controls.Add(txtComentario);

            flowLayoutPanelComentarios.Controls.Add(panel);
        }

        private void AbrirPerfilUsuario(int usuarioId)
        {
            try
            {
                using (var formPerfil = new FormMeuPerfil(usuarioId))
                {
                    formPerfil.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir perfil: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                txtNovoComentario.Text = "Digite seu comentário aqui...";
                txtNovoComentario.ForeColor = Color.Gray;
                CarregarComentarios();
                MessageBox.Show("Comentário adicionado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!string.IsNullOrWhiteSpace(txtNovoComentario.Text) &&
                    txtNovoComentario.ForeColor != Color.Gray)
                {
                    btnComentar.PerformClick();
                }
                e.Handled = true;
            }
        }

        private void FormLivroAberto_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Limpeza de recursos
            if (pictureBoxCapa.Image != null)
            {
                pictureBoxCapa.Image.Dispose();
            }
        }
    }
}