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

        // Classe helper para os ComboBoxes
        private class LivroComboItem
        {
            public int LivroId { get; set; }
            public string Titulo { get; set; }

            public override string ToString()
            {
                return Titulo;
            }
        }

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

                    // Cria lista com item vazio usando a classe helper
                    var livrosComVazio = new System.Collections.Generic.List<LivroComboItem>();
                    livrosComVazio.Add(new LivroComboItem { LivroId = 0, Titulo = "-- Selecione um livro --" });

                    foreach (var livro in livros)
                    {
                        livrosComVazio.Add(new LivroComboItem { LivroId = livro.LivroId, Titulo = livro.Titulo });
                    }

                    // Configura os ComboBoxes - cada um recebe sua própria lista
                    ConfigurarComboBox(cmbLivro1, livrosComVazio);
                    ConfigurarComboBox(cmbLivro2, livrosComVazio);
                    ConfigurarComboBox(cmbLivro3, livrosComVazio);
                    ConfigurarComboBox(cmbLivro4, livrosComVazio);
                    ConfigurarComboBox(cmbLivro5, livrosComVazio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarComboBox(ComboBox combo, System.Collections.Generic.List<LivroComboItem> livros)
        {
            // Cria uma nova lista para cada ComboBox
            var novaLista = new System.Collections.Generic.List<LivroComboItem>();
            foreach (var livro in livros)
            {
                novaLista.Add(new LivroComboItem { LivroId = livro.LivroId, Titulo = livro.Titulo });
            }

            combo.DataSource = novaLista;
            combo.DisplayMember = "Titulo";
            combo.ValueMember = "LivroId";
            combo.SelectedIndex = 0;
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
            var livroId = ObterLivroIdSelecionado(cmbLivro1);
            if (livroId.HasValue && livroId.Value > 0)
                CarregarCapaLivro(picLivro1, livroId.Value);
            else
            {
                picLivro1.Image = null;
                picLivro1.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var livroId = ObterLivroIdSelecionado(cmbLivro2);
            if (livroId.HasValue && livroId.Value > 0)
                CarregarCapaLivro(picLivro2, livroId.Value);
            else
            {
                picLivro2.Image = null;
                picLivro2.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var livroId = ObterLivroIdSelecionado(cmbLivro3);
            if (livroId.HasValue && livroId.Value > 0)
                CarregarCapaLivro(picLivro3, livroId.Value);
            else
            {
                picLivro3.Image = null;
                picLivro3.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var livroId = ObterLivroIdSelecionado(cmbLivro4);
            if (livroId.HasValue && livroId.Value > 0)
                CarregarCapaLivro(picLivro4, livroId.Value);
            else
            {
                picLivro4.Image = null;
                picLivro4.BackColor = Color.LightGray;
            }
        }

        private void cmbLivro5_SelectedIndexChanged(object sender, EventArgs e)
        {
            var livroId = ObterLivroIdSelecionado(cmbLivro5);
            if (livroId.HasValue && livroId.Value > 0)
                CarregarCapaLivro(picLivro5, livroId.Value);
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

                    // Atualiza dados
                    tier.NomeTier = txtNomeTier.Text.Trim();
                    tier.LivroId1 = ObterLivroIdSelecionado(cmbLivro1);
                    tier.LivroId2 = ObterLivroIdSelecionado(cmbLivro2);
                    tier.LivroId3 = ObterLivroIdSelecionado(cmbLivro3);
                    tier.LivroId4 = ObterLivroIdSelecionado(cmbLivro4);
                    tier.LivroId5 = ObterLivroIdSelecionado(cmbLivro5);

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

        // Método helper corrigido para obter LivroId do ComboBox
        private int? ObterLivroIdSelecionado(ComboBox combo)
        {
            if (combo.SelectedItem == null)
                return null;

            var item = combo.SelectedItem as LivroComboItem;
            if (item == null || item.LivroId == 0)
                return null;

            return item.LivroId;
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
            bool temLivro = ObterLivroIdSelecionado(cmbLivro1).HasValue ||
                           ObterLivroIdSelecionado(cmbLivro2).HasValue ||
                           ObterLivroIdSelecionado(cmbLivro3).HasValue ||
                           ObterLivroIdSelecionado(cmbLivro4).HasValue ||
                           ObterLivroIdSelecionado(cmbLivro5).HasValue;

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