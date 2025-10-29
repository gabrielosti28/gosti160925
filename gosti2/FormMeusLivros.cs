using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;

namespace gosti2
{
    public partial class FormMeusLivros : Form
    {
        private int usuarioLogadoId;

        public FormMeusLivros()
        {
            InitializeComponent();

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado para acessar esta área.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            usuarioLogadoId = AppManager.UsuarioLogado.UsuarioId;
            ConfigurarDataGridView();
            ConfigurarEventos();
            CarregarLivros();
        }

        private void ConfigurarDataGridView()
        {
            dataGridViewLivros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLivros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewLivros.MultiSelect = false;
            dataGridViewLivros.ReadOnly = true;

            // CORRIGIDO: Garante que a coluna LivroId existe e está visível (mas pode ficar escondida)
            if (dataGridViewLivros.Columns["colLivroId"] != null)
            {
                dataGridViewLivros.Columns["colLivroId"].Visible = false; // Mantém invisível para o usuário
            }

            // Verifica se a coluna UsuarioId já existe antes de adicionar
            if (dataGridViewLivros.Columns["UsuarioId"] == null)
            {
                var colUsuarioId = new DataGridViewTextBoxColumn
                {
                    Name = "UsuarioId",
                    HeaderText = "UsuarioId",
                    Visible = false
                };
                dataGridViewLivros.Columns.Add(colUsuarioId);
            }
        }

        private void ConfigurarEventos()
        {
            dataGridViewLivros.SelectionChanged += (s, e) =>
            {
                if (dataGridViewLivros.SelectedRows.Count > 0)
                {
                    // CORRIGIDO: Usa índice da coluna em vez do nome
                    var row = dataGridViewLivros.SelectedRows[0];
                    int livroUsuarioId = Convert.ToInt32(row.Cells["UsuarioId"].Value);

                    bool ehDonoDoLivro = (livroUsuarioId == usuarioLogadoId);
                    btnEditar.Enabled = ehDonoDoLivro;
                    btnRemover.Enabled = ehDonoDoLivro;
                }
                else
                {
                    btnEditar.Enabled = false;
                    btnRemover.Enabled = false;
                }
            };

            dataGridViewLivros.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    AbrirDetalhesLivro();
                }
            };

