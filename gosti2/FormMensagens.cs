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
    public partial class FormMensagens : Form
    {
        private int? conversaAtualUsuarioId = null;
        private Timer timerAtualizacao;

        public FormMensagens()
        {
            InitializeComponent();

            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            InicializarComponentes();
            CarregarConversas();
            InicializarTimerAtualizacao();
        }

        private void InicializarComponentes()
        {
            // Configuração do FlowLayoutPanel de conversas
            flowLayoutConversas.AutoScroll = true;
            flowLayoutConversas.FlowDirection = FlowDirection.TopDown;
            flowLayoutConversas.WrapContents = false;

            // Configuração do FlowLayoutPanel de mensagens
            flowLayoutMensagens.AutoScroll = true;
            flowLayoutMensagens.FlowDirection = FlowDirection.TopDown;
            flowLayoutMensagens.WrapContents = false;

            // Oculta painel de chat inicial
            panelChat.Visible = false;

            // Placeholder para caixa de texto
            ConfigurarPlaceholder();
        }

        private void ConfigurarPlaceholder()
        {
            txtMensagem.Text = "Digite sua mensagem...";
            txtMensagem.ForeColor = Color.Gray;

            txtMensagem.Enter += (s, e) =>
            {
                if (txtMensagem.Text == "Digite sua mensagem..." && txtMensagem.ForeColor == Color.Gray)
                {
                    txtMensagem.Text = "";
                    txtMensagem.ForeColor = Color.Black;
                }
            };

            txtMensagem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMensagem.Text))
                {
                    txtMensagem.Text = "Digite sua mensagem...";
                    txtMensagem.ForeColor = Color.Gray;
                }
            };
        }

        private void InicializarTimerAtualizacao()
        {
            // Atualiza mensagens a cada 3 segundos
            timerAtualizacao = new Timer();
            timerAtualizacao.Interval = 3000;
            timerAtualizacao.Tick += (s, e) =>
            {
                if (conversaAtualUsuarioId.HasValue)
                {
                    CarregarMensagensConversa(conversaAtualUsuarioId.Value, false);
                }
                CarregarConversas(); // Atualiza lista de conversas
            };
            timerAtualizacao.Start();
        }

        private void CarregarConversas()
        {
            try
            {
                flowLayoutConversas.SuspendLayout();
                flowLayoutConversas.Controls.Clear();

                var conversas = MensagemPrivadaManager.CarregarConversas();

                if (conversas.Count == 0)
                {
                    var lblVazio = new Label
                    {
                        Text = "📭 Nenhuma conversa ainda.\nInicie uma conversa visitando o perfil de outro usuário!",
                        Font = new Font("Segoe UI", 10, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowLayoutConversas.Controls.Add(lblVazio);
                }
                else
                {
                    foreach (var conversa in conversas)
                    {
                        var panel = CriarPanelConversa(conversa);
                        flowLayoutConversas.Controls.Add(panel);
                    }
                }

                flowLayoutConversas.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar conversas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CriarPanelConversa(dynamic conversa)
        {
            int largura = flowLayoutConversas.Width - 30;
            var ultimaMensagem = (Mensagem)conversa.UltimaMensagem;
            int contatoId = (int)conversa.ContatoId;
            int naoLidas = (int)conversa.NaoLidas;

            // Busca informações do contato
            Usuario contato = null;
            using (var db = new ApplicationDbContext())
            {
                contato = db.Usuarios.Find(contatoId);
            }

            if (contato == null) return new Panel();

            var panel = new Panel
            {
                Width = largura,
                Height = 80,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = naoLidas > 0 ? Color.FromArgb(240, 248, 255) : Color.White,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Tag = contatoId
            };

            // Foto do contato
            var picFoto = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(60, 60),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            if (contato.FotoPerfil != null && contato.FotoPerfil.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream(contato.FotoPerfil))
                    {
                        picFoto.Image = Image.FromStream(ms);
                    }
                }
                catch { }
            }

            panel.Controls.Add(picFoto);

            // Nome do contato
            var lblNome = new Label
            {
                Text = contato.NomeUsuario,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(80, 10),
                AutoSize = true
            };
            panel.Controls.Add(lblNome);

            // Preview da última mensagem
            var lblUltimaMensagem = new Label
            {
                Text = ultimaMensagem.Texto.Length > 50
                    ? ultimaMensagem.Texto.Substring(0, 50) + "..."
                    : ultimaMensagem.Texto,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(80, 35),
                MaximumSize = new Size(largura - 180, 30)
            };
            panel.Controls.Add(lblUltimaMensagem);

            // Data/hora
            var lblData = new Label
            {
                Text = FormatarTempoRelativo(ultimaMensagem.DataEnvio),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(80, 60),
                AutoSize = true
            };
            panel.Controls.Add(lblData);

            // Badge de não lidas
            if (naoLidas > 0)
            {
                var lblBadge = new Label
                {
                    Text = naoLidas > 99 ? "99+" : naoLidas.ToString(),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(220, 53, 69),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(largura - 50, 30),
                    Size = new Size(35, 20),
                    BorderStyle = BorderStyle.FixedSingle
                };
                panel.Controls.Add(lblBadge);
            }

            // Evento de clique
            panel.Click += (s, e) => AbrirConversa(contatoId);
            picFoto.Click += (s, e) => AbrirConversa(contatoId);
            lblNome.Click += (s, e) => AbrirConversa(contatoId);

            return panel;
        }

        private string FormatarTempoRelativo(DateTime data)
        {
            var tempo = DateTime.Now - data;

            if (tempo.TotalMinutes < 1) return "Agora";
            if (tempo.TotalMinutes < 60) return $"{(int)tempo.TotalMinutes}m";
            if (tempo.TotalHours < 24) return $"{(int)tempo.TotalHours}h";
            if (tempo.TotalDays < 7) return $"{(int)tempo.TotalDays}d";

            return data.ToString("dd/MM/yy");
        }

        private void AbrirConversa(int usuarioId)
        {
            conversaAtualUsuarioId = usuarioId;
            panelChat.Visible = true;

            // Marca mensagens como lidas
            MensagemPrivadaManager.MarcarComoLidas(usuarioId);

            // Atualiza nome do contato no header
            using (var db = new ApplicationDbContext())
            {
                var contato = db.Usuarios.Find(usuarioId);
                if (contato != null)
                {
                    lblNomeContato.Text = contato.NomeUsuario;
                }
            }

            // Carrega mensagens
            CarregarMensagensConversa(usuarioId);

            // Recarrega lista de conversas para atualizar badge
            CarregarConversas();
        }

        private void CarregarMensagensConversa(int usuarioId, bool rolarParaBaixo = true)
        {
            try
            {
                flowLayoutMensagens.SuspendLayout();

                // Salva posição do scroll se não for para rolar
                int scrollPos = 0;
                if (!rolarParaBaixo && flowLayoutMensagens.VerticalScroll.Visible)
                {
                    scrollPos = flowLayoutMensagens.VerticalScroll.Value;
                }

                flowLayoutMensagens.Controls.Clear();

                var mensagens = MensagemPrivadaManager.CarregarHistorico(usuarioId);

                foreach (var mensagem in mensagens)
                {
                    var panel = CriarPanelMensagem(mensagem);
                    flowLayoutMensagens.Controls.Add(panel);
                }

                flowLayoutMensagens.ResumeLayout();

                // Rola para o final ou restaura posição
                if (rolarParaBaixo)
                {
                    flowLayoutMensagens.ScrollControlIntoView(
                        flowLayoutMensagens.Controls[flowLayoutMensagens.Controls.Count - 1]
                    );
                }
                else if (flowLayoutMensagens.VerticalScroll.Visible)
                {
                    flowLayoutMensagens.VerticalScroll.Value = Math.Min(scrollPos, flowLayoutMensagens.VerticalScroll.Maximum);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar mensagens: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CriarPanelMensagem(Mensagem mensagem)
        {
            int largura = flowLayoutMensagens.Width - 40;
            bool ehMinhaMsg = (mensagem.RemetenteId == AppManager.UsuarioLogado.UsuarioId);

            var panel = new Panel
            {
                Width = largura,
                AutoSize = true,
                MinimumSize = new Size(largura, 60),
                Margin = new Padding(5)
            };

            // Balão da mensagem
            var panelBalao = new Panel
            {
                MaximumSize = new Size((int)(largura * 0.7), 0),
                AutoSize = true,
                MinimumSize = new Size(100, 40),
                BackColor = ehMinhaMsg ? Color.FromArgb(70, 130, 180) : Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            // Texto da mensagem
            var lblTexto = new Label
            {
                Text = mensagem.Texto,
                Font = new Font("Segoe UI", 10),
                ForeColor = ehMinhaMsg ? Color.White : Color.Black,
                AutoSize = true,
                MaximumSize = new Size((int)(largura * 0.7) - 30, 0)
            };
            panelBalao.Controls.Add(lblTexto);

            // Data/hora
            var lblHora = new Label
            {
                Text = mensagem.DataEnvio.ToString("HH:mm"),
                Font = new Font("Segoe UI", 8),
                ForeColor = ehMinhaMsg ? Color.FromArgb(200, 220, 240) : Color.Gray,
                AutoSize = true,
                Location = new Point(10, lblTexto.Bottom + 5)
            };
            panelBalao.Controls.Add(lblHora);

            // Ajusta altura do balão
            panelBalao.Height = lblHora.Bottom + 10;

            // Posiciona balão (direita para minhas msgs, esquerda para recebidas)
            if (ehMinhaMsg)
            {
                panelBalao.Location = new Point(largura - panelBalao.Width - 5, 5);
            }
            else
            {
                panelBalao.Location = new Point(5, 5);
            }

            panel.Controls.Add(panelBalao);
            panel.Height = panelBalao.Height + 10;

            return panel;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (!conversaAtualUsuarioId.HasValue)
            {
                MessageBox.Show("Selecione uma conversa primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMensagem.ForeColor == Color.Gray || string.IsNullOrWhiteSpace(txtMensagem.Text))
            {
                MessageBox.Show("Digite uma mensagem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MensagemPrivadaManager.EnviarMensagem(conversaAtualUsuarioId.Value, txtMensagem.Text))
            {
                txtMensagem.Clear();
                txtMensagem.Text = "Digite sua mensagem...";
                txtMensagem.ForeColor = Color.Gray;

                CarregarMensagensConversa(conversaAtualUsuarioId.Value);
                CarregarConversas();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            timerAtualizacao?.Stop();
            timerAtualizacao?.Dispose();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerAtualizacao?.Stop();
            timerAtualizacao?.Dispose();
            base.OnFormClosing(e);
        }
    }
}