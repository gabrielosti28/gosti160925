using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using gosti2.Models;
using gosti2.Data;
using gosti2.Tools;
using System.Collections.Generic;

namespace gosti2
{
    public partial class FormLivroAberto : Form
    {
        private int _livroId;
        private Livro _livro;
        private int _usuarioLogadoId;

        public FormLivroAberto(int livroId, int usuarioLogadoId = 0)
        {
            InitializeComponent();
            _livroId = livroId;
            _usuarioLogadoId = usuarioLogadoId;

            // ✅ CONFIGURAÇÃO DO DIAGNOSTIC CONTEXT
            DiagnosticContext.FormularioAtual = "FormLivroAberto";
            DiagnosticContext.MetodoAtual = "Constructor";
            DiagnosticContext.UsuarioId = usuarioLogadoId;

            DiagnosticContext.LogarInfo($"Abrindo FormLivroAberto para livro ID: {livroId}");

            ConfigureUI();
            CarregarLivro();
            CarregarComentarios();
            CarregarLikesDislikes();
            VerificarVotoUsuario();
        }

        private void ConfigureUI()
        {
            // ✅ CONFIGURAÇÕES DE ESTILO
            flowLayoutPanelComentarios.AutoScroll = true;
            flowLayoutPanelComentarios.WrapContents = false;

            // ✅ CONFIGURAR BOTÕES COM CORES MODERNAS
            btnLikeLivro.BackColor = Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            btnDislikeLivro.BackColor = Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));

            // ✅ EVENTO DE REDIMENSIONAMENTO
            this.Resize += (s, e) => AjustarTamanhoPainelComentarios();