            txtPesquisa.TextChanged += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
                {
                    CarregarLivros();
                }
                else
                {
                    BuscarLivros(txtPesquisa.Text);
                }
            };

            btnAtualizar.Click += (s, e) => CarregarLivros();
        }

        private void CarregarLivros()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livros = context.Livros
                        .Include(l => l.Usuario)
                        .OrderByDescending(l => l.DataAdicao)
                        .ToList();

                    dataGridViewLivros.Rows.Clear();

                    foreach (var livro in livros)
                    {
                        // CONVERTER BYTES DA CAPA PARA IMAGEM MANTENDO PROPORÇÃO
                        Image capaImage = null;
                        if (livro.Capa != null && livro.Capa.Length > 0)
                        {
                            try
                            {
                                using (var ms = new MemoryStream(livro.Capa))
                                {
                                    var originalImage = Image.FromStream(ms);
                                    capaImage = RedimensionarImagemProporcional(originalImage, 70, 100);
                                }
                            }
                            catch
                            {
                                capaImage = null;
                            }
                        }

                        // CORRIGIDO: Adiciona células na ordem correta das colunas
                        int rowIndex = dataGridViewLivros.Rows.Add();
                        var row = dataGridViewLivros.Rows[rowIndex];

                        // Preenche as células pelos nomes das colunas
                        row.Cells["colLivroId"].Value = livro.LivroId;
                        row.Cells["colCapa"].Value = capaImage;
                        row.Cells["colTitulo"].Value = livro.Titulo;
                        row.Cells["colAutor"].Value = livro.Autor;
                        row.Cells["colGenero"].Value = livro.Genero;
                        row.Cells["colStatus"].Value = livro.Lido ? "✅ Lido" : "📖 Para Ler";
                        row.Cells["colDataAdicao"].Value = livro.DataAdicao.ToString("dd/MM/yyyy");
                        row.Cells["UsuarioId"].Value = livro.UsuarioId;

                        // Ajustar altura da linha para melhor visualização da capa
                        row.Height = 110;

                        // Destaca livros do usuário logado
                        if (livro.UsuarioId == usuarioLogadoId)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCyan;
                        }

                        if (livro.Favorito)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                    }

                    AtualizarEstatisticas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarLivros(string termo)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livros = context.Livros
                        .Include(l => l.Usuario)
                        .Where(l => l.Titulo.Contains(termo) || l.Autor.Contains(termo))
                        .OrderByDescending(l => l.DataAdicao)
                        .ToList();

                    dataGridViewLivros.Rows.Clear();

                    foreach (var livro in livros)
                    {
                        // CONVERTER BYTES DA CAPA PARA IMAGEM MANTENDO PROPORÇÃO
                        Image capaImage = null;
                        if (livro.Capa != null && livro.Capa.Length > 0)
                        {
                            try
                            {
                                using (var ms = new MemoryStream(livro.Capa))
                                {
                                    var originalImage = Image.FromStream(ms);
                                    capaImage = RedimensionarImagemProporcional(originalImage, 70, 100);
                                }
                            }
                            catch
                            {
                                capaImage = null;
                            }
                        }

                        // CORRIGIDO: Adiciona células na ordem correta
                        int rowIndex = dataGridViewLivros.Rows.Add();
                        var row = dataGridViewLivros.Rows[rowIndex];

                        row.Cells["colLivroId"].Value = livro.LivroId;
                        row.Cells["colCapa"].Value = capaImage;
                        row.Cells["colTitulo"].Value = livro.Titulo;
                        row.Cells["colAutor"].Value = livro.Autor;
                        row.Cells["colGenero"].Value = livro.Genero;
                        row.Cells["colStatus"].Value = livro.Lido ? "✅ Lido" : "📖 Para Ler";
                        row.Cells["colDataAdicao"].Value = livro.DataAdicao.ToString("dd/MM/yyyy");
                        row.Cells["UsuarioId"].Value = livro.UsuarioId;

                        row.Height = 110;

                        if (livro.UsuarioId == usuarioLogadoId)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCyan;
                        }

                        if (livro.Favorito)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA REDIMENSIONAR IMAGEM MANTENDO A PROPORÇÃO
        private Image RedimensionarImagemProporcional(Image imagemOriginal, int larguraMax, int alturaMax)
        {
            if (imagemOriginal == null) return null;

            var ratioX = (double)larguraMax / imagemOriginal.Width;
            var ratioY = (double)alturaMax / imagemOriginal.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var novaLargura = (int)(imagemOriginal.Width * ratio);
            var novaAltura = (int)(imagemOriginal.Height * ratio);

            var novaImagem = new Bitmap(novaLargura, novaAltura);

            using (var graphics = Graphics.FromImage(novaImagem))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(imagemOriginal, 0, 0, novaLargura, novaAltura);
            }

            return novaImagem;
        }

        private void AtualizarEstatisticas()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var totalLivros = context.Livros.Count();
                    var livrosLidos = context.Livros.Count(l => l.Lido);
                    var livrosFavoritos = context.Livros.Count(l => l.Favorito);

                    lblEstatisticas.Text = $"📚 {totalLivros} Livros | ✅ {livrosLidos} Lidos | ⭐ {livrosFavoritos} Favoritos";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar estatísticas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var formAdicionar = new FormAdicionarLivro())
            {
                if (formAdicionar.ShowDialog() == DialogResult.OK)
                {
                    CarregarLivros();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CORRIGIDO: Obtém o LivroId corretamente
            var row = dataGridViewLivros.SelectedRows[0];
            var livroId = Convert.ToInt32(row.Cells["colLivroId"].Value);

            using (var formEditar = new FormAdicionarLivro(livroId))
            {
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    CarregarLivros();
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dataGridViewLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para remover.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CORRIGIDO: Obtém dados corretamente
            var row = dataGridViewLivros.SelectedRows[0];
            var livroId = Convert.ToInt32(row.Cells["colLivroId"].Value);
            var titulo = row.Cells["colTitulo"].Value.ToString();

            if (MessageBox.Show($"Deseja realmente remover '{titulo}'?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (AppManager.RemoverLivro(livroId))
                {
                    CarregarLivros();
                }
            }
        }

        private void AbrirDetalhesLivro()
        {
            if (dataGridViewLivros.SelectedRows.Count == 0)
                return;

            // CORRIGIDO: Obtém o LivroId corretamente
            var row = dataGridViewLivros.SelectedRows[0];
            var livroId = Convert.ToInt32(row.Cells["colLivroId"].Value);

            using (var formDetalhes = new FormLivroAberto(livroId, usuarioLogadoId))
            {
                formDetalhes.ShowDialog();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}