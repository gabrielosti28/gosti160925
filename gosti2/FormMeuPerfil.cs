using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormMeuPerfil : Form
    {
        public FormMeuPerfil()
        {
            InitializeComponent();

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CarregarDadosPerfil();
        }

        private void CarregarDadosPerfil()
        {
            var usuario = AppManager.UsuarioLogado;

            // FOTO DO PERFIL
            CarregarFotoPerfil();

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
        }

        private void CarregarFotoPerfil()
        {
            var usuario = AppManager.UsuarioLogado;

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
            var usuario = AppManager.UsuarioLogado;

            try
            {
                using (var db = new ApplicationDbContext())
                {
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
                    int totalLivros = db.Livros.Count(l => l.UsuarioId == usuario.UsuarioId);
                    lblTotalLivros.Text = $"📚 Livros adicionados: {totalLivros}";

                    // TOTAL DE COMENTÁRIOS
                    int totalComentarios = db.Comentarios.Count(c => c.UsuarioId == usuario.UsuarioId);
                    lblTotalComentarios.Text = $"💬 Comentários: {totalComentarios}";

                    // TIER LISTS (FUTURO)
                    lblTotalTierLists.Text = "⭐ Tier Lists: 0 (Em breve)";
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
            var usuario = AppManager.UsuarioLogado;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var ultimosLivros = db.Livros
                        .Where(l => l.UsuarioId == usuario.UsuarioId)
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

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}