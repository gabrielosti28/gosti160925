using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário fazer login primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CarregarDadosUsuario();
            CarregarFeedGlobal();
        }

        private void CarregarDadosUsuario()
        {
            var usuario = AppManager.UsuarioLogado;

            lblUsuario.Text = $"Bem-vindo, {usuario.NomeUsuario}!";
            lblBio.Text = string.IsNullOrEmpty(usuario.Bio)
                ? "🌟 Apaixonado por livros..."
                : usuario.Bio;

            CarregarFotoPerfil();
            CarregarEstatisticas();
            CarregarNotificacoes();
        }

        private void CarregarFotoPerfil()
        {
            var usuario = AppManager.UsuarioLogado;

            try
            {
                if (usuario.FotoPerfil != null && usuario.FotoPerfil.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(usuario.FotoPerfil))
                    {
                        pictureBoxPerfil.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxPerfil.BackColor = Color.LightGray;
                }
            }
            catch
            {
                pictureBoxPerfil.BackColor = Color.LightGray;
            }
        }

        private void CarregarEstatisticas()
        {
            try
            {
                var (totalLivros, livrosLidos, livrosFavoritos) =
                    AppManager.ObterEstatisticasUsuario();

                lblLivrosCadastrados.Text = totalLivros.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar estatísticas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarFeedGlobal()
        {
            try
            {
                flowLayoutPanelFeed.Controls.Clear();

                var mensagens = MensagemGlobalManager.CarregarMensagens(50);

                if (mensagens.Count == 0)
                {
                    var lblVazio = new Label
                    {
                        Text = "📝 Nenhuma mensagem ainda.\nSeja o primeiro a postar!",
                        Font = new Font("Segoe UI", 11, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowLayoutPanelFeed.Controls.Add(lblVazio);
                    return;
                }

                foreach (var mensagem in mensagens)
                {
                    var panelMsg = CriarPanelMensagem(mensagem);
                    flowLayoutPanelFeed.Controls.Add(panelMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar feed: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CriarPanelMensagem(MensagemGlobal mensagem)
        {
            int largura = flowLayoutPanelFeed.Width - 30;

            var panel = new Panel
            {
                Width = largura,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(5),
                Padding = new Padding(10),
                AutoSize = true,
                MinimumSize = new Size(largura, 80)
            };

            // Foto do usuário (clicável)
            var picUsuario = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                Cursor = Cursors.Hand
            };

            if (mensagem.Usuario?.FotoPerfil != null && mensagem.Usuario.FotoPerfil.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(mensagem.Usuario.FotoPerfil))
                    {
                        picUsuario.Image = Image.FromStream(ms);
                    }
                }
                catch { }
            }

            picUsuario.Click += (s, e) => AbrirPerfilUsuario(mensagem.UsuarioId);
            panel.Controls.Add(picUsuario);

            // Nome do usuário (clicável)
            var lblNome = new Label
            {
                Text = mensagem.Usuario?.NomeUsuario ?? "Usuário",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 130, 180),
                Location = new Point(70, 10),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            lblNome.Click += (s, e) => AbrirPerfilUsuario(mensagem.UsuarioId);
            panel.Controls.Add(lblNome);

            // Data e indicador de edição
            var textoData = mensagem.TempoRelativo;
            if (mensagem.Editada)
                textoData += " (editada)";

            var lblData = new Label
            {
                Text = textoData,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(70, 35),
                AutoSize = true
            };
            panel.Controls.Add(lblData);

            // Texto da mensagem
            if (!string.IsNullOrWhiteSpace(mensagem.TextoMensagem))
            {
                var txtMsg = new TextBox
                {
                    Text = mensagem.TextoMensagem,
                    Font = new Font("Segoe UI", 10),
                    Location = new Point(70, 55),
                    Width = largura - 90,
                    ReadOnly = true,
                    Multiline = true,
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.White,
                    ScrollBars = ScrollBars.Vertical
                };

                // Ajusta altura automaticamente
                int linhas = mensagem.TextoMensagem.Split('\n').Length;
                txtMsg.Height = Math.Max(60, linhas * 20);

                panel.Controls.Add(txtMsg);
                panel.Height = Math.Max(80, txtMsg.Bottom + 50);
            }

            // Imagem da mensagem (se houver)
            if (mensagem.ImagemMensagem != null && mensagem.ImagemMensagem.Length > 0)
            {
                try
                {
                    var picImagem = new PictureBox
                    {
                        Location = new Point(70, panel.Height - 10),
                        Size = new Size(largura - 90, 200),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand
                    };

                    using (var ms = new MemoryStream(mensagem.ImagemMensagem))
                    {
                        picImagem.Image = Image.FromStream(ms);
                    }

                    picImagem.Click += (s, e) =>
                    {
                        // Abre imagem em tela cheia (implementação simples)
                        var formImg = new Form
                        {
                            Text = "Imagem",
                            Size = new Size(800, 600),
                            StartPosition = FormStartPosition.CenterParent
                        };
                        var pic = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = picImagem.Image
                        };
                        formImg.Controls.Add(pic);
                        formImg.ShowDialog();
                    };

                    panel.Controls.Add(picImagem);
                    panel.Height = picImagem.Bottom + 10;
                }
                catch { }
            }

            // Botão de remover (apenas para o autor)
            if (mensagem.UsuarioId == AppManager.UsuarioLogado.UsuarioId)
            {
                var btnRemover = new Button
                {
                    Text = "🗑️",
                    Size = new Size(30, 25),
                    Location = new Point(largura - 45, 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(220, 80, 80),
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand
                };
                btnRemover.FlatAppearance.BorderSize = 0;

                btnRemover.Click += (s, e) =>
                {
                    if (MessageBox.Show("Deseja remover esta mensagem?", "Confirmar",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (MensagemGlobalManager.RemoverMensagem(mensagem.MensagemGlobalId))
                        {
                            CarregarFeedGlobal();
                        }
                    }
                };

                panel.Controls.Add(btnRemover);
            }

            return panel;
        }

        private void AbrirPerfilUsuario(int usuarioId)
        {
            try
            {
                this.Hide();
                using (var formPerfil = new FormMeuPerfil(usuarioId))
                {
                    formPerfil.ShowDialog();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir perfil: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPostar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMensagem.Text))
            {
                MessageBox.Show("Digite uma mensagem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MensagemGlobalManager.PostarMensagem(txtMensagem.Text))
            {
                txtMensagem.Clear();
                CarregarFeedGlobal();
            }
        }

        private void btnEnviarImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Imagens|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Selecionar Imagem";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Image img = Image.FromFile(openFileDialog.FileName))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] imagemBytes = ms.ToArray();

                            string texto = txtMensagem.Text;
                            if (MensagemGlobalManager.PostarMensagemComImagem(texto, imagemBytes))
                            {
                                txtMensagem.Clear();
                                CarregarFeedGlobal();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao enviar imagem: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEnviarEmoji_Click(object sender, EventArgs e)
        {
            // Menu simples de emojis
            var emojis = new[] { "😊", "❤️", "👍", "📚", "🎉", "😂", "🔥", "⭐", "💯", "🙌" };

            var menu = new ContextMenuStrip();
            foreach (var emoji in emojis)
            {
                var item = new ToolStripMenuItem(emoji);
                item.Click += (s, ev) =>
                {
                    txtMensagem.Text += emoji;
                    txtMensagem.Focus();
                };
                menu.Items.Add(item);
            }

            menu.Show(btnEnviarEmoji, new Point(0, btnEnviarEmoji.Height));
        }

        private void btnLivros_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var formLivros = new FormMeusLivros())
            {
                formLivros.ShowDialog();
            }
            this.Show();
            CarregarEstatisticas();
        }

        // SUBSTITUIR o método btnMensagens_Click no FormPrincipal.cs por:

        private void btnMensagens_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var formMensagens = new FormMensagens())
            {
                formMensagens.ShowDialog();
            }
            this.Show();

            // Atualiza estatísticas caso necessário
            CarregarEstatisticas();
        }

        private void btnTierList_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var FormTierList = new FormTierList())
            {
                FormTierList.ShowDialog();
            }
            this.Show();
            CarregarEstatisticas();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var FormMeuPerfil = new FormMeuPerfil())
            {
                FormMeuPerfil.ShowDialog();
            }
            this.Show();
            CarregarEstatisticas();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair do sistema?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppManager.Logout();
                this.Close();
            }
        }

        private void pictureBoxPerfil_Click(object sender, EventArgs e)
        {
            btnPerfil_Click(sender, e);
        }

        private void lblLivrosCadastrados_Click(object sender, EventArgs e)
        {
            btnLivros_Click(sender, e);
        }

        private void panelAtividades_Paint(object sender, PaintEventArgs e)
        {
            // Evento mantido para compatibilidade
        }

        private void panelLivros_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            btnTierList_Click(sender, e);
        }

        private void CarregarNotificacoes()
        {
            // Comentários nos livros do usuário
            int totalComentarios = AppManager.ContarComentariosNaoLidos();
            lblComentariosCount.Text = totalComentarios.ToString();

            // Mensagens não lidas
            int totalMensagens = MensagemPrivadaManager.ContarNaoLidas();
            lblMensagensCount.Text = totalMensagens.ToString();
        }

        private void panelNotifComentarios_Click(object sender, EventArgs e)
        {
            MostrarDetalhesComentarios();
        }

        private void lblComentariosCount_Click(object sender, EventArgs e)
        {
            MostrarDetalhesComentarios();
        }

        private void MostrarDetalhesComentarios()
        {
            try
            {
                var comentarios = AppManager.ObterComentariosNosLivrosDoUsuario();

                if (comentarios.Count == 0)
                {
                    MessageBox.Show("📭 Nenhum comentário novo em seus livros.", "Notificações",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Criar formulário de detalhes
                using (var formDetalhes = new Form())
                {
                    formDetalhes.Text = "Comentários nos seus Livros";
                    formDetalhes.Size = new Size(600, 500);
                    formDetalhes.StartPosition = FormStartPosition.CenterParent;
                    formDetalhes.FormBorderStyle = FormBorderStyle.FixedDialog;
                    formDetalhes.MaximizeBox = false;

                    // FlowLayoutPanel para lista de comentários
                    var flowPanel = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        AutoScroll = true,
                        FlowDirection = FlowDirection.TopDown,
                        WrapContents = false,
                        Padding = new Padding(10)
                    };

                    foreach (var comentario in comentarios)
                    {
                        var panelComentario = CriarPanelComentario(comentario);
                        flowPanel.Controls.Add(panelComentario);
                    }

                    formDetalhes.Controls.Add(flowPanel);

                    // Botão fechar
                    var btnFechar = new Button
                    {
                        Text = "Fechar",
                        Dock = DockStyle.Bottom,
                        Height = 40,
                        BackColor = Color.FromArgb(70, 130, 180),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    btnFechar.FlatAppearance.BorderSize = 0;
                    btnFechar.Click += (s, ev) => formDetalhes.Close();
                    formDetalhes.Controls.Add(btnFechar);

                    formDetalhes.ShowDialog();

                    // Atualiza contadores após visualizar
                    CarregarNotificacoes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar comentários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CriarPanelComentario(dynamic comentario)
        {
            var panel = new Panel
            {
                Width = 560,
                Height = 80,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(5),
                Cursor = Cursors.Hand
            };

            // Foto do usuário
            var picFoto = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            if (comentario.FotoPerfil != null && ((byte[])comentario.FotoPerfil).Length > 0)
            {
                try
                {
                    using (var ms = new System.IO.MemoryStream((byte[])comentario.FotoPerfil))
                    {
                        picFoto.Image = Image.FromStream(ms);
                    }
                }
                catch { }
            }

            panel.Controls.Add(picFoto);

            // Nome do usuário
            var lblNome = new Label
            {
                Text = comentario.NomeUsuario,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(70, 10),
                AutoSize = true
            };
            panel.Controls.Add(lblNome);

            // Livro comentado
            var lblLivro = new Label
            {
                Text = $"comentou em: {comentario.TituloLivro}",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(70, 30),
                AutoSize = true
            };
            panel.Controls.Add(lblLivro);

            // Tempo
            var lblTempo = new Label
            {
                Text = comentario.TempoRelativo,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(70, 50),
                AutoSize = true
            };
            panel.Controls.Add(lblTempo);

            // Clique para abrir livro
            panel.Click += (s, e) =>
            {
                using (var formLivro = new FormLivroAberto((int)comentario.LivroId, AppManager.UsuarioLogado.UsuarioId))
                {
                    formLivro.ShowDialog();
                }
            };

            return panel;
        }

        private void panelNotifMensagens_Click(object sender, EventArgs e)
        {
            btnMensagens_Click(sender, e);
        }

        private void lblMensagensCount_Click(object sender, EventArgs e)
        {
            btnMensagens_Click(sender, e);
        }






    }




}