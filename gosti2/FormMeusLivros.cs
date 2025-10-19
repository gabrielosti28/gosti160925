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

            // Verificar se está logado
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
        }

        private void ConfigurarEventos()
        {
            // Habilitar/desabilitar botões baseado na seleção
            dataGridViewLivros.SelectionChanged += (s, e) =>
            {
                bool temSelecao = dataGridViewLivros.SelectedRows.Count > 0;
                btnEditar.Enabled = temSelecao;
                btnRemover.Enabled = temSelecao;
            };

            // Duplo clique para abrir detalhes
            dataGridViewLivros.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    AbrirDetalhesLivro();
                }
            };

            // Pesquisa em tempo real
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

            // Atualizar lista
            btnAtualizar.Click += (s, e) => CarregarLivros();
        }

        private void CarregarLivros()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livros = context.Livros
                        .Where(l => l.UsuarioId == usuarioLogadoId)
                        .OrderByDescending(l => l.DataAdicao)
                        .ToList();

                    dataGridViewLivros.Rows.Clear();

                    foreach (var livro in livros)
                    {
                        int rowIndex = dataGridViewLivros.Rows.Add(
                            livro.LivroId,
                            null, // Capa (pode ser implementada depois)
                            livro.Titulo,
                            livro.Autor,
                            livro.Genero,
                            livro.Lido ? "✅ Lido" : "📖 Para Ler",
                            livro.DataAdicao.ToString("dd/MM/yyyy")
                        );

                        // Destaca favoritos
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
                        .Where(l => l.UsuarioId == usuarioLogadoId &&
                                   (l.Titulo.Contains(termo) || l.Autor.Contains(termo)))
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
                            livro.DataAdicao.ToString("dd/MM/yyyy")
                        );

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
            var (total, lidos, favoritos) = AppManager.ObterEstatisticasUsuario();
            lblEstatisticas.Text = $"📚 {total} Livros | ✅ {lidos} Lidos | ⭐ {favoritos} Favoritos";
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