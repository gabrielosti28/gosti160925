using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace gosti2
{
    public partial class FormMeusLivros : Form
    {
        private int usuarioLogadoId;

        public FormMeusLivros()
        {
            InitializeComponent();
            usuarioLogadoId = UsuarioManager.UsuarioLogado?.UserId ?? 0;
            CarregarLivros();
        }

        private void CarregarLivros()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Carrega todos os livros com informações do usuário
                    var livros = context.Livros
                        .Include(l => l.Usuario)
                        .ToList();

                    dataGridViewLivros.Rows.Clear();

                    foreach (var livro in livros)
                    {
                        Image capaImage = null;
                        if (livro.Capa != null && livro.Capa.Length > 0)
                        {
                            using (var ms = new System.IO.MemoryStream(livro.Capa))
                            {
                                capaImage = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            // Imagem padrão quando não há capa
                            capaImage = Properties.Resources.default_book_cover;
                        }

                        int rowIndex = dataGridViewLivros.Rows.Add(
                            capaImage,
                            livro.Titulo,
                            livro.Autor,
                            livro.CategoriaId,
                            livro.Lido ? "Lido" : "Não Lido",
                            livro.UsuarioId,
                            livro.Usuario.Nome
                        );

                        // Define a altura da linha para acomodar a imagem maior
                        dataGridViewLivros.Rows[rowIndex].Height = 120;
                    }

                    // Esconde as colunas de ID do usuário
                    dataGridViewLivros.Columns["colUsuarioId"].Visible = false;
                    dataGridViewLivros.Columns["colUsuarioNome"].Visible = false;
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
                    CarregarLivros(); // Recarrega a lista após adicionar
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para editar.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow linhaSelecionada = dataGridViewLivros.SelectedRows[0];
            int livroUsuarioId = Convert.ToInt32(linhaSelecionada.Cells["colUsuarioId"].Value);

            if (livroUsuarioId != usuarioLogadoId)
            {
                MessageBox.Show("Você só pode editar os livros que adicionou.",
                    "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obter o ID do livro selecionado
            int livroId = ObterIdLivroSelecionado();
            if (livroId > 0)
            {
                using (var formEditar = new FormAdicionarLivro(livroId))
                {
                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        CarregarLivros(); // Recarrega a lista após editar
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possível identificar o livro selecionado.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dataGridViewLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para remover.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow linhaSelecionada = dataGridViewLivros.SelectedRows[0];
            int livroUsuarioId = Convert.ToInt32(linhaSelecionada.Cells["colUsuarioId"].Value);

            if (livroUsuarioId != usuarioLogadoId)
            {
                MessageBox.Show("Você só pode remover os livros que adicionou.",
                    "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tituloLivro = linhaSelecionada.Cells["colTitulo"].Value.ToString();
            var result = MessageBox.Show($"Tem certeza que deseja remover o livro '{tituloLivro}'?",
                "Confirmar Remoção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int livroId = ObterIdLivroSelecionado();
                if (livroId > 0)
                {
                    RemoverLivro(livroId);
                    CarregarLivros(); // Recarrega a lista após remover
                }
            }
        }

        private int ObterIdLivroSelecionado()
        {
            if (dataGridViewLivros.SelectedRows.Count > 0)
            {
                // Em uma implementação real, você teria o ID do livro em uma coluna oculta
                // Por enquanto, vamos buscar pelo título (não ideal, mas funcional)
                string titulo = dataGridViewLivros.SelectedRows[0].Cells["colTitulo"].Value.ToString();

                using (var context = new ApplicationDbContext())
                {
                    var livro = context.Livros.FirstOrDefault(l => l.Titulo == titulo);
                    return livro?.LivroId ?? 0;
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
                    var livro = context.Livros.Find(livroId);
                    if (livro != null)
                    {
                        context.Livros.Remove(livro);
                        context.SaveChanges();
                        MessageBox.Show("Livro removido com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridViewLivros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Se clicou na coluna da imagem (índice 0)
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Aqui você pode abrir o form de detalhes no futuro
                MessageBox.Show("Funcionalidade de detalhes do livro em desenvolvimento!",
                    "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewLivros_SelectionChanged(object sender, EventArgs e)
        {
            // Habilita/desabilita botões baseado na seleção e propriedade
            if (dataGridViewLivros.SelectedRows.Count > 0)
            {
                DataGridViewRow linhaSelecionada = dataGridViewLivros.SelectedRows[0];
                int livroUsuarioId = Convert.ToInt32(linhaSelecionada.Cells["colUsuarioId"].Value);

                bool usuarioEDono = (livroUsuarioId == usuarioLogadoId);
                btnEditar.Enabled = usuarioEDono;
                btnRemover.Enabled = usuarioEDono;

                // Altera a cor de fundo para indicar propriedade
                foreach (DataGridViewRow row in dataGridViewLivros.Rows)
                {
                    int rowUsuarioId = Convert.ToInt32(row.Cells["colUsuarioId"].Value);
                    if (rowUsuarioId == usuarioLogadoId)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // Azul claro
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240); // Laranja claro
                    }
                }
            }
            else
            {
                btnEditar.Enabled = false;
                btnRemover.Enabled = false;
            }
        }
    }
}