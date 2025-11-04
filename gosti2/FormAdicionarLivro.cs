using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Data.Entity;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormAdicionarLivro : Form
    {
        private int _livroId;
        private bool _modoEdicao = false;
        private byte[] _imagemBytes = null; // NOVO: Armazena os bytes da imagem

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

                    // CORRIGIDO: Armazena os bytes e carrega a imagem de forma segura
                    if (livro.Capa != null && livro.Capa.Length > 0)
                    {
                        _imagemBytes = livro.Capa; // Guarda os bytes originais

                        using (var ms = new MemoryStream(livro.Capa))
                        {
                            // Cria uma cópia independente da imagem
                            Image imagemOriginal = Image.FromStream(ms);
                            pictureBoxCapa.Image = new Bitmap(imagemOriginal);
                            imagemOriginal.Dispose(); // Libera a imagem original
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

                    // CORRIGIDO: Salvar imagem de forma segura
                    if (_imagemBytes != null)
                    {
                        // Usa os bytes armazenados (seja da nova imagem ou da original)
                        livro.Capa = _imagemBytes;
                    }
                    else if (pictureBoxCapa.Image == null)
                    {
                        // Se não há imagem, define como null
                        livro.Capa = null;
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
                        // Libera a imagem anterior se existir
                        if (pictureBoxCapa.Image != null)
                        {
                            pictureBoxCapa.Image.Dispose();
                            pictureBoxCapa.Image = null;
                        }

                        // Carrega a nova imagem e converte para bytes
                        using (Image imagemOriginal = Image.FromFile(openFileDialog.FileName))
                        {
                            // Exibe no PictureBox (cria uma cópia)
                            pictureBoxCapa.Image = new Bitmap(imagemOriginal);

                            // Converte para bytes para salvar no banco
                            using (var ms = new MemoryStream())
                            {
                                imagemOriginal.Save(ms, ImageFormat.Jpeg);
                                _imagemBytes = ms.ToArray(); // ARMAZENA OS BYTES
                            }
                        }
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

        // Libera recursos ao fechar o formulário
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (pictureBoxCapa.Image != null)
            {
                pictureBoxCapa.Image.Dispose();
                pictureBoxCapa.Image = null;
            }
            base.OnFormClosing(e);
        }
    }
}