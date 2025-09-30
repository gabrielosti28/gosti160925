namespace gosti2
{
    partial class FormAdicionarLivro
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
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.checkBoxFavorito = new System.Windows.Forms.CheckBox();
            this.checkBoxLido = new System.Windows.Forms.CheckBox();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAutor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelImagem = new System.Windows.Forms.Panel();
            this.btnSelecionarImagem = new System.Windows.Forms.Button();
            this.pictureBoxCapa = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTopo.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelImagem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapa)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(500, 60);
            this.panelTopo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(230, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Adicionar Novo Livro";
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.btnCancelar);
            this.panelConteudo.Controls.Add(this.btnSalvar);
            this.panelConteudo.Controls.Add(this.checkBoxFavorito);
            this.panelConteudo.Controls.Add(this.checkBoxLido);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.label5);
            this.panelConteudo.Controls.Add(this.cmbGenero);
            this.panelConteudo.Controls.Add(this.label4);
            this.panelConteudo.Controls.Add(this.txtAutor);
            this.panelConteudo.Controls.Add(this.label3);
            this.panelConteudo.Controls.Add(this.txtTitulo);
            this.panelConteudo.Controls.Add(this.label2);
            this.panelConteudo.Controls.Add(this.panelImagem);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 60);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(20);
            this.panelConteudo.Size = new System.Drawing.Size(500, 540);
            this.panelConteudo.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(363, 460);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(243, 460);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 35);
            this.btnSalvar.TabIndex = 12;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // checkBoxFavorito
            // 
            this.checkBoxFavorito.AutoSize = true;
            this.checkBoxFavorito.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.checkBoxFavorito.Location = new System.Drawing.Point(383, 410);
            this.checkBoxFavorito.Name = "checkBoxFavorito";
            this.checkBoxFavorito.Size = new System.Drawing.Size(78, 23);
            this.checkBoxFavorito.TabIndex = 11;
            this.checkBoxFavorito.Text = "Favorito";
            this.checkBoxFavorito.UseVisualStyleBackColor = true;
            // 
            // checkBoxLido
            // 
            this.checkBoxLido.AutoSize = true;
            this.checkBoxLido.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.checkBoxLido.Location = new System.Drawing.Point(243, 410);
            this.checkBoxLido.Name = "checkBoxLido";
            this.checkBoxLido.Size = new System.Drawing.Size(54, 23);
            this.checkBoxLido.TabIndex = 10;
            this.checkBoxLido.Text = "Lido";
            this.checkBoxLido.UseVisualStyleBackColor = true;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescricao.Location = new System.Drawing.Point(172, 291);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(320, 100);
            this.txtDescricao.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(172, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Descrição:";
            // 
            // cmbGenero
            // 
            this.cmbGenero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Items.AddRange(new object[] {
            "Fantasia",
            "Ficção Científica",
            "Romance",
            "Suspense",
            "Terror",
            "Biografia",
            "História",
            "Autoajuda",
            "Poesia",
            "Drama",
            "Aventura",
            "Infantil"});
            this.cmbGenero.Location = new System.Drawing.Point(172, 215);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(320, 25);
            this.cmbGenero.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(172, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Gênero:";
            // 
            // txtAutor
            // 
            this.txtAutor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAutor.Location = new System.Drawing.Point(168, 146);
            this.txtAutor.Name = "txtAutor";
            this.txtAutor.Size = new System.Drawing.Size(320, 25);
            this.txtAutor.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(168, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Autor:";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitulo.Location = new System.Drawing.Point(168, 78);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(320, 25);
            this.txtTitulo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(168, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Título:";
            // 
            // panelImagem
            // 
            this.panelImagem.Controls.Add(this.btnSelecionarImagem);
            this.panelImagem.Controls.Add(this.pictureBoxCapa);
            this.panelImagem.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImagem.Location = new System.Drawing.Point(20, 20);
            this.panelImagem.Name = "panelImagem";
            this.panelImagem.Size = new System.Drawing.Size(146, 500);
            this.panelImagem.TabIndex = 1;
            // 
            // btnSelecionarImagem
            // 
            this.btnSelecionarImagem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSelecionarImagem.Location = new System.Drawing.Point(28, 226);
            this.btnSelecionarImagem.Name = "btnSelecionarImagem";
            this.btnSelecionarImagem.Size = new System.Drawing.Size(90, 25);
            this.btnSelecionarImagem.TabIndex = 2;
            this.btnSelecionarImagem.Text = "Selecionar...";
            this.btnSelecionarImagem.UseVisualStyleBackColor = true;
            this.btnSelecionarImagem.Click += new System.EventHandler(this.btnSelecionarImagem_Click);
            // 
            // pictureBoxCapa
            // 
            this.pictureBoxCapa.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxCapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCapa.Location = new System.Drawing.Point(5, 30);
            this.pictureBoxCapa.Name = "pictureBoxCapa";
            this.pictureBoxCapa.Size = new System.Drawing.Size(137, 190);
            this.pictureBoxCapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCapa.TabIndex = 1;
            this.pictureBoxCapa.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Capa:";
            // 
            // FormAdicionarLivro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelTopo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdicionarLivro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adicionar Livro";
            this.panelTopo.ResumeLayout(false);
            this.panelTopo.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.panelImagem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCapa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelImagem;
        private System.Windows.Forms.Button btnSelecionarImagem;
        private System.Windows.Forms.PictureBox pictureBoxCapa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAutor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxLido;
        private System.Windows.Forms.CheckBox checkBoxFavorito;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
    }
}