namespace gosti2
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnTierList = new System.Windows.Forms.Button();
            this.btnMensagens = new System.Windows.Forms.Button();
            this.btnLivros = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelEstatisticas = new System.Windows.Forms.Panel();
            this.lblLivrosCadastrados = new System.Windows.Forms.Label();
            this.lblAmigos = new System.Windows.Forms.Label();
            this.lblMensagens = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxPerfil = new System.Windows.Forms.PictureBox();
            this.lblBio = new System.Windows.Forms.Label();
            this.lblBemVindo = new System.Windows.Forms.Label();
            this.panelAtividades = new System.Windows.Forms.Panel();
            this.listBoxAtividades = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRodape = new System.Windows.Forms.Panel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.panelSuperior.SuspendLayout();
            this.panelLateral.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelEstatisticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).BeginInit();
            this.panelAtividades.SuspendLayout();
            this.panelRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelSuperior.Controls.Add(this.lblUsuario);
            this.panelSuperior.Controls.Add(this.panelLogo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1000, 80);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(100, 25);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(215, 30);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Bem-vindo, Usuário!";
            this.lblUsuario.Click += new System.EventHandler(this.lblUsuario_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelLogo.Location = new System.Drawing.Point(20, 10);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(60, 60);
            this.panelLogo.TabIndex = 0;
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
            this.panelLateral.Location = new System.Drawing.Point(0, 80);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(200, 520);
            this.panelLateral.TabIndex = 1;
            // 
            // btnPerfil
            // 
            this.btnPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(110)))), ((int)(((byte)(170)))));
            this.btnPerfil.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPerfil.FlatAppearance.BorderSize = 0;
            this.btnPerfil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerfil.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerfil.ForeColor = System.Drawing.Color.White;
            this.btnPerfil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPerfil.Location = new System.Drawing.Point(0, 240);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPerfil.Size = new System.Drawing.Size(200, 60);
            this.btnPerfil.TabIndex = 6;
            this.btnPerfil.Text = "👤 Meu Perfil";
            this.btnPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPerfil.UseVisualStyleBackColor = false;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click);
            // 
            // btnTierList
            // 
            this.btnTierList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(110)))), ((int)(((byte)(170)))));
            this.btnTierList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTierList.FlatAppearance.BorderSize = 0;
            this.btnTierList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnTierList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTierList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTierList.ForeColor = System.Drawing.Color.White;
            this.btnTierList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTierList.Location = new System.Drawing.Point(0, 180);
            this.btnTierList.Name = "btnTierList";
            this.btnTierList.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnTierList.Size = new System.Drawing.Size(200, 60);
            this.btnTierList.TabIndex = 5;
            this.btnTierList.Text = "⭐ Tier Lists";
            this.btnTierList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTierList.UseVisualStyleBackColor = false;
            this.btnTierList.Click += new System.EventHandler(this.btnTierList_Click);
            // 
            // btnMensagens
            // 
            this.btnMensagens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(110)))), ((int)(((byte)(170)))));
            this.btnMensagens.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMensagens.FlatAppearance.BorderSize = 0;
            this.btnMensagens.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnMensagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMensagens.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMensagens.ForeColor = System.Drawing.Color.White;
            this.btnMensagens.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMensagens.Location = new System.Drawing.Point(0, 120);
            this.btnMensagens.Name = "btnMensagens";
            this.btnMensagens.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMensagens.Size = new System.Drawing.Size(200, 60);
            this.btnMensagens.TabIndex = 4;
            this.btnMensagens.Text = "✉️ Mensagens";
            this.btnMensagens.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMensagens.UseVisualStyleBackColor = false;
            this.btnMensagens.Click += new System.EventHandler(this.btnMensagens_Click);
            // 
            // btnLivros
            // 
            this.btnLivros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(110)))), ((int)(((byte)(170)))));
            this.btnLivros.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLivros.FlatAppearance.BorderSize = 0;
            this.btnLivros.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(200)))));
            this.btnLivros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLivros.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLivros.ForeColor = System.Drawing.Color.White;
            this.btnLivros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivros.Location = new System.Drawing.Point(0, 60);
            this.btnLivros.Name = "btnLivros";
            this.btnLivros.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLivros.Size = new System.Drawing.Size(200, 60);
            this.btnLivros.TabIndex = 3;
            this.btnLivros.Text = "📚 Meus Livros";
            this.btnLivros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivros.UseVisualStyleBackColor = false;
            this.btnLivros.Click += new System.EventHandler(this.btnLivros_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60)))));
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(0)))));
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(0, 460);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(200, 60);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "🚪 Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelConteudo.Controls.Add(this.panelEstatisticas);
            this.panelConteudo.Controls.Add(this.pictureBoxPerfil);
            this.panelConteudo.Controls.Add(this.lblBio);
            this.panelConteudo.Controls.Add(this.lblBemVindo);
            this.panelConteudo.Controls.Add(this.panelAtividades);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(200, 80);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(20);
            this.panelConteudo.Size = new System.Drawing.Size(800, 520);
            this.panelConteudo.TabIndex = 2;
            // 
            // panelEstatisticas
            // 
            this.panelEstatisticas.BackColor = System.Drawing.Color.White;
            this.panelEstatisticas.Controls.Add(this.lblLivrosCadastrados);
            this.panelEstatisticas.Controls.Add(this.lblAmigos);
            this.panelEstatisticas.Controls.Add(this.lblMensagens);
            this.panelEstatisticas.Controls.Add(this.label4);
            this.panelEstatisticas.Controls.Add(this.label3);
            this.panelEstatisticas.Controls.Add(this.label2);
            this.panelEstatisticas.Location = new System.Drawing.Point(23, 80);
            this.panelEstatisticas.Name = "panelEstatisticas";
            this.panelEstatisticas.Size = new System.Drawing.Size(754, 100);
            this.panelEstatisticas.TabIndex = 8;
            // 
            // lblLivrosCadastrados
            // 
            this.lblLivrosCadastrados.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLivrosCadastrados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblLivrosCadastrados.Location = new System.Drawing.Point(150, 40);
            this.lblLivrosCadastrados.Name = "lblLivrosCadastrados";
            this.lblLivrosCadastrados.Size = new System.Drawing.Size(150, 30);
            this.lblLivrosCadastrados.TabIndex = 5;
            this.lblLivrosCadastrados.Text = "0";
            this.lblLivrosCadastrados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAmigos
            // 
            this.lblAmigos.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmigos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblAmigos.Location = new System.Drawing.Point(400, 40);
            this.lblAmigos.Name = "lblAmigos";
            this.lblAmigos.Size = new System.Drawing.Size(150, 30);
            this.lblAmigos.TabIndex = 4;
            this.lblAmigos.Text = "0";
            this.lblAmigos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMensagens
            // 
            this.lblMensagens.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagens.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblMensagens.Location = new System.Drawing.Point(650, 40);
            this.lblMensagens.Name = "lblMensagens";
            this.lblMensagens.Size = new System.Drawing.Size(100, 30);
            this.lblMensagens.TabIndex = 3;
            this.lblMensagens.Text = "0";
            this.lblMensagens.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(150, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 30);
            this.label4.TabIndex = 2;
            this.label4.Text = "📚 Livros";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(400, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "👥 Amigos";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            //   
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(650, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "✉️ Mensagens";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxPerfil
            // 
            this.pictureBoxPerfil.BackColor = System.Drawing.Color.White;
            this.pictureBoxPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPerfil.Location = new System.Drawing.Point(23, 23);
            this.pictureBoxPerfil.Name = "pictureBoxPerfil";
            this.pictureBoxPerfil.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPerfil.TabIndex = 7;
            this.pictureBoxPerfil.TabStop = false;
            this.pictureBoxPerfil.Click += new System.EventHandler(this.pictureBoxPerfil_Click);
            // 
            // lblBio
            // 
            this.lblBio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBio.ForeColor = System.Drawing.Color.DimGray;
            this.lblBio.Location = new System.Drawing.Point(300, 40);
            this.lblBio.Name = "lblBio";
            this.lblBio.Size = new System.Drawing.Size(477, 30);
            this.lblBio.TabIndex = 8;
            this.lblBio.Text = "Bio do usuário será exibida aqui...";
            this.lblBio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBemVindo
            // 
            this.lblBemVindo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBemVindo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblBemVindo.Location = new System.Drawing.Point(80, 30);
            this.lblBemVindo.Name = "lblBemVindo";
            this.lblBemVindo.Size = new System.Drawing.Size(200, 30);
            this.lblBemVindo.TabIndex = 9;
            this.lblBemVindo.Text = "Bem-vindo de volta!";
            this.lblBemVindo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelAtividades
            // 
            this.panelAtividades.BackColor = System.Drawing.Color.White;
            this.panelAtividades.Controls.Add(this.listBoxAtividades);
            this.panelAtividades.Controls.Add(this.label1);
            this.panelAtividades.Location = new System.Drawing.Point(23, 200);
            this.panelAtividades.Name = "panelAtividades";
            this.panelAtividades.Padding = new System.Windows.Forms.Padding(10);
            this.panelAtividades.Size = new System.Drawing.Size(754, 280);
            this.panelAtividades.TabIndex = 10;
            // 
            // listBoxAtividades
            // 
            this.listBoxAtividades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxAtividades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAtividades.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxAtividades.FormattingEnabled = true;
            this.listBoxAtividades.ItemHeight = 17;
            this.listBoxAtividades.Items.AddRange(new object[] {
            "📖 Você adicionou \"O Senhor dos Anéis\" aos favoritos",
            "⭐ Criou uma nova tier list de Fantasia",
            "💬 Comentou em \"Harry Potter e a Pedra Filosofal\"",
            "👥 Adicionou Maria Silva como amiga",
            "📚 Finalizou a leitura de \"1984\""});
            this.listBoxAtividades.Location = new System.Drawing.Point(10, 40);
            this.listBoxAtividades.Name = "listBoxAtividades";
            this.listBoxAtividades.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxAtividades.Size = new System.Drawing.Size(734, 230);
            this.listBoxAtividades.TabIndex = 1;
            this.listBoxAtividades.SelectedIndexChanged += new System.EventHandler(this.listBoxAtividades_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(734, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "📊 Suas Atividades Recentes";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelRodape
            // 
            this.panelRodape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelRodape.Controls.Add(this.labelCopyright);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodape.Location = new System.Drawing.Point(0, 600);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(1000, 40);
            this.panelRodape.TabIndex = 3;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.White;
            this.labelCopyright.Location = new System.Drawing.Point(0, 0);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(1000, 40);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "© 2024 BookConnect - Sua jornada literária começa aqui!";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelRodape);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard - BookConnect";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelLateral.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelEstatisticas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).EndInit();
            this.panelAtividades.ResumeLayout(false);
            this.panelRodape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelRodape;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Button btnLivros;
        private System.Windows.Forms.Button btnMensagens;
        private System.Windows.Forms.Button btnTierList;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.ListBox listBoxAtividades;
        private System.Windows.Forms.PictureBox pictureBoxPerfil;
        private System.Windows.Forms.Label lblBio;
        private System.Windows.Forms.Label lblBemVindo;
        private System.Windows.Forms.Panel panelAtividades;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelEstatisticas;
        private System.Windows.Forms.Label lblLivrosCadastrados;
        private System.Windows.Forms.Label lblAmigos;
        private System.Windows.Forms.Label lblMensagens;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}