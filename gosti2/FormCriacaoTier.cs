using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormCriacaoTier : Form
    {
        private int? _tierIdEdicao = null;
        private bool _modoEdicao = false;

        // Construtor para CRIAR nova tier list
        public FormCriacaoTier()
        {
            InitializeComponent();
            _modoEdicao = false;
            lblTitulo.Text = "⭐ Criar Tier List";
            CarregarLivros();
        }

        // Construtor para EDITAR tier list existente
        public FormCriacaoTier(int tierListId)
        {
            InitializeComponent();
            _tierIdEdicao = tierListId;
            _modoEdicao = true;
            lblTitulo.Text = "⭐ Editar Tier List";
            CarregarLivros();
            CarregarDadosTier();
        }

        private void CarregarLivros()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livros = db.Livros
                        .Where(l => l.UsuarioId == AppManager.UsuarioLogado.UsuarioId)
                        .OrderBy(l => l.Titulo)
                        .ToList();

                    if (livros.Count == 0)
                    {
                        MessageBox.Show("Você não possui livros cadastrados.\nAdicione livros antes de criar uma tier list.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }

                    // Cria lista com item vazio
                    var livrosComVazio = new System.Collections.Generic.List<dynamic>();
                    livrosComVazio.Add(new { LivroId = 0, Titulo = "-- Selecione um livro --" });
                    livrosComVazio.AddRange(livros.Select(l => new { l.LivroId, l.Titulo }));

                    // Configura os ComboBoxes - cria nova lista para cada um
                    cmbLivro1.DataSource = livrosComVazio.ToList();
                    cmbLivro1.DisplayMember = "Titulo";
                    cmbLivro1.ValueMember = "LivroId";
                    cmbLivro1.SelectedIndex = 0;

                    cmbLivro2.DataSource = new System.Collections.Generic.List<dynamic>(livrosComVazio);
                    cmbLivro2.DisplayMember = "Titulo";
                    cmbLivro2.ValueMember = "LivroId";
                    cmbLivro2.SelectedIndex = 0;

                    cmbLivro3.DataSource = new System.Collections.Generic.List<dynamic>(livrosComVazio);
                    cmbLivro3.DisplayMember = "Titulo";
                    cmbLivro3.ValueMember = "LivroId";
                    cmbLivro3.SelectedIndex = 0;

                    cmbLivro4.DataSource = new System.Collections.Generic.List<dynamic>(livrosComVazio);
                    cmbLivro4.DisplayMember = "Titulo";
                    cmbLivro4.ValueMember = "LivroId";
                    cmbLivro4.SelectedIndex = 0;

                    cmbLivro5.DataSource = new System.Collections.Generic.List<dynamic>(livrosComVazio);
                    cmbLivro5.DisplayMember = "Titulo";
                    cmbLivro5.ValueMember = "LivroId";
                    cmbLivro5.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarDadosTier()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var tier = db.Set<CategoriaTier>()
                        .Include(t => t.Livro1)
                        .Include(t => t.Livro2)
                        .Include(t => t.Livro3)
                        .Include(t => t.Livro4)
                        .Include(t => t.Livro5)
                        .FirstOrDefault(t => t.CategoriaTierId == _tierIdEdicao);

                    if (tier == null)
                    {
                        MessageBox.Show("Tier list não encontrada.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // Verifica permissão
                    if (tier.UsuarioId != AppManager.UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você não tem permissão para editar esta tier list.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }

                    // Carrega os dados
                    txtNomeTier.Text = tier.NomeTier;

                    if (tier.LivroId1.HasValue)
                    {
                        cmbLivro1.SelectedValue = tier.LivroId1.Value;
                        CarregarCapaLivro(picLivro1, tier.LivroId1.Value);
                    }

                    if (tier.LivroId2.HasValue)
                    {
                        cmbLivro2.SelectedValue = tier.LivroId2.Value;
                        CarregarCapaLivro(picLivro2, tier.LivroId2.Value);
                    }

                    if (tier.LivroId3.HasValue)
                    {
                        cmbLivro3.SelectedValue = tier.LivroId3.Value;
                        CarregarCapaLivro(picLivro3, tier.LivroId3.Value);
                    }

                    if (tier.LivroId4.HasValue)
                    {
                        cmbLivro4.SelectedValue = tier.LivroId4.Value;
                        CarregarCapaLivro(picLivro4, tier.LivroId4.Value);
                    }

                    if (tier.LivroId5.HasValue)
                    {
                        cmbLivro5.SelectedValue = tier.LivroId5.Value;
                        CarregarCapaLivro(picLivro5, tier.LivroId5.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tier list: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarCapaLivro(PictureBox pictureBox, int livroId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livro = db.Livros.Find(livroId);
                    if (livro != null && livro.Capa != null && livro.Capa.Length > 0)
                    {
                        using (var ms = new MemoryStream(livro.Capa))
                        {
                            pictureBox.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBox.Image = null;
                        pictureBox.BackColor = Color.LightGray;
                    }
                }
            }
            catch
            {
                pictureBox.Image = null;
                pictureBox.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLivro1.SelectedValue != null && (int)cmbLivro1.SelectedValue > 0)
                CarregarCapaLivro(picLivro1, (int)cmbLivro1.SelectedValue);
            else
            {
                picLivro1.Image = null;
                picLivro1.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLivro2.SelectedValue != null && (int)cmbLivro2.SelectedValue > 0)
                CarregarCapaLivro(picLivro2, (int)cmbLivro2.SelectedValue);
            else
            {
                picLivro2.Image = null;
                picLivro2.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLivro3.SelectedValue != null && (int)cmbLivro3.SelectedValue > 0)
                CarregarCapaLivro(picLivro3, (int)cmbLivro3.SelectedValue);
            else
            {
                picLivro3.Image = null;
                picLivro3.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLivro4.SelectedValue != null && (int)cmbLivro4.SelectedValue > 0)
                CarregarCapaLivro(picLivro4, (int)cmbLivro4.SelectedValue);
            else
            {
                picLivro4.Image = null;
                picLivro4.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLivro5.SelectedValue != null && (int)cmbLivro5.SelectedValue > 0)
                CarregarCapaLivro(picLivro5, (int)cmbLivro5.SelectedValue);
            else
            {
                picLivro5.Image = null;
                picLivro5.BackColor = Color.LightGray;
            }
        }

        private void btnRemover1_Click(object sender, EventArgs e)
        {
            cmbLivro1.SelectedIndex = 0;
            picLivro1.Image = null;
            picLivro1.BackColor = Color.LightGray;
        }

        private void btnRemover2_Click(object sender, EventArgs e)
        {
            cmbLivro2.SelectedIndex = 0;
            picLivro2.Image = null;
            picLivro2.BackColor = Color.LightGray;
        }

        private void btnRemover3_Click(object sender, EventArgs e)
        {
            cmbLivro3.SelectedIndex = 0;
            picLivro3.Image = null;
            picLivro3.BackColor = Color.LightGray;
        }

        private void btnRemover4_Click(object sender, EventArgs e)
        {
            cmbLivro4.SelectedIndex = 0;
            picLivro4.Image = null;
            picLivro4.BackColor = Color.LightGray;
        }

        private void btnRemover5_Click(object sender, EventArgs e)
        {
            cmbLivro5.SelectedIndex = 0;
            picLivro5.Image = null;
            picLivro5.BackColor = Color.LightGray;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    CategoriaTier tier;

                    if (_modoEdicao && _tierIdEdicao.HasValue)
                    {
                        // EDIÇÃO
                        tier = db.Set<CategoriaTier>().Find(_tierIdEdicao.Value);
                        if (tier == null)
                        {
                            MessageBox.Show("Tier list não encontrada.", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        // CRIAÇÃO
                        tier = new CategoriaTier
                        {
                            UsuarioId = AppManager.UsuarioLogado.UsuarioId,
                            DataCriacao = DateTime.Now
                        };
                        db.Set<CategoriaTier>().Add(tier);
                    }

                    // Atualiza dados - converte para int de forma segura
                    tier.NomeTier = txtNomeTier.Text.Trim();
                    tier.LivroId1 = ObterLivroId(cmbLivro1);
                    tier.LivroId2 = ObterLivroId(cmbLivro2);
                    tier.LivroId3 = ObterLivroId(cmbLivro3);
                    tier.LivroId4 = ObterLivroId(cmbLivro4);
                    tier.LivroId5 = ObterLivroId(cmbLivro5);

                    db.SaveChanges();

                    MessageBox.Show(_modoEdicao ? "Tier list atualizada com sucesso!" : "Tier list publicada com sucesso!",
                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar tier list: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adicione este método helper
        private int? ObterLivroId(ComboBox combo)
        {
            if (combo.SelectedValue == null)
                return null;

            int livroId = Convert.ToInt32(combo.SelectedValue);
            return livroId > 0 ? (int?)livroId : null;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomeTier.Text))
            {
                MessageBox.Show("Por favor, informe o nome da tier list.", "Campo Obrigatório",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomeTier.Focus();
                return false;
            }

            // Verifica se pelo menos um livro foi selecionado
            bool temLivro = (cmbLivro1.SelectedValue != null && (int)cmbLivro1.SelectedValue > 0) ||
                           (cmbLivro2.SelectedValue != null && (int)cmbLivro2.SelectedValue > 0) ||
                           (cmbLivro3.SelectedValue != null && (int)cmbLivro3.SelectedValue > 0) ||
                           (cmbLivro4.SelectedValue != null && (int)cmbLivro4.SelectedValue > 0) ||
                           (cmbLivro5.SelectedValue != null && (int)cmbLivro5.SelectedValue > 0);

            if (!temLivro)
            {
                MessageBox.Show("Selecione pelo menos um livro para a tier list.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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