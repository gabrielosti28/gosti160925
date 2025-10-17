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

            // Verificar se está logado
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            usuarioLogadoId = AppManager.UsuarioLogado.UsuarioId;
            ConfigurarDataGridView();
            CarregarLivros();
        }

        private void ConfigurarDataGridView()
        {
            // Configuração simples do grid
            dataGridViewLivros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                            livro.Titulo,
                            livro.Autor,
                            livro.Genero,
                            livro.Lido ? "✅ Lido" : "📖 Para Ler",
                            livro.DataAdicao.ToString("dd/MM/yyyy")
                        );

                        if (livro.Favorito)
                        {
                            dataGridViewLivros.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                    }

                    // Atualizar estatísticas
                    var (total, lidos, favoritos) = AppManager.ObterEstatisticasUsuario();
                    lblEstatisticas.Text = $"📚 {total} Livros | ✅ {lidos} Lidos | ⭐ {favoritos} Favoritos";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
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
                MessageBox.Show("Selecione um livro para editar", "Aviso");
                return;
            }

            var livroId = Convert.ToInt32(dataGridViewLivros.SelectedRows[0].Cells[0].Value);
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

            var livroId = Convert.ToInt32(dataGridViewLivros.SelectedRows[0].Cells[0].Value);
            var titulo = dataGridViewLivros.SelectedRows[0].Cells[1].Value.ToString();

            if (MessageBox.Show($"Deseja realmente remover '{titulo}'?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (AppManager.RemoverLivro(livroId))
                {
                    CarregarLivros();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}