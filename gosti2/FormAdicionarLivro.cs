using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Data.Entity;
using gosti2.Data;
using gosti2.Models;
namespace gosti2
{
    public partial class FormAdicionarLivro : Form
    {
        private int _livroId;
        private bool _modoEdicao = false;

        // Construtor para ADICIONAR novo livro
        public FormAdicionarLivro()
        {
            InitializeComponent();
            _modoEdicao = false;
            this.Text = "Adicionar Novo Livro";
            lblTitulo.Text = "Adicionar Novo Livro";
        }

        // Construtor para EDITAR livro existente
        public FormAdicionarLivro(int livroId)
        {
            InitializeComponent();
            _livroId = livroId;
            _modoEdicao = true;
            this.Text = "Editar Livro";
            lblTitulo.Text = "Editar Livro";
            CarregarDadosLivro();
        }

        private void CarregarDadosLivro()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var livro = context.Livros.Find(_livroId);

                    if (livro == null)
                    {
                        MessageBox.Show("Livro não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // Verificar permissão
                    if (livro.UsuarioId != AppManager.UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você não tem permissão para editar este livro.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }

                    // Carregar dados nos campos
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
                            pictureBoxCapa.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados do livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            this.Cursor = Cursors.WaitCursor;
            btnSalvar.Enabled = false;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    Livro livro;

                    if (_modoEdicao)
                    {
                        // EDIÇÃO
                        livro = context.Livros.Find(_livroId);

                        if (livro == null)
                        {
                            MessageBox.Show("Livro não encontrado!", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Verificar permissão novamente
                        if (livro.UsuarioId != AppManager.UsuarioLogado.UsuarioId)
                        {
                            MessageBox.Show("Você não tem permissão para editar este livro.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        // NOVO LIVRO
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
                    livro.Genero = cmbGenero.Text.Trim();
                    livro.Descricao = txtDescricao.Text.Trim();
                    livro.Lido = checkBoxLido.Checked;
                    livro.Favorito = checkBoxFavorito.Checked;

                    // Salvar imagem se houver
                    if (pictureBoxCapa.Image != null)
                    {
                        using (var ms = new System.IO.MemoryStream())
                        {
                            pictureBoxCapa.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            livro.Capa = ms.ToArray();
                        }
                    }

                    context.SaveChanges();

                    MessageBox.Show(
                        _modoEdicao ? "Livro atualizado com sucesso!" : "Livro adicionado com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnSalvar.Enabled = true;
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

            if (string.IsNullOrWhiteSpace(cmbGenero.Text))
            {
                MessageBox.Show("Por favor, selecione ou informe o gênero do livro.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGenero.Focus();
                return false;
            }

            return true;
        }

        private void btnSelecionarImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Selecionar Capa do Livro";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxCapa.Image = Image.FromFile(openFileDialog.FileName);
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
            if (MessageBox.Show("Deseja cancelar? As alterações não serão salvas.", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}