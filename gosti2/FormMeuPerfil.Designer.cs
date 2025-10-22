namespace gosti2
{
    partial class FormMeuPerfil
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
            this.panelTopo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.lblNomeUsuario = new System.Windows.Forms.Label();
            this.panelEstatisticas = new System.Windows.Forms.Panel();
            this.lblTotalTierLists = new System.Windows.Forms.Label();
            this.lblTotalComentarios = new System.Windows.Forms.Label();
            this.lblTotalLivros = new System.Windows.Forms.Label();
            this.lblDataLogin = new System.Windows.Forms.Label();
            this.panelUltimosLivros = new System.Windows.Forms.Panel();
            this.panelLivro3 = new System.Windows.Forms.Panel();
            this.lblLivro3 = new System.Windows.Forms.Label();
            this.panelLivro2 = new System.Windows.Forms.Panel();
            this.lblLivro2 = new System.Windows.Forms.Label();
            this.panelLivro1 = new System.Windows.Forms.Panel();
            this.lblLivro1 = new System.Windows.Forms.Label();
            this.lblTituloUltimosLivros = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.picLivro3 = new System.Windows.Forms.PictureBox();
            this.picLivro2 = new System.Windows.Forms.PictureBox();
            this.picLivro1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPerfil = new System.Windows.Forms.PictureBox();
            this.panelTopo.SuspendLayout();
            this.panelCentral.SuspendLayout();
            this.panelEstatisticas.SuspendLayout();
            this.panelUltimosLivros.SuspendLayout();
            this.panelLivro3.SuspendLayout();
            this.panelLivro2.SuspendLayout();
            this.panelLivro1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(900, 60);
            this.panelTopo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(174, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "👤 Meu Perfil";
            // 
            // panelCentral
            // 
            this.panelCentral.BackColor = System.Drawing.Color.White;
            this.panelCentral.Controls.Add(this.txtBio);
            this.panelCentral.Controls.Add(this.lblNomeUsuario);
            this.panelCentral.Controls.Add(this.pictureBoxPerfil);
            this.panelCentral.Location = new System.Drawing.Point(30, 80);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(840, 150);
            this.panelCentral.TabIndex = 1;
            // 
            // txtBio
            // 
            this.txtBio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.txtBio.ForeColor = System.Drawing.Color.Gray;
            this.txtBio.Location = new System.Drawing.Point(450, 20);
            this.txtBio.Multiline = true;
            this.txtBio.Name = "txtBio";
            this.txtBio.ReadOnly = true;
            this.txtBio.Size = new System.Drawing.Size(370, 110);
            this.txtBio.TabIndex = 2;
            this.txtBio.Text = "Bio do usuário...";
            // 
            // lblNomeUsuario
            // 
            this.lblNomeUsuario.AutoSize = true;
            this.lblNomeUsuario.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblNomeUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblNomeUsuario.Location = new System.Drawing.Point(155, 55);
            this.lblNomeUsuario.Name = "lblNomeUsuario";
            this.lblNomeUsuario.Size = new System.Drawing.Size(160, 30);
            this.lblNomeUsuario.TabIndex = 1;
            this.lblNomeUsuario.Text = "Nome Usuário";
            // 
            // panelEstatisticas
            // 
            this.panelEstatisticas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEstatisticas.Controls.Add(this.lblTotalTierLists);
            this.panelEstatisticas.Controls.Add(this.lblTotalComentarios);
            this.panelEstatisticas.Controls.Add(this.lblTotalLivros);
            this.panelEstatisticas.Controls.Add(this.lblDataLogin);
            this.panelEstatisticas.Location = new System.Drawing.Point(30, 250);
            this.panelEstatisticas.Name = "panelEstatisticas";
            this.panelEstatisticas.Size = new System.Drawing.Size(840, 120);
            this.panelEstatisticas.TabIndex = 2;
            // 
            // lblTotalTierLists
            // 
            this.lblTotalTierLists.AutoSize = true;
            this.lblTotalTierLists.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalTierLists.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalTierLists.Location = new System.Drawing.Point(450, 50);
            this.lblTotalTierLists.Name = "lblTotalTierLists";
            this.lblTotalTierLists.Size = new System.Drawing.Size(174, 20);
            this.lblTotalTierLists.TabIndex = 3;
            this.lblTotalTierLists.Text = "⭐ Tier Lists: 0 (Em breve)";
            // 
            // lblTotalComentarios
            // 
            this.lblTotalComentarios.AutoSize = true;
            this.lblTotalComentarios.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalComentarios.Location = new System.Drawing.Point(30, 80);
            this.lblTotalComentarios.Name = "lblTotalComentarios";
            this.lblTotalComentarios.Size = new System.Drawing.Size(133, 20);
            this.lblTotalComentarios.TabIndex = 2;
            this.lblTotalComentarios.Text = "💬 Comentários: 0";
            // 
            // lblTotalLivros
            // 
            this.lblTotalLivros.AutoSize = true;
            this.lblTotalLivros.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalLivros.Location = new System.Drawing.Point(30, 50);
            this.lblTotalLivros.Name = "lblTotalLivros";
            this.lblTotalLivros.Size = new System.Drawing.Size(172, 20);
            this.lblTotalLivros.TabIndex = 1;
            this.lblTotalLivros.Text = "📚 Livros adicionados: 0";
            // 
            // lblDataLogin
            // 
            this.lblDataLogin.AutoSize = true;
            this.lblDataLogin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDataLogin.Location = new System.Drawing.Point(30, 20);
            this.lblDataLogin.Name = "lblDataLogin";
            this.lblDataLogin.Size = new System.Drawing.Size(200, 20);
            this.lblDataLogin.TabIndex = 0;
            this.lblDataLogin.Text = "📅 Último login: 00/00/0000";
            // 
            // panelUltimosLivros
            // 
            this.panelUltimosLivros.BackColor = System.Drawing.Color.White;
            this.panelUltimosLivros.Controls.Add(this.panelLivro3);
            this.panelUltimosLivros.Controls.Add(this.panelLivro2);
            this.panelUltimosLivros.Controls.Add(this.panelLivro1);
            this.panelUltimosLivros.Controls.Add(this.lblTituloUltimosLivros);
            this.panelUltimosLivros.Location = new System.Drawing.Point(30, 390);
            this.panelUltimosLivros.Name = "panelUltimosLivros";
            this.panelUltimosLivros.Size = new System.Drawing.Size(840, 180);
            this.panelUltimosLivros.TabIndex = 3;
            // 
            // panelLivro3
            // 
            this.panelLivro3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLivro3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLivro3.Controls.Add(this.lblLivro3);
            this.panelLivro3.Controls.Add(this.picLivro3);
            this.panelLivro3.Location = new System.Drawing.Point(564, 50);
            this.panelLivro3.Name = "panelLivro3";
            this.panelLivro3.Size = new System.Drawing.Size(250, 110);
            this.panelLivro3.TabIndex = 3;
            // 
            // lblLivro3
            // 
            this.lblLivro3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLivro3.Location = new System.Drawing.Point(90, 10);
            this.lblLivro3.Name = "lblLivro3";
            this.lblLivro3.Size = new System.Drawing.Size(150, 90);
            this.lblLivro3.TabIndex = 1;
            this.lblLivro3.Text = "Título do livro...";
            // 
            // panelLivro2
            // 
            this.panelLivro2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLivro2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLivro2.Controls.Add(this.lblLivro2);
            this.panelLivro2.Controls.Add(this.picLivro2);
            this.panelLivro2.Location = new System.Drawing.Point(294, 50);
            this.panelLivro2.Name = "panelLivro2";
            this.panelLivro2.Size = new System.Drawing.Size(250, 110);
            this.panelLivro2.TabIndex = 2;
            // 
            // lblLivro2
            // 
            this.lblLivro2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLivro2.Location = new System.Drawing.Point(90, 10);
            this.lblLivro2.Name = "lblLivro2";
            this.lblLivro2.Size = new System.Drawing.Size(150, 90);
            this.lblLivro2.TabIndex = 1;
            this.lblLivro2.Text = "Título do livro...";
            // 
            // panelLivro1
            // 
            this.panelLivro1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLivro1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLivro1.Controls.Add(this.lblLivro1);
            this.panelLivro1.Controls.Add(this.picLivro1);
            this.panelLivro1.Location = new System.Drawing.Point(24, 50);
            this.panelLivro1.Name = "panelLivro1";
            this.panelLivro1.Size = new System.Drawing.Size(250, 110);
            this.panelLivro1.TabIndex = 1;
            // 
            // lblLivro1
            // 
            this.lblLivro1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLivro1.Location = new System.Drawing.Point(90, 10);
            this.lblLivro1.Name = "lblLivro1";
            this.lblLivro1.Size = new System.Drawing.Size(150, 90);
            this.lblLivro1.TabIndex = 1;
            this.lblLivro1.Text = "Título do livro...";
            // 
            // lblTituloUltimosLivros
            // 
            this.lblTituloUltimosLivros.AutoSize = true;
            this.lblTituloUltimosLivros.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloUltimosLivros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblTituloUltimosLivros.Location = new System.Drawing.Point(20, 15);
            this.lblTituloUltimosLivros.Name = "lblTituloUltimosLivros";
            this.lblTituloUltimosLivros.Size = new System.Drawing.Size(253, 21);
            this.lblTituloUltimosLivros.TabIndex = 0;
            this.lblTituloUltimosLivros.Text = "📖 Últimos 3 livros adicionados";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(750, 590);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(120, 35);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "✕ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // picLivro3
            // 
            this.picLivro3.BackColor = System.Drawing.Color.LightGray;
            this.picLivro3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLivro3.Location = new System.Drawing.Point(10, 10);
            this.picLivro3.Name = "picLivro3";
            this.picLivro3.Size = new System.Drawing.Size(70, 90);
            this.picLivro3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLivro3.TabIndex = 0;
            this.picLivro3.TabStop = false;
            // 
            // picLivro2
            // 
            this.picLivro2.BackColor = System.Drawing.Color.LightGray;
            this.picLivro2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLivro2.Location = new System.Drawing.Point(10, 10);
            this.picLivro2.Name = "picLivro2";
            this.picLivro2.Size = new System.Drawing.Size(70, 90);
            this.picLivro2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLivro2.TabIndex = 0;
            this.picLivro2.TabStop = false;
            // 
            // picLivro1
            // 
            this.picLivro1.BackColor = System.Drawing.Color.LightGray;
            this.picLivro1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLivro1.Location = new System.Drawing.Point(10, 10);
            this.picLivro1.Name = "picLivro1";
            this.picLivro1.Size = new System.Drawing.Size(70, 90);
            this.picLivro1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLivro1.TabIndex = 0;
            this.picLivro1.TabStop = false;
            // 
            // pictureBoxPerfil
            // 
            this.pictureBoxPerfil.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPerfil.Location = new System.Drawing.Point(24, 20);
            this.pictureBoxPerfil.Name = "pictureBoxPerfil";
            this.pictureBoxPerfil.Size = new System.Drawing.Size(110, 110);
            this.pictureBoxPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPerfil.TabIndex = 0;
            this.pictureBoxPerfil.TabStop = false;
            this.pictureBoxPerfil.Click += new System.EventHandler(this.pictureBoxPerfil_Click);
            // 
            // FormMeuPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.panelUltimosLivros);
            this.Controls.Add(this.panelEstatisticas);
            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.panelTopo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMeuPerfil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Meu Perfil - BookConnect";
            this.panelTopo.ResumeLayout(false);
            this.panelTopo.PerformLayout();
            this.panelCentral.ResumeLayout(false);
            this.panelCentral.PerformLayout();
            this.panelEstatisticas.ResumeLayout(false);
            this.panelEstatisticas.PerformLayout();
            this.panelUltimosLivros.ResumeLayout(false);
            this.panelUltimosLivros.PerformLayout();
            this.panelLivro3.ResumeLayout(false);
            this.panelLivro2.ResumeLayout(false);
            this.panelLivro1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLivro3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.PictureBox pictureBoxPerfil;
        private System.Windows.Forms.Label lblNomeUsuario;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Panel panelEstatisticas;
        private System.Windows.Forms.Label lblDataLogin;
        private System.Windows.Forms.Label lblTotalLivros;
        private System.Windows.Forms.Label lblTotalComentarios;
        private System.Windows.Forms.Label lblTotalTierLists;
        private System.Windows.Forms.Panel panelUltimosLivros;
        private System.Windows.Forms.Label lblTituloUltimosLivros;
        private System.Windows.Forms.Panel panelLivro1;
        private System.Windows.Forms.PictureBox picLivro1;
        private System.Windows.Forms.Label lblLivro1;
        private System.Windows.Forms.Panel panelLivro2;
        private System.Windows.Forms.PictureBox picLivro2;
        private System.Windows.Forms.Label lblLivro2;
        private System.Windows.Forms.Panel panelLivro3;
        private System.Windows.Forms.PictureBox picLivro3;
        private System.Windows.Forms.Label lblLivro3;
        private System.Windows.Forms.Button btnFechar;
    }
}