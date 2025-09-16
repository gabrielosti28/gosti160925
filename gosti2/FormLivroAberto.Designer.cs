namespace gosti2
{
    partial class FormLivroAberto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLivroAberto));
            this.panelTopo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelComentarios = new System.Windows.Forms.Panel();
            this.flowLayoutPanelComentarios = new System.Windows.Forms.FlowLayoutPanel();
            this.panelNovoComentario = new System.Windows.Forms.Panel();
            this.btnComentar = new System.Windows.Forms.Button();
            this.txtNovoComentario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelInfoLivro = new System.Windows.Forms.Panel();
            this.panelLikes = new System.Windows.Forms.Panel();
            this.btnDislikeLivro = new System.Windows.Forms.Button();
            this.btnLikeLivro = new System.Windows.Forms.Button();
            this.lblDislikes = new System.Windows.Forms.Label();
            this.lblLikes = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblAdicionadoPor = new System.Windows.Forms.Label();
            this.lblGenero = new System.Windows.Forms.Label();
            this.lblAutor = new System.Windows.Forms.Label();
            this.pictureBoxCapa = new System.Windows.Forms.PictureBox();
            this.panelTopo.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelComentarios.SuspendLayout();
            this.panelNovoComentario.SuspendLayout();
            this.panelInfoLivro.SuspendLayout();
            this.panelLikes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapa)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Controls.Add(this.btnFechar);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(1000, 70);
            this.panelTopo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(95, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Título";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.IndianRed;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(860, 15);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(120, 35);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.panelComentarios);
            this.panelConteudo.Controls.Add(this.panelInfoLivro);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 70);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(1000, 680);
            this.panelConteudo.TabIndex = 1;
            // 
            // panelComentarios
            // 
            this.panelComentarios.Controls.Add(this.flowLayoutPanelComentarios);
            this.panelComentarios.Controls.Add(this.panelNovoComentario);
            this.panelComentarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComentarios.Location = new System.Drawing.Point(400, 0);
            this.panelComentarios.Name = "panelComentarios";
            this.panelComentarios.Padding = new System.Windows.Forms.Padding(10);
            this.panelComentarios.Size = new System.Drawing.Size(600, 680);
            this.panelComentarios.TabIndex = 1;
            // 
            // flowLayoutPanelComentarios
            // 
            this.flowLayoutPanelComentarios.AutoScroll = true;
            this.flowLayoutPanelComentarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelComentarios.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelComentarios.Location = new System.Drawing.Point(10, 100);
            this.flowLayoutPanelComentarios.Name = "flowLayoutPanelComentarios";
            this.flowLayoutPanelComentarios.Size = new System.Drawing.Size(580, 570);
            this.flowLayoutPanelComentarios.TabIndex = 1;
            this.flowLayoutPanelComentarios.WrapContents = false;
            // 
            // panelNovoComentario
            // 
            this.panelNovoComentario.Controls.Add(this.btnComentar);
            this.panelNovoComentario.Controls.Add(this.txtNovoComentario);
            this.panelNovoComentario.Controls.Add(this.label4);
            this.panelNovoComentario.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNovoComentario.Location = new System.Drawing.Point(10, 10);
            this.panelNovoComentario.Name = "panelNovoComentario";
            this.panelNovoComentario.Size = new System.Drawing.Size(580, 90);
            this.panelNovoComentario.TabIndex = 0;
            // 
            // btnComentar
            // 
            this.btnComentar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComentar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnComentar.FlatAppearance.BorderSize = 0;
            this.btnComentar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComentar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnComentar.ForeColor = System.Drawing.Color.White;
            this.btnComentar.Location = new System.Drawing.Point(470, 50);
            this.btnComentar.Name = "btnComentar";
            this.btnComentar.Size = new System.Drawing.Size(100, 30);
            this.btnComentar.TabIndex = 2;
            this.btnComentar.Text = "Comentar";
            this.btnComentar.UseVisualStyleBackColor = false;
            this.btnComentar.Click += new System.EventHandler(this.btnComentar_Click);
            // 
            // txtNovoComentario
            // 
            this.txtNovoComentario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNovoComentario.Location = new System.Drawing.Point(10, 30);
            this.txtNovoComentario.Multiline = true;
            this.txtNovoComentario.Name = "txtNovoComentario";
            this.txtNovoComentario.Size = new System.Drawing.Size(450, 50);
            this.txtNovoComentario.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Deixe seu comentário:";
            // 
            // panelInfoLivro
            // 
            this.panelInfoLivro.Controls.Add(this.panelLikes);
            this.panelInfoLivro.Controls.Add(this.txtDescricao);
            this.panelInfoLivro.Controls.Add(this.lblAdicionadoPor);
            this.panelInfoLivro.Controls.Add(this.lblGenero);
            this.panelInfoLivro.Controls.Add(this.lblAutor);
            this.panelInfoLivro.Controls.Add(this.pictureBoxCapa);
            this.panelInfoLivro.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInfoLivro.Location = new System.Drawing.Point(0, 0);
            this.panelInfoLivro.Name = "panelInfoLivro";
            this.panelInfoLivro.Padding = new System.Windows.Forms.Padding(20);
            this.panelInfoLivro.Size = new System.Drawing.Size(400, 680);
            this.panelInfoLivro.TabIndex = 0;
            // 
            // panelLikes
            // 
            this.panelLikes.Controls.Add(this.btnDislikeLivro);
            this.panelLikes.Controls.Add(this.btnLikeLivro);
            this.panelLikes.Controls.Add(this.lblDislikes);
            this.panelLikes.Controls.Add(this.lblLikes);
            this.panelLikes.Controls.Add(this.label3);
            this.panelLikes.Location = new System.Drawing.Point(23, 400);
            this.panelLikes.Name = "panelLikes";
            this.panelLikes.Size = new System.Drawing.Size(354, 100);
            this.panelLikes.TabIndex = 5;
            // 
            // btnDislikeLivro
            // 
            this.btnDislikeLivro.BackColor = System.Drawing.Color.IndianRed;
            this.btnDislikeLivro.FlatAppearance.BorderSize = 0;
            this.btnDislikeLivro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDislikeLivro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDislikeLivro.ForeColor = System.Drawing.Color.White;
            this.btnDislikeLivro.Location = new System.Drawing.Point(200, 40);
            this.btnDislikeLivro.Name = "btnDislikeLivro";
            this.btnDislikeLivro.Size = new System.Drawing.Size(120, 40);
            this.btnDislikeLivro.TabIndex = 4;
            this.btnDislikeLivro.Text = "👎 Dislike";
            this.btnDislikeLivro.UseVisualStyleBackColor = false;
            this.btnDislikeLivro.Click += new System.EventHandler(this.btnDislikeLivro_Click);
            // 
            // btnLikeLivro
            // 
            this.btnLikeLivro.BackColor = System.Drawing.Color.ForestGreen;
            this.btnLikeLivro.FlatAppearance.BorderSize = 0;
            this.btnLikeLivro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLikeLivro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLikeLivro.ForeColor = System.Drawing.Color.White;
            this.btnLikeLivro.Location = new System.Drawing.Point(40, 40);
            this.btnLikeLivro.Name = "btnLikeLivro";
            this.btnLikeLivro.Size = new System.Drawing.Size(120, 40);
            this.btnLikeLivro.TabIndex = 3;
            this.btnLikeLivro.Text = "👍 Like";
            this.btnLikeLivro.UseVisualStyleBackColor = false;
            this.btnLikeLivro.Click += new System.EventHandler(this.btnLikeLivro_Click);
            // 
            // lblDislikes
            // 
            this.lblDislikes.AutoSize = true;
            this.lblDislikes.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDislikes.Location = new System.Drawing.Point(240, 10);
            this.lblDislikes.Name = "lblDislikes";
            this.lblDislikes.Size = new System.Drawing.Size(40, 25);
            this.lblDislikes.TabIndex = 2;
            this.lblDislikes.Text = "👎 0";
            // 
            // lblLikes
            // 
            this.lblLikes.AutoSize = true;
            this.lblLikes.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLikes.Location = new System.Drawing.Point(80, 10);
            this.lblLikes.Name = "lblLikes";
            this.lblLikes.Size = new System.Drawing.Size(40, 25);
            this.lblLikes.TabIndex = 1;
            this.lblLikes.Text = "👍 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Votos: ";
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescricao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescricao.Location = new System.Drawing.Point(23, 250);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.Size = new System.Drawing.Size(354, 130);
            this.txtDescricao.TabIndex = 4;
            this.txtDescricao.Text = "Descrição do livro...";
            // 
            // lblAdicionadoPor
            // 
            this.lblAdicionadoPor.AutoSize = true;
            this.lblAdicionadoPor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAdicionadoPor.Location = new System.Drawing.Point(23, 220);
            this.lblAdicionadoPor.Name = "lblAdicionadoPor";
            this.lblAdicionadoPor.Size = new System.Drawing.Size(109, 19);
            this.lblAdicionadoPor.TabIndex = 3;
            this.lblAdicionadoPor.Text = "Adicionado por: ";
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenero.Location = new System.Drawing.Point(23, 190);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(58, 19);
            this.lblGenero.TabIndex = 2;
            this.lblGenero.Text = "Gênero: ";
            // 
            // lblAutor
            // 
            this.lblAutor.AutoSize = true;
            this.lblAutor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAutor.Location = new System.Drawing.Point(23, 160);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(47, 19);
            this.lblAutor.TabIndex = 1;
            this.lblAutor.Text = "Autor: ";
            // 
            // pictureBoxCapa
            // 
            this.pictureBoxCapa.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxCapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCapa.Location = new System.Drawing.Point(23, 23);
            this.pictureBoxCapa.Name = "pictureBoxCapa";
            this.pictureBoxCapa.Size = new System.Drawing.Size(354, 120);
            this.pictureBoxCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCapa.TabIndex = 0;
            this.pictureBoxCapa.TabStop = false;
            // 
            // FormLivroAberto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelTopo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormLivroAberto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalhes do Livro";
            this.panelTopo.ResumeLayout(false);
            this.panelTopo.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelComentarios.ResumeLayout(false);
            this.panelNovoComentario.ResumeLayout(false);
            this.panelNovoComentario.PerformLayout();
            this.panelInfoLivro.ResumeLayout(false);
            this.panelInfoLivro.PerformLayout();
            this.panelLikes.ResumeLayout(false);
            this.panelLikes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapa)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelInfoLivro;
        private System.Windows.Forms.PictureBox pictureBoxCapa;
        private System.Windows.Forms.Label lblAutor;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblAdicionadoPor;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Panel panelComentarios;
        private System.Windows.Forms.Panel panelNovoComentario;
        private System.Windows.Forms.Button btnComentar;
        private System.Windows.Forms.TextBox txtNovoComentario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelComentarios;
        private System.Windows.Forms.Panel panelLikes;
        private System.Windows.Forms.Button btnDislikeLivro;
        private System.Windows.Forms.Button btnLikeLivro;
        private System.Windows.Forms.Label lblDislikes;
        private System.Windows.Forms.Label lblLikes;
        private System.Windows.Forms.Label label3;
    }
}