namespace gosti2
{
    partial class infoTela
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelInformacoes = new System.Windows.Forms.Panel();
            this.labelFuncionalidades = new System.Windows.Forms.Label();
            this.labelDescricao = new System.Windows.Forms.Label();
            this.panelFuncionalidades = new System.Windows.Forms.Panel();
            this.listBoxFuncionalidades = new System.Windows.Forms.ListBox();
            this.panelComoUsar = new System.Windows.Forms.Panel();
            this.labelComoUsar = new System.Windows.Forms.Label();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.panelRodape = new System.Windows.Forms.Panel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.panelSuperior.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelInformacoes.SuspendLayout();
            this.panelFuncionalidades.SuspendLayout();
            this.panelComoUsar.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.panelRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelSuperior.Controls.Add(this.labelTitulo);
            this.panelSuperior.Controls.Add(this.panelLogo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 100);
            this.panelSuperior.TabIndex = 0;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.ForeColor = System.Drawing.Color.White;
            this.labelTitulo.Location = new System.Drawing.Point(100, 35);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(522, 37);
            this.labelTitulo.TabIndex = 1;
            this.labelTitulo.Text = "Informações do Sistema - BookConnect";
            // 
            // panelLogo
            // 
            this.panelLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelLogo.Location = new System.Drawing.Point(30, 20);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(60, 60);
            this.panelLogo.TabIndex = 0;
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelConteudo.Controls.Add(this.panelInformacoes);
            this.panelConteudo.Controls.Add(this.panelFuncionalidades);
            this.panelConteudo.Controls.Add(this.panelComoUsar);
            this.panelConteudo.Controls.Add(this.panelBotoes);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 100);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(20);
            this.panelConteudo.Size = new System.Drawing.Size(800, 550);
            this.panelConteudo.TabIndex = 1;
            // 
            // panelInformacoes
            // 
            this.panelInformacoes.Controls.Add(this.labelFuncionalidades);
            this.panelInformacoes.Controls.Add(this.labelDescricao);
            this.panelInformacoes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInformacoes.Location = new System.Drawing.Point(20, 340);
            this.panelInformacoes.Name = "panelInformacoes";
            this.panelInformacoes.Padding = new System.Windows.Forms.Padding(10);
            this.panelInformacoes.Size = new System.Drawing.Size(760, 120);
            this.panelInformacoes.TabIndex = 4;
            // 
            // labelFuncionalidades
            // 
            this.labelFuncionalidades.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelFuncionalidades.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFuncionalidades.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.labelFuncionalidades.Location = new System.Drawing.Point(10, 60);
            this.labelFuncionalidades.Name = "labelFuncionalidades";
            this.labelFuncionalidades.Size = new System.Drawing.Size(740, 50);
            this.labelFuncionalidades.TabIndex = 1;
            this.labelFuncionalidades.Text = "📚 Sistema de Rede Social para amantes de livros - Compartilhe, descubra e conect" +
    "e-se com outros leitores!";
            this.labelFuncionalidades.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDescricao
            // 
            this.labelDescricao.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDescricao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescricao.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelDescricao.Location = new System.Drawing.Point(10, 10);
            this.labelDescricao.Name = "labelDescricao";
            this.labelDescricao.Size = new System.Drawing.Size(740, 50);
            this.labelDescricao.TabIndex = 0;
            this.labelDescricao.Text = "🌟 BookConnect é uma plataforma onde leitores podem compartilhar suas experiência" +
    "s literárias favoritas.";
            this.labelDescricao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFuncionalidades
            // 
            this.panelFuncionalidades.Controls.Add(this.listBoxFuncionalidades);
            this.panelFuncionalidades.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFuncionalidades.Location = new System.Drawing.Point(20, 140);
            this.panelFuncionalidades.Name = "panelFuncionalidades";
            this.panelFuncionalidades.Padding = new System.Windows.Forms.Padding(10);
            this.panelFuncionalidades.Size = new System.Drawing.Size(760, 200);
            this.panelFuncionalidades.TabIndex = 5;
            // 
            // listBoxFuncionalidades
            // 
            this.listBoxFuncionalidades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxFuncionalidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFuncionalidades.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxFuncionalidades.FormattingEnabled = true;
            this.listBoxFuncionalidades.ItemHeight = 17;
            this.listBoxFuncionalidades.Items.AddRange(new object[] {
            "✅ Cadastro de usuários com perfil personalizado",
            "✅ Adição de livros com capa, descrição e categoria",
            "✅ Sistema de tier lists para ranking de livros",
            "✅ Mensagens privadas entre usuários",
            "✅ Comentários e discussões sobre livros",
            "✅ Interface intuitiva e amigável",
            "✅ Banco de dados seguro SQL Server",
            "✅ Navegação fácil entre as funcionalidades"});
            this.listBoxFuncionalidades.Location = new System.Drawing.Point(10, 10);
            this.listBoxFuncionalidades.Name = "listBoxFuncionalidades";
            this.listBoxFuncionalidades.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxFuncionalidades.Size = new System.Drawing.Size(740, 180);
            this.listBoxFuncionalidades.TabIndex = 0;
            // 
            // panelComoUsar
            // 
            this.panelComoUsar.Controls.Add(this.labelComoUsar);
            this.panelComoUsar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelComoUsar.Location = new System.Drawing.Point(20, 20);
            this.panelComoUsar.Name = "panelComoUsar";
            this.panelComoUsar.Padding = new System.Windows.Forms.Padding(10);
            this.panelComoUsar.Size = new System.Drawing.Size(760, 120);
            this.panelComoUsar.TabIndex = 6;
            // 
            // labelComoUsar
            // 
            this.labelComoUsar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelComoUsar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComoUsar.ForeColor = System.Drawing.Color.DimGray;
            this.labelComoUsar.Location = new System.Drawing.Point(10, 10);
            this.labelComoUsar.Name = "labelComoUsar";
            this.labelComoUsar.Size = new System.Drawing.Size(740, 100);
            this.labelComoUsar.TabIndex = 0;
            // 
            // panelBotoes
            // 
            this.panelBotoes.Controls.Add(this.btnVoltar);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(20, 480);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoes.Size = new System.Drawing.Size(760, 50);
            this.panelBotoes.TabIndex = 7;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnVoltar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Location = new System.Drawing.Point(610, 10);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(140, 30);
            this.btnVoltar.TabIndex = 0;
            this.btnVoltar.Text = "↩️ Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click_1);
            // 
            // panelRodape
            // 
            this.panelRodape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelRodape.Controls.Add(this.labelCopyright);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodape.Location = new System.Drawing.Point(0, 650);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(800, 40);
            this.panelRodape.TabIndex = 2;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.White;
            this.labelCopyright.Location = new System.Drawing.Point(0, 0);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(800, 40);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "© 2024 BookConnect - Desenvolvido com 💙 para a comunidade literária";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoTela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 690);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelRodape);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "infoTela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informações do Sistema - BookConnect";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelInformacoes.ResumeLayout(false);
            this.panelFuncionalidades.ResumeLayout(false);
            this.panelComoUsar.ResumeLayout(false);
            this.panelBotoes.ResumeLayout(false);
            this.panelRodape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelRodape;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Panel panelInformacoes;
        private System.Windows.Forms.Label labelDescricao;
        private System.Windows.Forms.Label labelFuncionalidades;
        private System.Windows.Forms.Panel panelFuncionalidades;
        private System.Windows.Forms.ListBox listBoxFuncionalidades;
        private System.Windows.Forms.Panel panelComoUsar;
        private System.Windows.Forms.Label labelComoUsar;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnVoltar;
    }
}