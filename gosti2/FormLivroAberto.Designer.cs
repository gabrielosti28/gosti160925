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
            this.components = new System.ComponentModel.Container();
            this.panelTopo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelComentarios = new System.Windows.Forms.Panel();
            this.panelNovoComentario = new System.Windows.Forms.Panel();
            this.lblContadorCaracteres = new System.Windows.Forms.Label();
            this.btnComentar = new System.Windows.Forms.Button();
            this.txtNovoComentario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelInfoLivro = new System.Windows.Forms.Panel();
            this.lblAprovacao = new System.Windows.Forms.Label();
            this.lblInfoAdicional = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblAdicionadoPor = new System.Windows.Forms.Label();
            this.lblGenero = new System.Windows.Forms.Label();
            this.lblAutor = new System.Windows.Forms.Label();
            this.pictureBoxCapa = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanelComentarios = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTopo.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelComentarios.SuspendLayout();
            this.panelNovoComentario.SuspendLayout();
            this.panelInfoLivro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapa)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Controls.Add(this.btnFechar);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(1200, 80);
            this.panelTopo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.MaximumSize = new System.Drawing.Size(800, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(88, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Título";
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(35)))), ((int)(((byte)(51)))));
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(58)))));
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(1060, 20);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(120, 40);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "✕ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.panelComentarios);
            this.panelConteudo.Controls.Add(this.panelInfoLivro);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 80);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(1200, 720);
            this.panelConteudo.TabIndex = 1;
            // 
            // panelComentarios
            // 
            this.panelComentarios.Controls.Add(this.flowLayoutPanelComentarios);
            this.panelComentarios.Controls.Add(this.panelNovoComentario);
            this.panelComentarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComentarios.Location = new System.Drawing.Point(450, 0);
            this.panelComentarios.Name = "panelComentarios";
            this.panelComentarios.Padding = new System.Windows.Forms.Padding(15);
            this.panelComentarios.Size = new System.Drawing.Size(750, 720);
            this.panelComentarios.TabIndex = 1;
            // 
            // panelNovoComentario
            // 
            this.panelNovoComentario.BackColor = System.Drawing.Color.White;
            this.panelNovoComentario.Controls.Add(this.lblContadorCaracteres);
            this.panelNovoComentario.Controls.Add(this.btnComentar);
            this.panelNovoComentario.Controls.Add(this.txtNovoComentario);
            this.panelNovoComentario.Controls.Add(this.label4);
            this.panelNovoComentario.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNovoComentario.Location = new System.Drawing.Point(15, 15);
            this.panelNovoComentario.Name = "panelNovoComentario";
            this.panelNovoComentario.Padding = new System.Windows.Forms.Padding(10);
            this.panelNovoComentario.Size = new System.Drawing.Size(720, 115);
            this.panelNovoComentario.TabIndex = 0;
            // 
            // lblContadorCaracteres
            // 
            this.lblContadorCaracteres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContadorCaracteres.AutoSize = true;
            this.lblContadorCaracteres.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblContadorCaracteres.ForeColor = System.Drawing.Color.Gray;
            this.lblContadorCaracteres.Location = new System.Drawing.Point(590, 13);
            this.lblContadorCaracteres.Name = "lblContadorCaracteres";
            this.lblContadorCaracteres.Size = new System.Drawing.Size(41, 13);
            this.lblContadorCaracteres.TabIndex = 3;
            this.lblContadorCaracteres.Text = "0/2000";
            // 
            // btnComentar
            // 
            this.btnComentar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComentar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnComentar.FlatAppearance.BorderSize = 0;
            this.btnComentar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(160)))));
            this.btnComentar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.btnComentar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComentar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnComentar.ForeColor = System.Drawing.Color.White;
            this.btnComentar.Location = new System.Drawing.Point(590, 70);
            this.btnComentar.Name = "btnComentar";
            this.btnComentar.Size = new System.Drawing.Size(120, 35);
            this.btnComentar.TabIndex = 2;
            this.btnComentar.Text = "💬 Comentar";
            this.btnComentar.UseVisualStyleBackColor = false;
            this.btnComentar.Click += new System.EventHandler(this.btnComentar_Click);
            // 
            // txtNovoComentario
            // 
            this.txtNovoComentario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNovoComentario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNovoComentario.Location = new System.Drawing.Point(13, 35);
            this.txtNovoComentario.Multiline = true;
            this.txtNovoComentario.Name = "txtNovoComentario";
            this.txtNovoComentario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNovoComentario.Size = new System.Drawing.Size(570, 70);
            this.txtNovoComentario.TabIndex = 1;
            this.txtNovoComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNovoComentario_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Deixe seu comentário:";
            // 
            // panelInfoLivro
            // 
            this.panelInfoLivro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelInfoLivro.Controls.Add(this.lblAprovacao);
            this.panelInfoLivro.Controls.Add(this.lblInfoAdicional);
            this.panelInfoLivro.Controls.Add(this.txtDescricao);
            this.panelInfoLivro.Controls.Add(this.lblAdicionadoPor);
            this.panelInfoLivro.Controls.Add(this.lblGenero);
            this.panelInfoLivro.Controls.Add(this.lblAutor);
            this.panelInfoLivro.Controls.Add(this.pictureBoxCapa);
            this.panelInfoLivro.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInfoLivro.Location = new System.Drawing.Point(0, 0);
            this.panelInfoLivro.Name = "panelInfoLivro";
            this.panelInfoLivro.Padding = new System.Windows.Forms.Padding(25);
            this.panelInfoLivro.Size = new System.Drawing.Size(450, 720);
            this.panelInfoLivro.TabIndex = 0;
            // 
            // lblAprovacao
            // 
            this.lblAprovacao.AutoSize = true;
            this.lblAprovacao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblAprovacao.ForeColor = System.Drawing.Color.Green;
            this.lblAprovacao.Location = new System.Drawing.Point(28, 610);
            this.lblAprovacao.Name = "lblAprovacao";
            this.lblAprovacao.Size = new System.Drawing.Size(0, 15);
            this.lblAprovacao.TabIndex = 7;
            this.lblAprovacao.Visible = false;
            // 
            // lblInfoAdicional
            // 
            this.lblInfoAdicional.AutoSize = true;
            this.lblInfoAdicional.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoAdicional.Location = new System.Drawing.Point(28, 630);
            this.lblInfoAdicional.MaximumSize = new System.Drawing.Size(394, 0);
            this.lblInfoAdicional.Name = "lblInfoAdicional";
            this.lblInfoAdicional.Size = new System.Drawing.Size(0, 15);
            this.lblInfoAdicional.TabIndex = 6;
            this.lblInfoAdicional.Visible = false;
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescricao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescricao.Location = new System.Drawing.Point(28, 259);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.Size = new System.Drawing.Size(394, 416);
            this.txtDescricao.TabIndex = 4;
            this.txtDescricao.Text = "Carregando descrição...";
            // 
            // lblAdicionadoPor
            // 
            this.lblAdicionadoPor.AutoSize = true;
            this.lblAdicionadoPor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAdicionadoPor.Location = new System.Drawing.Point(28, 237);
            this.lblAdicionadoPor.Name = "lblAdicionadoPor";
            this.lblAdicionadoPor.Size = new System.Drawing.Size(109, 19);
            this.lblAdicionadoPor.TabIndex = 3;
            this.lblAdicionadoPor.Text = "Adicionado por: ";
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenero.Location = new System.Drawing.Point(28, 218);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(61, 19);
            this.lblGenero.TabIndex = 2;
            this.lblGenero.Text = "Gênero: ";
            // 
            // lblAutor
            // 
            this.lblAutor.AutoSize = true;
            this.lblAutor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAutor.Location = new System.Drawing.Point(27, 199);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(51, 19);
            this.lblAutor.TabIndex = 1;
            this.lblAutor.Text = "Autor: ";
            // 
            // pictureBoxCapa
            // 
            this.pictureBoxCapa.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxCapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCapa.Location = new System.Drawing.Point(28, 28);
            this.pictureBoxCapa.Name = "pictureBoxCapa";
            this.pictureBoxCapa.Size = new System.Drawing.Size(394, 168);
            this.pictureBoxCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCapa.TabIndex = 0;
            this.pictureBoxCapa.TabStop = false;
            // 
            // flowLayoutPanelComentarios
            // 
            this.flowLayoutPanelComentarios.AutoScroll = true;
            this.flowLayoutPanelComentarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.flowLayoutPanelComentarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelComentarios.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelComentarios.Location = new System.Drawing.Point(15, 130);
            this.flowLayoutPanelComentarios.Name = "flowLayoutPanelComentarios";
            this.flowLayoutPanelComentarios.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanelComentarios.Size = new System.Drawing.Size(720, 575);
            this.flowLayoutPanelComentarios.TabIndex = 2;
            this.flowLayoutPanelComentarios.WrapContents = false;
            // 
            // FormLivroAberto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelTopo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "FormLivroAberto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalhes do Livro - BookConnect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLivroAberto_FormClosing);
            this.panelTopo.ResumeLayout(false);
            this.panelTopo.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelComentarios.ResumeLayout(false);
            this.panelNovoComentario.ResumeLayout(false);
            this.panelNovoComentario.PerformLayout();
            this.panelInfoLivro.ResumeLayout(false);
            this.panelInfoLivro.PerformLayout();
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
        private System.Windows.Forms.Label lblContadorCaracteres;
        private System.Windows.Forms.Label lblInfoAdicional;
        private System.Windows.Forms.Label lblAprovacao;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelComentarios;
    }
}