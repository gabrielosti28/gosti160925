using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormJogoMemoria : Form
    {
        private List<Livro> livrosJogo;
        private List<PictureBox> cartas = new List<PictureBox>();
        private PictureBox primeiraCarta, segundaCarta;
        private int paresEncontrados = 0;
        private int movimentos = 0;
        private int tempoDecorrido = 0;
        private Timer timerJogo;
        private string dificuldade;

        public FormJogoMemoria(string nivelDificuldade = "Médio")
        {
            //InitializeComponent();
            dificuldade = nivelDificuldade;
            IniciarJogo();
        }

        private void InitializeComponent()
        {
            // Configuração básica do formulário
            this.Text = "Jogo da Memória - BookConnect";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel superior (info)
            var panelInfo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(70, 130, 180)
            };

            var lblMovimentos = new Label
            {
                Name = "lblMovimentos",
                Text = "Movimentos: 0",
                Location = new Point(20, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };

            var lblTempo = new Label
            {
                Name = "lblTempo",
                Text = "Tempo: 0s",
                Location = new Point(250, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };

            var lblPares = new Label
            {
                Name = "lblPares",
                Text = "Pares: 0/0",
                Location = new Point(450, 20),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };

            panelInfo.Controls.AddRange(new Control[] { lblMovimentos, lblTempo, lblPares });
            this.Controls.Add(panelInfo);

            // Panel do jogo (cartas)
            var panelJogo = new Panel
            {
                Name = "panelJogo",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 248, 255),
                Padding = new Padding(20)
            };
            this.Controls.Add(panelJogo);

            // Timer
            timerJogo = new Timer { Interval = 1000 };
            timerJogo.Tick += (s, e) =>
            {
                tempoDecorrido++;
                this.Controls.Find("lblTempo", true)[0].Text = $"Tempo: {tempoDecorrido}s";
            };
        }

        private void IniciarJogo()
        {
            // Determina quantidade de pares por dificuldade
            int numPares = dificuldade == "Fácil" ? 6 : dificuldade == "Médio" ? 8 : 12;

            // Carrega livros
            livrosJogo = JogoMemoriaManager.ObterLivrosParaJogo(numPares);

            if (livrosJogo.Count < numPares)
            {
                MessageBox.Show("Livros insuficientes para iniciar o jogo.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Atualiza label de pares
            this.Controls.Find("lblPares", true)[0].Text = $"Pares: 0/{numPares}";

            // Cria cartas duplicadas e embaralha
            var todasCartas = new List<Livro>();
            todasCartas.AddRange(livrosJogo);
            todasCartas.AddRange(livrosJogo); // Duplica para pares
            todasCartas = todasCartas.OrderBy(x => Guid.NewGuid()).ToList();

            // Cria grid de cartas
            CriarGridCartas(todasCartas);

            // Inicia timer
            timerJogo.Start();
        }

        private void CriarGridCartas(List<Livro> livros)
        {
            var panelJogo = this.Controls.Find("panelJogo", true)[0];
            int totalCartas = livros.Count;

            // Calcula grid (4 colunas)
            int colunas = 4;
            int linhas = (int)Math.Ceiling(totalCartas / (double)colunas);

            int larguraCarta = 150;
            int alturaCarta = 200;
            int espacamento = 15;

            for (int i = 0; i < totalCartas; i++)
            {
                int col = i % colunas;
                int row = i / colunas;

                var carta = new PictureBox
                {
                    Size = new Size(larguraCarta, alturaCarta),
                    Location = new Point(
                        20 + col * (larguraCarta + espacamento),
                        20 + row * (alturaCarta + espacamento)
                    ),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray,
                    Cursor = Cursors.Hand,
                    Tag = livros[i] // Armazena o livro
                };

                // Mostra verso da carta (cinza)
                carta.Image = CriarImagemVerso();
                carta.Click += Carta_Click;

                cartas.Add(carta);
                panelJogo.Controls.Add(carta);
            }
        }

        private Image CriarImagemVerso()
        {
            var bmp = new Bitmap(150, 200);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(70, 130, 180));
                using (var font = new Font("Arial", 40, FontStyle.Bold))
                {
                    g.DrawString("📚", font, Brushes.White, new PointF(40, 70));
                }
            }
            return bmp;
        }

        private void Carta_Click(object sender, EventArgs e)
        {
            var cartaClicada = sender as PictureBox;

            // Ignora se já foi revelada ou se já há 2 cartas abertas
            if (cartaClicada.Tag == null || primeiraCarta == cartaClicada || segundaCarta != null)
                return;

            // Revela a carta
            var livro = cartaClicada.Tag as Livro;
            cartaClicada.Image?.Dispose();

            using (var ms = new MemoryStream(livro.Capa))
            {
                cartaClicada.Image = Image.FromStream(ms);
            }

            if (primeiraCarta == null)
            {
                primeiraCarta = cartaClicada;
            }
            else
            {
                segundaCarta = cartaClicada;
                movimentos++;
                this.Controls.Find("lblMovimentos", true)[0].Text = $"Movimentos: {movimentos}";

                // Verifica se é par
                var livro1 = primeiraCarta.Tag as Livro;
                var livro2 = segundaCarta.Tag as Livro;

                if (livro1.LivroId == livro2.LivroId)
                {
                    // ACERTOU! Remove as cartas
                    primeiraCarta.Tag = null;
                    segundaCarta.Tag = null;
                    primeiraCarta = null;
                    segundaCarta = null;

                    paresEncontrados++;
                    this.Controls.Find("lblPares", true)[0].Text =
                        $"Pares: {paresEncontrados}/{livrosJogo.Count}";

                    // Verifica vitória
                    if (paresEncontrados == livrosJogo.Count)
                    {
                        FinalizarJogo();
                    }
                }
                else
                {
                    // ERROU! Aguarda 1 segundo e vira as cartas
                    var timer = new Timer { Interval = 1000 };
                    timer.Tick += (s, ev) =>
                    {
                        primeiraCarta.Image?.Dispose();
                        segundaCarta.Image?.Dispose();
                        primeiraCarta.Image = CriarImagemVerso();
                        segundaCarta.Image = CriarImagemVerso();
                        primeiraCarta = null;
                        segundaCarta = null;
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();
                }
            }
        }

        private void FinalizarJogo()
        {
            timerJogo.Stop();

            int pontuacao = JogoMemoriaManager.CalcularPontuacao(livrosJogo.Count, movimentos, tempoDecorrido);

            var resultado = MessageBox.Show(
                $"🎉 PARABÉNS! Você completou o jogo!\n\n" +
                $"⭐ Pontuação: {pontuacao} pontos\n" +
                $"🎯 Movimentos: {movimentos}\n" +
                $"⏱️ Tempo: {tempoDecorrido} segundos\n\n" +
                $"Deseja salvar sua pontuação no ranking?",
                "Jogo Finalizado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (resultado == DialogResult.Yes)
            {
                if (JogoMemoriaManager.SalvarPontuacao(pontuacao, movimentos, tempoDecorrido, dificuldade))
                {
                    MessageBox.Show("Pontuação salva com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerJogo?.Stop();
            timerJogo?.Dispose();

            // Libera imagens
            foreach (var carta in cartas)
            {
                carta.Image?.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}