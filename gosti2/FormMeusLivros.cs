using System;
using System.Data.Entity;
using System.Drawing;
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

            // ADICIONA COLUNA USUARIOID OCULTA LOGO NO INÍCIO
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
                    int livroUsuarioId = Convert.ToInt32(
                        dataGridViewLivros.SelectedRows[0].Cells["UsuarioId"].Value);

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
                        int rowIndex = dataGridViewLivros.Rows.Add(
                            livro.LivroId,
                            null,
                            livro.Titulo,
                            livro.Autor,
                            livro.Genero,
                            livro.Lido ? "✅ Lido" : "📖 Para Ler",
                            livro.DataAdicao.ToString("dd/MM/yyyy"),
                            livro.UsuarioId  // ADICIONA USUARIOID AQUI
                        );

                        // Destaca livros do usuário logado
                        if (livro.UsuarioId == usuarioLogadoId)
                        {
                            dataGridViewLivros.Rows[rowIndex].DefaultCellStyle.BackColor =
                                Color.LightCyan;
                        }

                        if (livro.Favorito)
                        {
                            dataGridViewLivros.Rows[rowIndex].DefaultCellStyle.BackColor =
                                Color.LightYellow;
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
                        int rowIndex = dataGridViewLivros.Rows.Add(
                            livro.LivroId,
                            null,
                            livro.Titulo,
                            livro.Autor,
                            livro.Genero,
                            livro.Lido ? "✅ Lido" : "📖 Para Ler",
                            livro.DataAdicao.ToString("dd/MM/yyyy"),
                            livro.UsuarioId
                        );

                        if (livro.UsuarioId == usuarioLogadoId)
                        {
                            dataGridViewLivros.Rows[rowIndex].DefaultCellStyle.BackColor =
                                Color.LightCyan;
                        }

                        if (livro.Favorito)
                        {
                            dataGridViewLivros.Rows[rowIndex].DefaultCellStyle.BackColor =
                                Color.LightYellow;
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

            var livroId = Convert.ToInt32(dataGridViewLivros.SelectedRows[0].Cells["colLivroId"].Value);

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

            var livroId = Convert.ToInt32(dataGridViewLivros.SelectedRows[0].Cells["colLivroId"].Value);
            var titulo = dataGridViewLivros.SelectedRows[0].Cells["colTitulo"].Value.ToString();

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

            var livroId = Convert.ToInt32(dataGridViewLivros.SelectedRows[0].Cells["colLivroId"].Value);

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