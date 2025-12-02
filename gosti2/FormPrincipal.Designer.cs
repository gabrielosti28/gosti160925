using System.Windows.Forms;

namespace gosti2
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pictureBoxPerfil = new System.Windows.Forms.PictureBox();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnTierList = new System.Windows.Forms.Button();
            this.btnMensagens = new System.Windows.Forms.Button();
            this.btnLivros = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelFeedGlobal = new System.Windows.Forms.Panel();
            this.flowLayoutPanelFeed = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPostarMensagem = new System.Windows.Forms.Panel();
            this.btnEnviarImagem = new System.Windows.Forms.Button();
            this.btnEnviarEmoji = new System.Windows.Forms.Button();
            this.btnPostar = new System.Windows.Forms.Button();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEstatisticas = new System.Windows.Forms.Panel();
            this.panelLivros = new System.Windows.Forms.Panel();
            this.lblLivrosCadastrados = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBio = new System.Windows.Forms.Label();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).BeginInit();
            this.panelLateral.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelFeedGlobal.SuspendLayout();
            this.panelPostarMensagem.SuspendLayout();
            this.panelEstatisticas.SuspendLayout();
            this.panelLivros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelSuperior.Controls.Add(this.lblUsuario);
            this.panelSuperior.Controls.Add(this.pictureBoxPerfil);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1085, 69);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(86, 26);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(187, 25);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Bem-vindo, Usuário!";
            // 
            // pictureBoxPerfil
            // 
            this.pictureBoxPerfil.BackColor = System.Drawing.Color.White;
            this.pictureBoxPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPerfil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPerfil.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxPerfil.Name = "pictureBoxPerfil";
            this.pictureBoxPerfil.Size = new System.Drawing.Size(52, 52);
            this.pictureBoxPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPerfil.TabIndex = 7;
            this.pictureBoxPerfil.TabStop = false;
            this.pictureBoxPerfil.Click += new System.EventHandler(this.pictureBoxPerfil_Click);
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.panelLateral.Controls.Add(this.btnPerfil);
            this.panelLateral.Controls.Add(this.btnTierList);
            this.panelLateral.Controls.Add(this.btnMensagens);
            this.panelLateral.Controls.Add(this.btnLivros);
            this.panelLateral.Controls.Add(this.btnSair);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 69);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(171, 590);
            this.panelLateral.TabIndex = 1;
            // 
            // btnPerfil
            // 
            this.btnPerfil.BackColor = System.Drawing.Color.Transparent;
            this.btnPerfil.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPerfil.FlatAppearance.BorderSize = 0;
            this.btnPerfil.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnPerfil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(210)))));
            this.btnPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerfil.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPerfil.ForeColor = System.Drawing.Color.White;
            this.btnPerfil.Location = new System.Drawing.Point(0, 291);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnPerfil.Size = new System.Drawing.Size(171, 115);
            this.btnPerfil.TabIndex = 6;
            this.btnPerfil.Text = "👤 Meu Perfil";
            this.btnPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPerfil.UseVisualStyleBackColor = false;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click);
            // 
            // btnTierList
            // 
            this.btnTierList.BackColor = System.Drawing.Color.Transparent;
            this.btnTierList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTierList.FlatAppearance.BorderSize = 0;
            this.btnTierList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnTierList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(210)))));
            this.btnTierList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTierList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTierList.ForeColor = System.Drawing.Color.White;
            this.btnTierList.Location = new System.Drawing.Point(0, 186);
            this.btnTierList.Name = "btnTierList";
            this.btnTierList.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnTierList.Size = new System.Drawing.Size(171, 105);
            this.btnTierList.TabIndex = 5;
            this.btnTierList.Text = "⭐ Tier Lists";
            this.btnTierList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTierList.UseVisualStyleBackColor = false;
            this.btnTierList.Click += new System.EventHandler(this.btnTierList_Click);
            // 
            // btnMensagens
            // 
            this.btnMensagens.BackColor = System.Drawing.Color.Transparent;
            this.btnMensagens.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMensagens.FlatAppearance.BorderSize = 0;
            this.btnMensagens.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnMensagens.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(210)))));
            this.btnMensagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMensagens.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnMensagens.ForeColor = System.Drawing.Color.White;
            this.btnMensagens.Location = new System.Drawing.Point(0, 87);
            this.btnMensagens.Name = "btnMensagens";
            this.btnMensagens.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnMensagens.Size = new System.Drawing.Size(171, 99);
            this.btnMensagens.TabIndex = 4;
            this.btnMensagens.Text = "✉️ Mensagens";
            this.btnMensagens.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMensagens.UseVisualStyleBackColor = false;
            this.btnMensagens.Click += new System.EventHandler(this.btnMensagens_Click);
            // 
            // btnLivros
            // 
            this.btnLivros.BackColor = System.Drawing.Color.Transparent;
            this.btnLivros.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLivros.FlatAppearance.BorderSize = 0;
            this.btnLivros.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnLivros.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(140)))), ((int)(((byte)(210)))));
            this.btnLivros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLivros.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLivros.ForeColor = System.Drawing.Color.White;
            this.btnLivros.Location = new System.Drawing.Point(0, 0);
            this.btnLivros.Name = "btnLivros";
            this.btnLivros.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnLivros.Size = new System.Drawing.Size(171, 87);
            this.btnLivros.TabIndex = 3;
            this.btnLivros.Text = "📚 Meus Livros";
            this.btnLivros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivros.UseVisualStyleBackColor = false;
            this.btnLivros.Click += new System.EventHandler(this.btnLivros_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(35)))), ((int)(((byte)(51)))));
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(58)))));
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(0, 538);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(171, 52);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "🚪 Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.White;
            this.panelConteudo.Controls.Add(this.panelFeedGlobal);
            this.panelConteudo.Controls.Add(this.panelEstatisticas);
            this.panelConteudo.Controls.Add(this.lblBio);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(171, 69);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(17);
            this.panelConteudo.Size = new System.Drawing.Size(914, 590);
            this.panelConteudo.TabIndex = 2;
            // 
            // panelFeedGlobal
            // 
            this.panelFeedGlobal.BackColor = System.Drawing.Color.White;
            this.panelFeedGlobal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFeedGlobal.Controls.Add(this.flowLayoutPanelFeed);
            this.panelFeedGlobal.Controls.Add(this.panelPostarMensagem);
            this.panelFeedGlobal.Location = new System.Drawing.Point(20, 156);
            this.panelFeedGlobal.Name = "panelFeedGlobal";
            this.panelFeedGlobal.Size = new System.Drawing.Size(852, 422);
            this.panelFeedGlobal.TabIndex = 12;
            // 
            // flowLayoutPanelFeed
            // 
            this.flowLayoutPanelFeed.AutoScroll = true;
            this.flowLayoutPanelFeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.flowLayoutPanelFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFeed.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelFeed.Location = new System.Drawing.Point(0, 87);
            this.flowLayoutPanelFeed.Name = "flowLayoutPanelFeed";
            this.flowLayoutPanelFeed.Padding = new System.Windows.Forms.Padding(9);
            this.flowLayoutPanelFeed.Size = new System.Drawing.Size(850, 333);
            this.flowLayoutPanelFeed.TabIndex = 1;
            this.flowLayoutPanelFeed.WrapContents = false;
            // 
            // panelPostarMensagem
            // 
            this.panelPostarMensagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelPostarMensagem.Controls.Add(this.btnEnviarImagem);
            this.panelPostarMensagem.Controls.Add(this.btnEnviarEmoji);
            this.panelPostarMensagem.Controls.Add(this.btnPostar);
            this.panelPostarMensagem.Controls.Add(this.txtMensagem);
            this.panelPostarMensagem.Controls.Add(this.label1);
            this.panelPostarMensagem.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPostarMensagem.Location = new System.Drawing.Point(0, 0);
            this.panelPostarMensagem.Name = "panelPostarMensagem";
            this.panelPostarMensagem.Padding = new System.Windows.Forms.Padding(9);
            this.panelPostarMensagem.Size = new System.Drawing.Size(850, 87);
            this.panelPostarMensagem.TabIndex = 0;
            // 
            // btnEnviarImagem
            // 
            this.btnEnviarImagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.btnEnviarImagem.FlatAppearance.BorderSize = 0;
            this.btnEnviarImagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarImagem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEnviarImagem.ForeColor = System.Drawing.Color.White;
            this.btnEnviarImagem.Location = new System.Drawing.Point(456, 52);
            this.btnEnviarImagem.Name = "btnEnviarImagem";
            this.btnEnviarImagem.Size = new System.Drawing.Size(86, 26);
            this.btnEnviarImagem.TabIndex = 4;
            this.btnEnviarImagem.Text = "🖼️ Imagem";
            this.btnEnviarImagem.UseVisualStyleBackColor = false;
            this.btnEnviarImagem.Click += new System.EventHandler(this.btnEnviarImagem_Click);
            // 
            // btnEnviarEmoji
            // 
            this.btnEnviarEmoji.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnEnviarEmoji.FlatAppearance.BorderSize = 0;
            this.btnEnviarEmoji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarEmoji.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEnviarEmoji.ForeColor = System.Drawing.Color.White;
            this.btnEnviarEmoji.Location = new System.Drawing.Point(365, 52);
            this.btnEnviarEmoji.Name = "btnEnviarEmoji";
            this.btnEnviarEmoji.Size = new System.Drawing.Size(86, 26);
            this.btnEnviarEmoji.TabIndex = 3;
            this.btnEnviarEmoji.Text = "😊 Emoji";
            this.btnEnviarEmoji.UseVisualStyleBackColor = false;
            this.btnEnviarEmoji.Click += new System.EventHandler(this.btnEnviarEmoji_Click);
            // 
            // btnPostar
            // 
            this.btnPostar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.btnPostar.FlatAppearance.BorderSize = 0;
            this.btnPostar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPostar.ForeColor = System.Drawing.Color.White;
            this.btnPostar.Location = new System.Drawing.Point(547, 52);
            this.btnPostar.Name = "btnPostar";
            this.btnPostar.Size = new System.Drawing.Size(77, 26);
            this.btnPostar.TabIndex = 2;
            this.btnPostar.Text = "📤 Postar";
            this.btnPostar.UseVisualStyleBackColor = false;
            this.btnPostar.Click += new System.EventHandler(this.btnPostar_Click);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMensagem.Location = new System.Drawing.Point(11, 30);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(613, 22);
            this.txtMensagem.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "💬 Feed de Livros";
            // 
            // panelEstatisticas
            // 
            this.panelEstatisticas.BackColor = System.Drawing.Color.Transparent;
            this.panelEstatisticas.Controls.Add(this.panelNotifMensagens);
            this.panelEstatisticas.Controls.Add(this.panelNotifComentarios);
            this.panelEstatisticas.Controls.Add(this.panelLivros);
            this.panelEstatisticas.Location = new System.Drawing.Point(20, 52);
            this.panelEstatisticas.Name = "panelEstatisticas";
            this.panelEstatisticas.Size = new System.Drawing.Size(646, 87);
            this.panelEstatisticas.TabIndex = 11;
            // 
            // panelLivros
            // 
            this.panelLivros.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelLivros.Controls.Add(this.lblLivrosCadastrados);
            this.panelLivros.Controls.Add(this.label4);
            this.panelLivros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelLivros.Location = new System.Drawing.Point(0, 0);
            this.panelLivros.Name = "panelLivros";
            this.panelLivros.Size = new System.Drawing.Size(171, 87);
            this.panelLivros.TabIndex = 6;
            this.panelLivros.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLivros_Paint);
            // 
            // panelNotifComentarios
            // 
            this.panelNotifComentarios = new System.Windows.Forms.Panel();
            this.lblNotifComentarios = new System.Windows.Forms.Label();
            this.lblComentariosCount = new System.Windows.Forms.Label();
            this.panelNotifComentarios.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelNotifComentarios.Controls.Add(this.lblComentariosCount);
            this.panelNotifComentarios.Controls.Add(this.lblNotifComentarios);
            this.panelNotifComentarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelNotifComentarios.Location = new System.Drawing.Point(177, 0);
            this.panelNotifComentarios.Name = "panelNotifComentarios";
            this.panelNotifComentarios.Size = new System.Drawing.Size(171, 87);
            this.panelNotifComentarios.TabIndex = 7;
            this.panelNotifComentarios.Click += new System.EventHandler(this.panelNotifComentarios_Click);
            // 
            // lblComentariosCount
            // 
            this.lblComentariosCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblComentariosCount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblComentariosCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblComentariosCount.Location = new System.Drawing.Point(0, 35);
            this.lblComentariosCount.Name = "lblComentariosCount";
            this.lblComentariosCount.Size = new System.Drawing.Size(171, 35);
            this.lblComentariosCount.TabIndex = 5;
            this.lblComentariosCount.Text = "0";
            this.lblComentariosCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblComentariosCount.Click += new System.EventHandler(this.lblComentariosCount_Click);
            // 
            // lblNotifComentarios
            // 
            this.lblNotifComentarios.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNotifComentarios.ForeColor = System.Drawing.Color.Gray;
            this.lblNotifComentarios.Location = new System.Drawing.Point(0, 13);
            this.lblNotifComentarios.Name = "lblNotifComentarios";
            this.lblNotifComentarios.Size = new System.Drawing.Size(171, 22);
            this.lblNotifComentarios.TabIndex = 2;
            this.lblNotifComentarios.Text = "💬 Comentários";
            this.lblNotifComentarios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelNotifMensagens
            // 
            this.panelNotifMensagens = new System.Windows.Forms.Panel();
            this.lblNotifMensagens = new System.Windows.Forms.Label();
            this.lblMensagensCount = new System.Windows.Forms.Label();
            this.panelNotifMensagens.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelNotifMensagens.Controls.Add(this.lblMensagensCount);
            this.panelNotifMensagens.Controls.Add(this.lblNotifMensagens);
            this.panelNotifMensagens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelNotifMensagens.Location = new System.Drawing.Point(354, 0);
            this.panelNotifMensagens.Name = "panelNotifMensagens";
            this.panelNotifMensagens.Size = new System.Drawing.Size(171, 87);
            this.panelNotifMensagens.TabIndex = 8;
            this.panelNotifMensagens.Click += new System.EventHandler(this.panelNotifMensagens_Click);
            // 
            // lblMensagensCount
            // 
            this.lblMensagensCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMensagensCount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMensagensCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.lblMensagensCount.Location = new System.Drawing.Point(0, 35);
            this.lblMensagensCount.Name = "lblMensagensCount";
            this.lblMensagensCount.Size = new System.Drawing.Size(171, 35);
            this.lblMensagensCount.TabIndex = 5;
            this.lblMensagensCount.Text = "0";
            this.lblMensagensCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMensagensCount.Click += new System.EventHandler(this.lblMensagensCount_Click);
            // 
            // lblNotifMensagens
            // 
            this.lblNotifMensagens.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNotifMensagens.ForeColor = System.Drawing.Color.Gray;
            this.lblNotifMensagens.Location = new System.Drawing.Point(0, 13);
            this.lblNotifMensagens.Name = "lblNotifMensagens";
            this.lblNotifMensagens.Size = new System.Drawing.Size(171, 22);
            this.lblNotifMensagens.TabIndex = 2;
            this.lblNotifMensagens.Text = "✉️ Mensagens";
            this.lblNotifMensagens.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;




            // 
            // lblLivrosCadastrados
            // 
            this.lblLivrosCadastrados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLivrosCadastrados.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblLivrosCadastrados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblLivrosCadastrados.Location = new System.Drawing.Point(0, 35);
            this.lblLivrosCadastrados.Name = "lblLivrosCadastrados";
            this.lblLivrosCadastrados.Size = new System.Drawing.Size(171, 35);
            this.lblLivrosCadastrados.TabIndex = 5;
            this.lblLivrosCadastrados.Text = "0";
            this.lblLivrosCadastrados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLivrosCadastrados.Click += new System.EventHandler(this.lblLivrosCadastrados_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(0, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "📚 Livros Cadastrados";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBio
            // 
            this.lblBio.AutoSize = true;
            this.lblBio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBio.ForeColor = System.Drawing.Color.Gray;
            this.lblBio.Location = new System.Drawing.Point(20, 17);
            this.lblBio.Name = "lblBio";
            this.lblBio.Size = new System.Drawing.Size(174, 19);
            this.lblBio.TabIndex = 10;
            this.lblBio.Text = "🌟 Apaixonado por livros...";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 659);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.panelSuperior);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gosti2 - Principal";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).EndInit();
            this.panelLateral.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.panelFeedGlobal.ResumeLayout(false);
            this.panelPostarMensagem.ResumeLayout(false);
            this.panelPostarMensagem.PerformLayout();
            this.panelEstatisticas.ResumeLayout(false);
            this.panelLivros.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNotifComentarios;
        private System.Windows.Forms.Label lblNotifComentarios;
        private System.Windows.Forms.Label lblComentariosCount;
        private System.Windows.Forms.Panel panelNotifMensagens;
        private System.Windows.Forms.Label lblNotifMensagens;
        private System.Windows.Forms.Label lblMensagensCount;
        private Panel panelSuperior;
        private Label lblUsuario;
        private PictureBox pictureBoxPerfil;
        private Panel panelLateral;
        private Button btnPerfil;
        private Button btnTierList;
        private Button btnMensagens;
        private Button btnLivros;
        private Button btnSair;
        private Panel panelConteudo;
        private Panel panelFeedGlobal;
        private FlowLayoutPanel flowLayoutPanelFeed;
        private Panel panelPostarMensagem;
        private Button btnEnviarImagem;
        private Button btnEnviarEmoji;
        private Button btnPostar;
        private TextBox txtMensagem;
        private Label label1;
        private Panel panelEstatisticas;
        private Panel panelLivros;
        private Label lblLivrosCadastrados;
        private Label label4;
        private Label lblBio;
    }
}