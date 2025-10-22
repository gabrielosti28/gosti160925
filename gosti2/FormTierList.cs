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
    public partial class FormTierList : Form
    {
        private CategoriaTier tierSelecionada;

        public FormTierList()
        {
            InitializeComponent();

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CarregarTierLists();
        }

        private void CarregarTierLists()
        {
            try
            {
                flowLayoutPanelTiers.Controls.Clear();

                using (var db = new ApplicationDbContext())
                {
                    var tiers = db.Set<CategoriaTier>()
                        .Include(t => t.Usuario)
                        .Include(t => t.Livro1)
                        .Include(t => t.Livro2)
                        .Include(t => t.Livro3)
                        .Include(t => t.Livro4)
                        .Include(t => t.Livro5)
                        .OrderByDescending(t => t.DataCriacao)
                        .ToList();

                    if (tiers.Count == 0)
                    {
                        var lblVazio = new Label
                        {
                            Text = "📋 Nenhuma tier list criada ainda.\nClique em 'Criar Tier List' para começar!",
                            Font = new Font("Segoe UI", 12, FontStyle.Italic),
                            ForeColor = Color.Gray,
                            AutoSize = true,
                            Margin = new Padding(20)
                        };
                        flowLayoutPanelTiers.Controls.Add(lblVazio);
                        return;
                    }

                    foreach (var tier in tiers)
                    {
                        var panelTier = CriarPanelTier(tier);
                        flowLayoutPanelTiers.Controls.Add(panelTier);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tier lists: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CriarPanelTier(CategoriaTier tier)
        {
            var panel = new Panel
            {
                Width = flowLayoutPanelTiers.Width - 60,
                Height = 200,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(10),
                Tag = tier
            };

            panel.Click += (s, e) => SelecionarTier(tier);

            // Foto do usuário
            var picUsuario = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            if (tier.Usuario.FotoPerfil != null && tier.Usuario.FotoPerfil.Length > 0)
            {
                using (var ms = new MemoryStream(tier.Usuario.FotoPerfil))
                {
                    picUsuario.Image = Image.FromStream(ms);
                }
            }

            panel.Controls.Add(picUsuario);

            // Nome do usuário e data
            var lblUsuario = new Label
            {
                Text = tier.Usuario.NomeUsuario,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(70, 10),
                AutoSize = true
            };
            panel.Controls.Add(lblUsuario);

            var lblData = new Label
            {
                Text = tier.DataCriacao.ToString("dd/MM/yyyy HH:mm"),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(70, 35),
                AutoSize = true
            };
            panel.Controls.Add(lblData);

            // Nome da tier list
            var lblNomeTier = new Label
            {
                Text = tier.NomeTier,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 130, 180),
                Location = new Point(10, 70),
                AutoSize = true
            };
            panel.Controls.Add(lblNomeTier);

            // Livros (5 posições)
            int x = 10;
            AdicionarLivroPanel(panel, tier.Livro1, x, 100, "1º");
            AdicionarLivroPanel(panel, tier.Livro2, x + 120, 100, "2º");
            AdicionarLivroPanel(panel, tier.Livro3, x + 240, 100, "3º");
            AdicionarLivroPanel(panel, tier.Livro4, x + 360, 100, "4º");
            AdicionarLivroPanel(panel, tier.Livro5, x + 480, 100, "5º");

            return panel;
        }

        private void AdicionarLivroPanel(Panel panelPai, Livro livro, int x, int y, string posicao)
        {
            var panelLivro = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(100, 90),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                Cursor = Cursors.Hand
            };

            var lblPosicao = new Label
            {
                Text = posicao,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(5, 5),
                AutoSize = true
            };
            panelLivro.Controls.Add(lblPosicao);

            if (livro != null)
            {
                var picCapa = new PictureBox
                {
                    Location = new Point(10, 20),
                    Size = new Size(80, 65),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.LightGray
                };

                if (livro.Capa != null && livro.Capa.Length > 0)
                {
                    using (var ms = new MemoryStream(livro.Capa))
                    {
                        picCapa.Image = Image.FromStream(ms);
                    }
                }

                panelLivro.Controls.Add(picCapa);

                panelLivro.Click += (s, e) =>
                {
                    using (var formLivro = new FormLivroAberto(livro.LivroId, AppManager.UsuarioLogado.UsuarioId))
                    {
                        formLivro.ShowDialog();
                    }
                };
            }
            else
            {
                var lblVazio = new Label
                {
                    Text = "Vazio",
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(30, 40),
                    AutoSize = true
                };
                panelLivro.Controls.Add(lblVazio);
            }

            panelPai.Controls.Add(panelLivro);
        }

        private void SelecionarTier(CategoriaTier tier)
        {
            tierSelecionada = tier;

            // Habilita editar/remover apenas se for do usuário logado
            bool ehMinhaTier = (tier.UsuarioId == AppManager.UsuarioLogado.UsuarioId);
            btnEditar.Enabled = ehMinhaTier;
            btnRemover.Enabled = ehMinhaTier;

            // Destaca o painel selecionado
            foreach (Control ctrl in flowLayoutPanelTiers.Controls)
            {
                if (ctrl is Panel panel)
                {
                    panel.BackColor = (panel.Tag == tier) ? Color.LightCyan : Color.White;
                }
            }
        }

        private void btnCriarTier_Click(object sender, EventArgs e)
        {
            using (var formCriar = new FormCriacaoTier())
            { 
                if (formCriar.ShowDialog() == DialogResult.OK)
                {
                    CarregarTierLists();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tierSelecionada == null)
            {
                MessageBox.Show("Selecione uma tier list para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var formEditar = new FormCriacaoTier(tierSelecionada.CategoriaTierId))
            {
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    CarregarTierLists();
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (tierSelecionada == null)
            {
                MessageBox.Show("Selecione uma tier list para remover.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Deseja realmente remover a tier list '{tierSelecionada.NomeTier}'?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var tier = db.Set<CategoriaTier>().Find(tierSelecionada.CategoriaTierId);
                        if (tier != null)
                        {
                            db.Set<CategoriaTier>().Remove(tier);
                            db.SaveChanges();

                            MessageBox.Show("Tier list removida com sucesso!", "Sucesso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            tierSelecionada = null;
                            CarregarTierLists();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao remover tier list: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarTierLists();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}