            // ✅ CONFIGURAR PLACEHOLDER
            txtNovoComentario.Text = "Digite seu comentário aqui... (mínimo 5 caracteres)";
            txtNovoComentario.ForeColor = Color.Gray;
            txtNovoComentario.Enter += (s, e) =>
            {
                if (txtNovoComentario.Text == "Digite seu comentário aqui... (mínimo 5 caracteres)")
                {
                    txtNovoComentario.Text = "";
                    txtNovoComentario.ForeColor = Color.Black;
                }
            };
            txtNovoComentario.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNovoComentario.Text))
                {
                    txtNovoComentario.Text = "Digite seu comentário aqui... (mínimo 5 caracteres)";
                    txtNovoComentario.ForeColor = Color.Gray;
                }
            };
        }

        private void AjustarTamanhoPainelComentarios()
        {
            foreach (Control control in flowLayoutPanelComentarios.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = flowLayoutPanelComentarios.ClientSize.Width - 25;
                }
            }
        }

        private void CarregarLivro()
        {
            try
            {
                DiagnosticContext.LogarInfo($"Carregando livro ID: {_livroId}");

                using (var context = new ApplicationDbContext())
                {
                    _livro = context.Livros
                        .Include(l => l.Usuario)
                        .FirstOrDefault(l => l.LivroId == _livroId);

                    if (_livro != null)
                    {
                        lblTitulo.Text = _livro.Titulo;
                        lblAutor.Text = $"Autor: {_livro.Autor}";
                        lblGenero.Text = $"Gênero: {_livro.Genero}";
                        lblAdicionadoPor.Text = $"Adicionado por: {_livro.Usuario?.NomeUsuario ?? "Usuário desconhecido"}";
                        txtDescricao.Text = _livro.Descricao ?? "Sem descrição";

                        CarregarCapaLivro(_livro.Capa);
                        ExibirInformacoesAdicionais(_livro);

                        DiagnosticContext.LogarInfo($"Livro carregado: {_livro.Titulo}");
                    }
                    else
                    {
                        DiagnosticContext.LogarErro($"Livro não encontrado: ID {_livroId}",
                            new Exception("Livro não encontrado no banco"));
                        MessageBox.Show("Livro não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao carregar livro ID: {_livroId}", ex);
                MessageBox.Show($"Erro ao carregar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarCapaLivro(byte[] capa)
        {
            try
            {
                if (capa != null && capa.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream(capa))
                    {
                        if (pictureBoxCapa.Image != null)
                        {
                            var oldImage = pictureBoxCapa.Image;
                            pictureBoxCapa.Image = null;
                            oldImage.Dispose();
                        }

                        pictureBoxCapa.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxCapa.Image = CreateDefaultBookCover();
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro ao carregar capa do livro", ex);
                pictureBoxCapa.Image = CreateDefaultBookCover();
            }
        }

        private Bitmap CreateDefaultBookCover()
        {
            var bmp = new Bitmap(394, 120);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180))))));
                using (var font = new Font("Arial", 24, FontStyle.Bold))
                {
                    g.DrawString("📚", font, Brushes.White, new PointF(160, 30));
                }
                g.DrawString("Sem Capa", new Font("Arial", 12), Brushes.White, new PointF(140, 80));
            }
            return bmp;
        }

        private void ExibirInformacoesAdicionais(Livro livro)
        {
            var infoAdicional = new System.Text.StringBuilder();

            if (livro.AnoPublicacao.HasValue)
                infoAdicional.AppendLine($"📅 Ano: {livro.AnoPublicacao}");

            if (!string.IsNullOrEmpty(livro.ISBN))
                infoAdicional.AppendLine($"🏷️ ISBN: {livro.ISBN}");

            if (!string.IsNullOrEmpty(livro.Editora))
                infoAdicional.AppendLine($"🏢 Editora: {livro.Editora}");

            if (livro.Paginas.HasValue)
                infoAdicional.AppendLine($"📖 Páginas: {livro.Paginas}");

            // ✅ CORREÇÃO: Removido CategoriaTierId pois não existe na classe Livro
            // Se você quiser adicionar essa funcionalidade, precisa primeiro atualizar a classe Livro

            if (infoAdicional.Length > 0)
            {
                lblInfoAdicional.Text = infoAdicional.ToString();
                lblInfoAdicional.Visible = true;
            }
        }

        private void CarregarComentarios()
        {
            try
            {
                DiagnosticContext.LogarInfo($"Carregando comentários para livro ID: {_livroId}");
                flowLayoutPanelComentarios.Controls.Clear();

                using (var context = new ApplicationDbContext())
                {
                    var comentarios = context.Comentarios
                        .Include(c => c.Usuario)
                        .Where(c => c.LivroId == _livroId)
                        .OrderByDescending(c => c.DataComentario)
                        .ToList();

                    // ✅ ATUALIZAR ESTATÍSTICAS
                    AtualizarEstatisticasComentarios(comentarios);

                    if (comentarios.Any())
                    {
                        foreach (var comentario in comentarios)
                        {
                            AdicionarComentarioPanel(comentario);
                        }
                        DiagnosticContext.LogarInfo($"{comentarios.Count} comentários carregados");
                    }
                    else
                    {
                        var lblSemComentarios = new Label
                        {
                            Text = "📝 Seja o primeiro a comentar sobre este livro!",
                            Font = new Font("Segoe UI", 10, FontStyle.Italic),
                            ForeColor = Color.Gray,
                            AutoSize = true,
                            Margin = new Padding(10)
                        };
                        flowLayoutPanelComentarios.Controls.Add(lblSemComentarios);
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao carregar comentários para livro ID: {_livroId}", ex);
                MessageBox.Show($"Erro ao carregar comentários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarEstatisticasComentarios(List<Comentario> comentarios)
        {
            try
            {
                lblTotalComentarios.Text = comentarios.Count.ToString();

                var comentariosPopulares = comentarios.Count(c => c.EhPopular);
                var comentariosPolemicos = comentarios.Count(c => c.EhPolemico);

                lblComentariosPopulares.Text = $"🔥 {comentariosPopulares} populares";
                lblComentariosPolemicos.Text = $"⚡ {comentariosPolemicos} polêmicos";

                // ✅ ATUALIZAR ENGAGEMENT TOTAL
                var totalLikes = comentarios.Sum(c => c.Likes);
                var totalDislikes = comentarios.Sum(c => c.Dislikes);
                var pontuacaoTotal = totalLikes - totalDislikes;
                var taxaAprovacao = totalLikes + totalDislikes > 0 ?
                    (double)totalLikes / (totalLikes + totalDislikes) * 100 : 0;

                lblPontuacaoTotal.Text = $"Pontuação: {pontuacaoTotal}";
                lblTaxaAprovacao.Text = $"Aprovação: {taxaAprovacao:0}%";
                progressBarAprovacao.Value = (int)Math.Min(taxaAprovacao, 100);
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro ao atualizar estatísticas de comentários", ex);
            }
        }

        private void AdicionarComentarioPanel(Comentario comentario)
        {
            var panel = new Panel
            {
                Width = flowLayoutPanelComentarios.Width - 25,
                Height = 180, // ✅ AUMENTADO PARA NOVAS INFORMAÇÕES
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // ✅ CABEÇALHO DO COMENTÁRIO
            var lblUsuario = new Label
            {
                Text = comentario.Usuario?.NomeUsuario ?? "Usuário desconhecido",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                ForeColor = Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))))
            };

            // ✅ STATUS DO COMENTÁRIO (NOVO)
            var lblStatus = new Label
            {
                Text = comentario.Status,
                Font = new Font("Segoe UI", 7, FontStyle.Bold),
                Location = new Point(lblUsuario.Right + 10, 12),
                AutoSize = true,
                ForeColor = ObterCorStatus(comentario.Status)
            };

            var lblData = new Label
            {
                Text = comentario.ObterTempoRelativo(),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(panel.Width - 120, 10),
                AutoSize = true
            };

            // ✅ INFORMAÇÃO DE EDIÇÃO (SE APLICÁVEL)
            Label lblEditado = null;
            if (comentario.Editado && comentario.DataEdicao.HasValue)
            {
                lblEditado = new Label
                {
                    Text = $"✏️ Editado {comentario.DataEdicao.Value:dd/MM HH:mm}",
                    Font = new Font("Segoe UI", 7),
                    ForeColor = Color.DarkGray,
                    Location = new Point(10, 30),
                    AutoSize = true
                };
            }

            // ✅ TEXTO DO COMENTÁRIO
            var txtComentario = new TextBox
            {
                Text = comentario.Texto,
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Location = new Point(10, lblEditado != null ? 50 : 35),
                Size = new Size(panel.Width - 30, 60),
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9),
                ScrollBars = ScrollBars.Vertical
            };

            // ✅ PAINEL DE INTERAÇÕES (MELHORADO)
            var panelInteracoes = new Panel
            {
                Location = new Point(10, 120),
                Size = new Size(panel.Width - 20, 50)
            };

            // ✅ BOTÕES DE LIKE/DISLIKE
            var btnLike = new Button
            {
                Text = $"👍 {comentario.Likes}",
                Tag = comentario.ComentarioId,
                Size = new Size(80, 30),
                Location = new Point(0, 10),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = comentario.Likes > 0 ? Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(240)))), ((int)(((byte)(222))))) : Color.White,
                ForeColor = comentario.Likes > 0 ? Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(87)))), ((int)(((byte)(36))))) : Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLike.Click += BtnLikeComentario_Click;

            var btnDislike = new Button
            {
                Text = $"👎 {comentario.Dislikes}",
                Tag = comentario.ComentarioId,
                Size = new Size(80, 30),
                Location = new Point(90, 10),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = comentario.Dislikes > 0 ? Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(236))))) : Color.White,
                ForeColor = comentario.Dislikes > 0 ? Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69))))) : Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDislike.Click += BtnDislikeComentario_Click;

            // ✅ MÉTRICAS DO COMENTÁRIO (NOVO)
            var lblMetricas = new Label
            {
                Text = comentario.ObterRelatorioDesempenho(),
                Font = new Font("Segoe UI", 7),
                ForeColor = Color.Gray,
                Location = new Point(180, 15),
                AutoSize = true
            };

            // ✅ BOTÕES DE AÇÃO (EDITAR/EXCLUIR)
            if (_usuarioLogadoId == comentario.UsuarioId)
            {
                var btnEditar = new Button
                {
                    Text = "✏️",
                    Tag = comentario.ComentarioId,
                    Size = new Size(30, 25),
                    Location = new Point(panelInteracoes.Width - 70, 12),
                    Font = new Font("Segoe UI", 8),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Enabled = comentario.PodeSerEditado(_usuarioLogadoId)
                };
                btnEditar.Click += BtnEditarComentario_Click;
                panelInteracoes.Controls.Add(btnEditar);

                var btnExcluir = new Button
                {
                    Text = "🗑️",
                    Tag = comentario.ComentarioId,
                    Size = new Size(30, 25),
                    Location = new Point(panelInteracoes.Width - 35, 12),
                    Font = new Font("Segoe UI", 8),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(236))))),
                    Enabled = comentario.PodeSerExcluido(_usuarioLogadoId)
                };
                btnExcluir.Click += BtnExcluirComentario_Click;
                panelInteracoes.Controls.Add(btnExcluir);

                // ✅ TOOLTIPS
                toolTip.SetToolTip(btnEditar, "Editar comentário (30 minutos)");
                toolTip.SetToolTip(btnExcluir, "Excluir comentário (1 hora)");
            }

            panelInteracoes.Controls.Add(btnLike);
            panelInteracoes.Controls.Add(btnDislike);
            panelInteracoes.Controls.Add(lblMetricas);

            // ✅ ADICIONAR CONTROLES AO PAINEL
            panel.Controls.Add(lblUsuario);
            panel.Controls.Add(lblStatus);
            panel.Controls.Add(lblData);
            if (lblEditado != null) panel.Controls.Add(lblEditado);
            panel.Controls.Add(txtComentario);
            panel.Controls.Add(panelInteracoes);

            flowLayoutPanelComentarios.Controls.Add(panel);
        }

        // ✅ CORREÇÃO: Substituído switch expression por método tradicional
        private Color ObterCorStatus(string status)
        {
            if (status == "🔥 Popular")
                return Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            else if (status == "⚡ Polêmico")
                return Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            else if (status == "🆕 Recente")
                return Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            else
                return Color.Gray;
        }

        private void BtnEditarComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0) return;

            var button = (Button)sender;
            int comentarioId = (int)button.Tag;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = context.Comentarios.Find(comentarioId);
                    if (comentario != null && comentario.PodeSerEditado(_usuarioLogadoId))
                    {
                        // ✅ CORREÇÃO: Substituído InputBox por TextBox customizado
                        string novoTexto = MostrarDialogoEdicao(comentario.Texto);

                        if (!string.IsNullOrWhiteSpace(novoTexto) && novoTexto != comentario.Texto)
                        {
                            if (comentario.Editar(novoTexto))
                            {
                                context.SaveChanges();
                                CarregarComentarios();
                                DiagnosticContext.LogarInfo($"Comentário {comentarioId} editado com sucesso");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao editar comentário {comentarioId}", ex);
                MessageBox.Show("Erro ao editar comentário.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CORREÇÃO: Método alternativo para InputBox
        private string MostrarDialogoEdicao(string textoAtual)
        {
            using (var form = new Form())
            {
                form.Text = "Editar Comentário";
                form.Size = new Size(500, 300);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var textBox = new TextBox
                {
                    Multiline = true,
                    Text = textoAtual,
                    Location = new Point(10, 10),
                    Size = new Size(460, 200),
                    ScrollBars = ScrollBars.Vertical
                };

                var btnOk = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(300, 220),
                    Size = new Size(80, 30)
                };

                var btnCancelar = new Button
                {
                    Text = "Cancelar",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(390, 220),
                    Size = new Size(80, 30)
                };

                form.Controls.Add(textBox);
                form.Controls.Add(btnOk);
                form.Controls.Add(btnCancelar);
                form.AcceptButton = btnOk;
                form.CancelButton = btnCancelar;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    return textBox.Text.Trim();
                }
            }
            return null;
        }

        private void BtnExcluirComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0) return;

            var button = (Button)sender;
            int comentarioId = (int)button.Tag;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = context.Comentarios.Find(comentarioId);
                    if (comentario != null && comentario.PodeSerExcluido(_usuarioLogadoId))
                    {
                        var result = MessageBox.Show(
                            "Tem certeza que deseja excluir este comentário?",
                            "Confirmar Exclusão",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            context.Comentarios.Remove(comentario);
                            context.SaveChanges();
                            CarregarComentarios();
                            DiagnosticContext.LogarInfo($"Comentário {comentarioId} excluído com sucesso");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao excluir comentário {comentarioId}", ex);
                MessageBox.Show("Erro ao excluir comentário.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ MÉTODOS RESTANTES (mantidos da versão anterior)
        private void btnComentar_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para comentar.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ VERIFICAR SE É PLACEHOLDER
            if (txtNovoComentario.ForeColor == Color.Gray || string.IsNullOrWhiteSpace(txtNovoComentario.Text) || txtNovoComentario.Text == "Digite seu comentário aqui... (mínimo 5 caracteres)")
            {
                MessageBox.Show("📝 Digite um comentário antes de enviar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNovoComentario.Focus();
                return;
            }

            if (txtNovoComentario.Text.Length > 2000)
            {
                MessageBox.Show("❌ Comentário muito longo. Máximo 2000 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNovoComentario.Text.Length < 5)
            {
                MessageBox.Show("❌ Comentário muito curto. Mínimo 5 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = new Comentario
                    {
                        Texto = txtNovoComentario.Text.Trim(),
                        LivroId = _livroId,
                        UsuarioId = _usuarioLogadoId,
                        DataComentario = DateTime.Now,
                        Likes = 0,
                        Dislikes = 0
                    };

                    context.Comentarios.Add(comentario);
                    context.SaveChanges();

                    txtNovoComentario.Clear();
                    CarregarComentarios();

                    MessageBox.Show("✅ Comentário adicionado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao adicionar comentário para livro {_livroId}", ex);
                MessageBox.Show($"❌ Erro ao adicionar comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLikeLivro_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para curtir.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RegistrarVotoLivro(true);
        }

        private void btnDislikeLivro_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para não curtir.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RegistrarVotoLivro(false);
        }

        private void RegistrarVotoLivro(bool isLike)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var existingVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (existingVote != null)
                    {
                        existingVote.IsLike = isLike;
                        existingVote.DataAcao = DateTime.Now;
                    }
                    else
                    {
                        var vote = new LikeDislike
                        {
                            LivroId = _livroId,
                            UsuarioId = _usuarioLogadoId,
                            IsLike = isLike,
                            DataAcao = DateTime.Now
                        };
                        context.LikesDislikes.Add(vote);
                    }

                    context.SaveChanges();
                    CarregarLikesDislikes();
                    VerificarVotoUsuario();

                    DiagnosticContext.LogarInfo($"Voto registrado para livro {_livroId}: {(isLike ? "Like" : "Dislike")}");
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao registrar voto para livro {_livroId}", ex);
                MessageBox.Show($"❌ Erro ao registrar voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLikeComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para curtir comentários.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var button = (Button)sender;
            int comentarioId = (int)button.Tag;
            RegistrarVotoComentario(comentarioId, true);
        }

        private void BtnDislikeComentario_Click(object sender, EventArgs e)
        {
            if (_usuarioLogadoId == 0)
            {
                MessageBox.Show("❌ Você precisa estar logado para não curtir comentários.", "Acesso Negado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var button = (Button)sender;
            int comentarioId = (int)button.Tag;
            RegistrarVotoComentario(comentarioId, false);
        }

        private void RegistrarVotoComentario(int comentarioId, bool isLike)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var comentario = context.Comentarios.Find(comentarioId);
                    if (comentario != null)
                    {
                        if (isLike)
                            comentario.Likes++;
                        else
                            comentario.Dislikes++;

                        context.SaveChanges();
                        CarregarComentarios();
                        DiagnosticContext.LogarInfo($"Voto registrado para comentário {comentarioId}: {(isLike ? "Like" : "Dislike")}");
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao votar no comentário {comentarioId}", ex);
                MessageBox.Show($"❌ Erro ao votar no comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarLikesDislikes()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var likes = context.LikesDislikes.Count(ld => ld.LivroId == _livroId && ld.IsLike);
                    var dislikes = context.LikesDislikes.Count(ld => ld.LivroId == _livroId && !ld.IsLike);

                    lblLikes.Text = $"👍 {likes}";
                    lblDislikes.Text = $"👎 {dislikes}";

                    var total = likes + dislikes;
                    if (total > 0)
                    {
                        var percentual = (likes * 100) / total;
                        lblAprovacao.Text = $"{percentual}% de aprovação";
                        lblAprovacao.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao carregar likes/dislikes para livro {_livroId}", ex);
                MessageBox.Show($"Erro ao carregar likes: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarVotoUsuario()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var userVote = context.LikesDislikes
                        .FirstOrDefault(ld => ld.LivroId == _livroId && ld.UsuarioId == _usuarioLogadoId);

                    if (userVote != null)
                    {
                        btnLikeLivro.Enabled = false;
                        btnDislikeLivro.Enabled = false;

                        if (userVote.IsLike)
                        {
                            btnLikeLivro.BackColor = Color.ForestGreen;
                            btnLikeLivro.ForeColor = Color.White;
                            btnLikeLivro.Text = "👍 Você curtiu";
                        }
                        else
                        {
                            btnDislikeLivro.BackColor = Color.IndianRed;
                            btnDislikeLivro.ForeColor = Color.White;
                            btnDislikeLivro.Text = "👎 Você não curtiu";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro($"Erro ao verificar voto do usuário para livro {_livroId}", ex);
                MessageBox.Show($"Erro ao verificar voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNovoComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && ModifierKeys != Keys.Shift)
            {
                if (!string.IsNullOrWhiteSpace(txtNovoComentario.Text) && txtNovoComentario.ForeColor != Color.Gray)
                {
                    btnComentar.PerformClick();
                }
                e.Handled = true;
            }
        }

        private void FormLivroAberto_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ✅ LIMPEZA SEGURA DE RECURSOS
            if (pictureBoxCapa.Image != null)
            {
                var image = pictureBoxCapa.Image;
                pictureBoxCapa.Image = null;
                image.Dispose();
            }

            DiagnosticContext.LogarInfo("FormLivroAberto fechado");
        }

        private void txtNovoComentario_TextChanged(object sender, EventArgs e)
        {
            // ✅ VERIFICAR SE É PLACEHOLDER
            if (txtNovoComentario.ForeColor == Color.Gray) return;

            int count = txtNovoComentario.Text.Length;
            int max = 2000;
            lblContadorCaracteres.Text = $"{count}/{max}";

            if (count > max)
            {
                lblContadorCaracteres.ForeColor = Color.Red;
                btnComentar.Enabled = false;
            }
            else if (count > max * 0.8)
            {
                lblContadorCaracteres.ForeColor = Color.Orange;
                btnComentar.Enabled = count >= 5;
            }
            else if (count < 5)
            {
                lblContadorCaracteres.ForeColor = Color.Red;
                btnComentar.Enabled = false;
            }
            else
            {
                lblContadorCaracteres.ForeColor = Color.Green;
                btnComentar.Enabled = true;
            }
        }
    }
}