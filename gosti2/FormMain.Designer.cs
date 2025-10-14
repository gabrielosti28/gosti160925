namespace gosti2
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        //private System.Windows.Forms.Timer timerAtualizacao;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                timerAtualizacao?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.linkSobre = new System.Windows.Forms.LinkLabel();
            this.lblHoraAtual = new System.Windows.Forms.Label();
            this.lblStatusBanco = new System.Windows.Forms.Label();
            this.lblEstatisticas = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblBoasVindas = new System.Windows.Forms.Label();
            this.btnConfiguracoes = new System.Windows.Forms.Button();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnExplorar = new System.Windows.Forms.Button();
            this.btnMeusLivros = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.panelRodape = new System.Windows.Forms.Panel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.timerAtualizacao = new System.Windows.Forms.Timer(this.components);
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelConteudo.SuspendLayout();
            this.panelRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelSuperior.Controls.Add(this.labelTitulo);
            this.panelSuperior.Controls.Add(this.pictureBoxLogo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 100);
            this.panelSuperior.TabIndex = 0;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.ForeColor = System.Drawing.Color.White;
            this.labelTitulo.Location = new System.Drawing.Point(100, 35);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(350, 32);
            this.labelTitulo.TabIndex = 1;
            this.labelTitulo.Text = "📚 BookConnect - Dashboard";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::gosti2.Properties.Resources.default_book_cover;
            this.pictureBoxLogo.Location = new System.Drawing.Point(30, 20);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelConteudo.Controls.Add(this.linkSobre);
            this.panelConteudo.Controls.Add(this.lblHoraAtual);
            this.panelConteudo.Controls.Add(this.lblStatusBanco);
            this.panelConteudo.Controls.Add(this.lblEstatisticas);
            this.panelConteudo.Controls.Add(this.lblEmail);
            this.panelConteudo.Controls.Add(this.lblBoasVindas);
            this.panelConteudo.Controls.Add(this.btnConfiguracoes);
            this.panelConteudo.Controls.Add(this.btnPerfil);
            this.panelConteudo.Controls.Add(this.btnExplorar);
            this.panelConteudo.Controls.Add(this.btnMeusLivros);
            this.panelConteudo.Controls.Add(this.btnSair);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 100);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(30);
            this.panelConteudo.Size = new System.Drawing.Size(800, 450);
            this.panelConteudo.TabIndex = 1;
            // 
            // linkSobre
            // 
            this.linkSobre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkSobre.AutoSize = true;
            this.linkSobre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSobre.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.linkSobre.Location = new System.Drawing.Point(30, 410);
            this.linkSobre.Name = "linkSobre";
            this.linkSobre.Size = new System.Drawing.Size(85, 15);
            this.linkSobre.TabIndex = 10;
            this.linkSobre.TabStop = true;
            this.linkSobre.Text = "📋 Sobre o App";
           // this.linkSobre.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSobre_LinkClicked);
            // 
            // lblHoraAtual
            // 
            this.lblHoraAtual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHoraAtual.AutoSize = true;
            this.lblHoraAtual.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraAtual.ForeColor = System.Drawing.Color.Gray;
            this.lblHoraAtual.Location = new System.Drawing.Point(700, 410);
            this.lblHoraAtual.Name = "lblHoraAtual";
            this.lblHoraAtual.Size = new System.Drawing.Size(70, 15);
            this.lblHoraAtual.TabIndex = 9;
            this.lblHoraAtual.Text = "00:00:00";
            this.lblHoraAtual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusBanco
            // 
            this.lblStatusBanco.AutoSize = true;
            this.lblStatusBanco.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusBanco.Location = new System.Drawing.Point(30, 120);
            this.lblStatusBanco.Name = "lblStatusBanco";
            this.lblStatusBanco.Size = new System.Drawing.Size(150, 15);
            this.lblStatusBanco.TabIndex = 8;
            this.lblStatusBanco.Text = "🔄 Verificando banco...";
            // 
            // lblEstatisticas
            // 
            this.lblEstatisticas.AutoSize = true;
            this.lblEstatisticas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstatisticas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblEstatisticas.Location = new System.Drawing.Point(30, 100);
            this.lblEstatisticas.Name = "lblEstatisticas";
            this.lblEstatisticas.Size = new System.Drawing.Size(250, 19);
            this.lblEstatisticas.TabIndex = 7;
            this.lblEstatisticas.Text = "📚 Carregando suas estatísticas...";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(30, 70);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(200, 20);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "📧 Carregando informações...";
            // 
            // lblBoasVindas
            // 
            this.lblBoasVindas.AutoSize = true;
            this.lblBoasVindas.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoasVindas.Location = new System.Drawing.Point(30, 30);
            this.lblBoasVindas.Name = "lblBoasVindas";
            this.lblBoasVindas.Size = new System.Drawing.Size(300, 30);
            this.lblBoasVindas.TabIndex = 5;
            this.lblBoasVindas.Text = "🌟 Bem-vindo ao BookConnect!";
            //this.lblBoasVindas.Click += new System.EventHandler(this.lblBoasVindas_Click);
            // 
            // btnConfiguracoes
            // 
            this.btnConfiguracoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(160)))), ((int)(((byte)(90)))));
            this.btnConfiguracoes.FlatAppearance.BorderSize = 0;
            this.btnConfiguracoes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(140)))), ((int)(((byte)(70)))));
            this.btnConfiguracoes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(100)))));
            this.btnConfiguracoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracoes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracoes.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracoes.Location = new System.Drawing.Point(500, 250);
            this.btnConfiguracoes.Name = "btnConfiguracoes";
            this.btnConfiguracoes.Size = new System.Drawing.Size(250, 60);
            this.btnConfiguracoes.TabIndex = 4;
            this.btnConfiguracoes.Text = "⚙️ Configurações";
            this.btnConfiguracoes.UseVisualStyleBackColor = false;
            //this.btnConfiguracoes.Click += new System.EventHandler(this.btnConfiguracoes_Click);
            // 
            // btnPerfil
            // 
            this.btnPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnPerfil.FlatAppearance.BorderSize = 0;
            this.btnPerfil.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(120)))), ((int)(((byte)(170)))));
            this.btnPerfil.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.btnPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerfil.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerfil.ForeColor = System.Drawing.Color.White;
            this.btnPerfil.Location = new System.Drawing.Point(500, 170);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(250, 60);
            this.btnPerfil.TabIndex = 3;
            this.btnPerfil.Text = "👤 Meu Perfil";
            this.btnPerfil.UseVisualStyleBackColor = false;
            //this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click);
            // 
            // btnExplorar
            // 
            this.btnExplorar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnExplorar.FlatAppearance.BorderSize = 0;
            this.btnExplorar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(120)))), ((int)(((byte)(170)))));
            this.btnExplorar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.btnExplorar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExplorar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExplorar.ForeColor = System.Drawing.Color.White;
            this.btnExplorar.Location = new System.Drawing.Point(500, 90);
            this.btnExplorar.Name = "btnExplorar";
            this.btnExplorar.Size = new System.Drawing.Size(250, 60);
            this.btnExplorar.TabIndex = 2;
            this.btnExplorar.Text = "🔍 Explorar Livros";
            this.btnExplorar.UseVisualStyleBackColor = false;
           // this.btnExplorar.Click += new System.EventHandler(this.btnExplorar_Click);
            // 
            // btnMeusLivros
            // 
            this.btnMeusLivros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnMeusLivros.FlatAppearance.BorderSize = 0;
            this.btnMeusLivros.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(120)))), ((int)(((byte)(170)))));
            this.btnMeusLivros.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(150)))), ((int)(((byte)(220)))));
            this.btnMeusLivros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           // this.btnMeusLivros.Font = new System.Windows.Forms.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeusLivros.ForeColor = System.Drawing.Color.White;
            this.btnMeusLivros.Location = new System.Drawing.Point(500, 10);
            this.btnMeusLivros.Name = "btnMeusLivros";
            this.btnMeusLivros.Size = new System.Drawing.Size(250, 60);
            this.btnMeusLivros.TabIndex = 1;
            this.btnMeusLivros.Text = "📚 Meus Livros";
            this.btnMeusLivros.UseVisualStyleBackColor = false;
            this.btnMeusLivros.Click += new System.EventHandler(this.btnMeusLivros_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(500, 330);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(250, 60);
            this.btnSair.TabIndex = 5;
            this.btnSair.Text = "🚪 Sair do Sistema";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panelRodape
            // 
            this.panelRodape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelRodape.Controls.Add(this.labelCopyright);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodape.Location = new System.Drawing.Point(0, 550);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(800, 50);
            this.panelRodape.TabIndex = 2;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.White;
            this.labelCopyright.Location = new System.Drawing.Point(0, 0);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(800, 50);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "© 2024 BookConnect - Sua rede social literária favorita";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerAtualizacao
            // 
            this.timerAtualizacao.Enabled = true;
            this.timerAtualizacao.Interval = 1000;
           // this.timerAtualizacao.Tick += new System.EventHandler(this.timerAtualizacao_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelRodape);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BookConnect - Dashboard";
           // this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.panelRodape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelRodape;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnMeusLivros;
        private System.Windows.Forms.Button btnExplorar;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Button btnConfiguracoes;
        private System.Windows.Forms.Label lblBoasVindas;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblEstatisticas;
        private System.Windows.Forms.Label lblStatusBanco;
        private System.Windows.Forms.Label lblHoraAtual;
        private System.Windows.Forms.LinkLabel linkSobre;
        private System.Windows.Forms.Timer timerAtualizacao;
    }
}