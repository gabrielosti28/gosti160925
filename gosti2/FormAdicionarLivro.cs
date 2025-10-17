using System;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;

namespace gosti2
{
    public partial class FormAdicionarLivro : Form
    {
        private int _livroId;
        private bool _modoEdicao = false;

        // Construtor padrão (para adicionar novo livro)
        public FormAdicionarLivro()
        {
            InitializeComponent();
            _modoEdicao = false;
            this.Text = "Adicionar Novo Livro";
        }

        // Construtor para edição (com ID do livro)
        public FormAdicionarLivro(int livroId)
        {
            InitializeComponent();
            _livroId = livroId;
            _modoEdicao = true;
            this.Text = "Editar Livro";
            CarregarDadosLivro();
        }

        private void CarregarDadosLivro()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livro = context.Livros.Find(_livroId);
                    if (livro != null)
                    {
                        txtTitulo.Text = livro.Titulo;
                        txtAutor.Text = livro.Autor;
                        cmbGenero.Text = livro.Genero;
                        txtDescricao.Text = livro.Descricao;
                        checkBoxLido.Checked = livro.Lido;
                        checkBoxFavorito.Checked = livro.Favorito;

                        // Carregar imagem se existir
                        if (livro.Capa != null && livro.Capa.Length > 0)
                        {
                            using (var ms = new System.IO.MemoryStream(livro.Capa))
                            {
                                pictureBoxCapa.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados do livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    Livro livro;

                    if (_modoEdicao)
                    {
                        livro = context.Livros.Find(_livroId);
                        if (livro == null)
                        {
                            MessageBox.Show("Livro não encontrado!", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Verificar permissão
                        if (livro.UsuarioId != AppManager.UsuarioLogado.UsuarioId)
                        {
                            MessageBox.Show("Você não tem permissão para editar este livro.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        livro = new Livro
                        {
                            UsuarioId = AppManager.UsuarioLogado.UsuarioId,
                            DataAdicao = DateTime.Now
                        };
                        context.Livros.Add(livro);
                    }

                    // Atualizar dados
                    livro.Titulo = txtTitulo.Text.Trim();
                    livro.Autor = txtAutor.Text.Trim();
                    livro.Genero = cmbGenero.Text;
                    livro.Descricao = txtDescricao.Text.Trim();
                    livro.Lido = checkBoxLido.Checked;
                    livro.Favorito = checkBoxFavorito.Checked;

                    // Salvar imagem
                    if (pictureBoxCapa.Image != null)
                    {
                        using (var ms = new System.IO.MemoryStream())
                        {
                            pictureBoxCapa.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            livro.Capa = ms.ToArray();
                        }
                    }

                    context.SaveChanges();

                    MessageBox.Show(_modoEdicao ? "Livro atualizado!" : "Livro adicionado!",
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("Por favor, informe o título do livro.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAutor.Text))
            {
                MessageBox.Show("Por favor, informe o autor do livro.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAutor.Focus();
                return false;
            }

            return true;
        }

        private void btnSelecionarImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Selecionar Capa do Livro";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxCapa.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao carregar imagem: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        public static class DatabaseHelper
        {
            public static void VerificarEstruturaBanco()
            {
                try
                {
                    using (var context = new ApplicationDbContext())
                    {
                        // Tenta uma operação simples para verificar se a estrutura está OK
                        var count = context.Livros.Count();
                        Console.WriteLine("Estrutura do banco verificada com sucesso.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Problema na estrutura do banco: {ex.Message}\n\nExecute o script SQL de correção.",
                        "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        




    }
}