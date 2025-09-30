using System;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
            usuarioLogadoId = UsuarioManager.UsuarioLogado?.UsuarioId ?? 0; // Corrigido para UsuarioId
            ConfigurarDataGridView();
            CarregarLivros();
        }

        private void ConfigurarDataGridView()
        {
            // Adiciona coluna oculta para LivroId
            if (!dataGridViewLivros.Columns.Contains("colLivroId"))
            {
                var colunaLivroId = new DataGridViewTextBoxColumn
                {
                    Name = "colLivroId",
                    HeaderText = "LivroId",
                    Visible = false,
                    DataPropertyName = "LivroId"
                };
                dataGridViewLivros.Columns.Insert(0, colunaLivroId);
            }

            // Configurações de performance
            dataGridViewLivros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLivros.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewLivros.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void CarregarLivros()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Carrega apenas livros do usuário logado com includes necessários
                    var livros = context.Livros
                        .Include(l => l.Usuario)
                        .Include(l => l.CategoriaTier) // Inclui a categoria tier
                        .Where(l => l.UsuarioId == usuarioLogadoId) // Filtra por usuário logado
                        .OrderByDescending(l => l.DataAdicao)
                        .ToList();

                    dataGridViewLivros.Rows.Clear();

                    foreach (var livro in livros)
                    {
                        Image capaImage = ObterImagemCapa(livro.Capa);

                        string statusLeitura = ObterStatusLeitura(livro);
                        string categoriaTier = livro.CategoriaTier?.Nome ?? "Não categorizado";

                        int rowIndex = dataGridViewLivros.Rows.Add(
                            livro.LivroId, // ID oculto para referência
                            capaImage,
                            livro.Titulo,
                            livro.Autor,
                            livro.Genero,
                            statusLeitura,
                            categoriaTier,
                            livro.DataAdicao.ToString("dd/MM/yyyy")
                        );

                        // Configurações visuais da linha
                        dataGridViewLivros.Rows[rowIndex].Height = 160;
                        dataGridViewLivros.Rows[rowIndex].Cells["colCapa"].Style.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;

                        // Destaca livros favoritos
                        if (livro.Favorito)
                        {
                            dataGridViewLivros.Rows[rowIndex].DefaultCellStyle.BackColor =
                                Color.FromArgb(255, 255, 200); // Amarelo claro para favoritos
                        }
                    }

                    AtualizarEstatisticas(livros.Count());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image ObterImagemCapa(byte[] capaBytes)
        {
            if (capaBytes != null && capaBytes.Length > 0)
            {
                try
                {
                    using (var ms = new System.IO.MemoryStream(capaBytes))
                    {
                        var originalImage = Image.FromStream(ms);
                        return RedimensionarImagem(originalImage, 80, 120); // Tamanho otimizado
                    }
                }
                catch
                {
                    // Fallback para imagem padrão em caso de erro
                    return CriarImagemPadrao();
                }
            }
            return CriarImagemPadrao();
        }

        private Image CriarImagemPadrao()
        {
            // Cria uma imagem padrão programaticamente
            var defaultImage = new Bitmap(80, 120);
            using (var g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);
                using (var font = new Font("Arial", 8))
                using (var brush = new SolidBrush(Color.DarkGray))
                {
                    g.DrawString("Sem Capa", font, brush, 10, 50);
                }
            }
            return defaultImage;
        }

        private string ObterStatusLeitura(Livro livro)
        {
            if (livro.Lido) return "✅ Lido";
            if (livro.Favorito) return "⭐ Favorito";
            return "📖 Para Ler";
        }

        private void AtualizarEstatisticas(int totalLivros)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livrosLidos = context.Livros
                        .Count(l => l.UsuarioId == usuarioLogadoId && l.Lido);

                    var livrosFavoritos = context.Livros
                        .Count(l => l.UsuarioId == usuarioLogadoId && l.Favorito);

                    lblEstatisticas.Text =
                        $"Total: {totalLivros} | Lidos: {livrosLidos} | Favoritos: {livrosFavoritos}";
                }
            }
            catch (Exception ex)
            {
                lblEstatisticas.Text = "Estatísticas indisponíveis";
                Console.WriteLine($"Erro ao carregar estatísticas: {ex.Message}");
            }
        }

        private Image RedimensionarImagem(Image imagem, int largura, int altura)
        {
            var destRect = new Rectangle(0, 0, largura, altura);
            var destImage = new Bitmap(largura, altura);

            destImage.SetResolution(imagem.HorizontalResolution, imagem.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(imagem, destRect, 0, 0, imagem.Width, imagem.Height,
                                     GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
            var livroId = ObterIdLivroSelecionado();
            if (livroId == 0)
            {
                MessageBox.Show("Selecione um livro para editar.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificação de propriedade redundante (já filtramos por usuário)
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
            var livroId = ObterIdLivroSelecionado();
            if (livroId == 0)
            {
                MessageBox.Show("Selecione um livro para remover.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tituloLivro = dataGridViewLivros.SelectedRows[0].Cells["colTitulo"].Value.ToString();
            var result = MessageBox.Show(
                $"Tem certeza que deseja remover o livro '{tituloLivro}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar Remoção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                RemoverLivro(livroId);
            }
        }

        private int ObterIdLivroSelecionado()
        {
            if (dataGridViewLivros.SelectedRows.Count > 0)
            {
                var row = dataGridViewLivros.SelectedRows[0];
                if (row.Cells["colLivroId"].Value != null)
                {
                    return Convert.ToInt32(row.Cells["colLivroId"].Value);
                }
            }
            return 0;
        }

        private void RemoverLivro(int livroId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livro = context.Livros
                        .Include(l => l.Avaliacoes)
                        .Include(l => l.Comentarios)
                        .Include(l => l.LikesDislikes)
                        .FirstOrDefault(l => l.LivroId == livroId);

                    if (livro != null)
                    {
                        // O Entity Framework cuidará do cascade delete devido às configurações
                        context.Livros.Remove(livro);
                        context.SaveChanges();

                        MessageBox.Show("Livro removido com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CarregarLivros();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewLivros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                AbrirDetalhesLivro();
            }
        }

        private void AbrirDetalhesLivro()
        {
            var livroId = ObterIdLivroSelecionado();
            if (livroId > 0)
            {
                try
                {
                    DatabaseInitializer.Initialize(); // Garante que o banco está inicializado

                    using (var formLivroAberto = new FormLivroAberto(livroId))
                    {
                        formLivroAberto.ShowDialog();
                        CarregarLivros(); // Recarrega para refletir possíveis mudanças
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao abrir livro: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewLivros_SelectionChanged(object sender, EventArgs e)
        {
            bool linhaSelecionada = dataGridViewLivros.SelectedRows.Count > 0;
            btnEditar.Enabled = linhaSelecionada;
            btnRemover.Enabled = linhaSelecionada;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarLivros();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarLivros(txtPesquisa.Text.Trim());
        }

        private void FiltrarLivros(string termoPesquisa)
        {
            if (string.IsNullOrEmpty(termoPesquisa))
            {
                foreach (DataGridViewRow row in dataGridViewLivros.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            foreach (DataGridViewRow row in dataGridViewLivros.Rows)
            {
                var titulo = row.Cells["colTitulo"].Value?.ToString() ?? "";
                var autor = row.Cells["colAutor"].Value?.ToString() ?? "";
                var genero = row.Cells["colGenero"].Value?.ToString() ?? "";

                bool corresponde = titulo.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                 autor.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                 genero.IndexOf(termoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0;

                row.Visible = corresponde;
            }
        }
    }
}