namespace gosti2
{
    partial class FormConfiguracaoEmergencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfiguracaoEmergencia));
            this.labelTitulo = new System.Windows.Forms.Label();
            this.labelServidor = new System.Windows.Forms.Label();
            this.comboBoxServidores = new System.Windows.Forms.ComboBox();
            this.btnTestarConexao = new System.Windows.Forms.Button();
            this.btnCriarBanco = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelDescricao = new System.Windows.Forms.Label();
            this.btnTestarBanco = new System.Windows.Forms.Button();
            this.btnAdicionarServidor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.ForeColor = System.Drawing.Color.DarkRed;
            this.labelTitulo.Location = new System.Drawing.Point(80, 20);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(324, 24);
            this.labelTitulo.TabIndex = 0;
            this.labelTitulo.Text = "🔧 Configuração de Emergência";
            // 
            // labelServidor
            // 
            this.labelServidor.AutoSize = true;
            this.labelServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServidor.Location = new System.Drawing.Point(30, 90);
            this.labelServidor.Name = "labelServidor";
            this.labelServidor.Size = new System.Drawing.Size(56, 15);
            this.labelServidor.TabIndex = 1;
            this.labelServidor.Text = "Servidor:";
            // 
            // comboBoxServidores
            // 
            this.comboBoxServidores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServidores.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxServidores.FormattingEnabled = true;
            this.comboBoxServidores.Location = new System.Drawing.Point(92, 87);
            this.comboBoxServidores.Name = "comboBoxServidores";
            this.comboBoxServidores.Size = new System.Drawing.Size(250, 23);
            this.comboBoxServidores.TabIndex = 2;
            // 
            // btnTestarConexao
            // 
            this.btnTestarConexao.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTestarConexao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestarConexao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestarConexao.ForeColor = System.Drawing.Color.White;
            this.btnTestarConexao.Location = new System.Drawing.Point(30, 130);
            this.btnTestarConexao.Name = "btnTestarConexao";
            this.btnTestarConexao.Size = new System.Drawing.Size(120, 35);
            this.btnTestarConexao.TabIndex = 3;
            this.btnTestarConexao.Text = "🔍 Testar Servidor";
            this.btnTestarConexao.UseVisualStyleBackColor = false;
            this.btnTestarConexao.Click += new System.EventHandler(this.btnTestarConexao_Click);
            // 
            // btnCriarBanco
            // 
            this.btnCriarBanco.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCriarBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCriarBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriarBanco.ForeColor = System.Drawing.Color.White;
            this.btnCriarBanco.Location = new System.Drawing.Point(160, 130);
            this.btnCriarBanco.Name = "btnCriarBanco";
            this.btnCriarBanco.Size = new System.Drawing.Size(120, 35);
            this.btnCriarBanco.TabIndex = 4;
            this.btnCriarBanco.Text = "🗃️ Criar Banco";
            this.btnCriarBanco.UseVisualStyleBackColor = false;
            this.btnCriarBanco.Click += new System.EventHandler(this.btnCriarBanco_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(290, 130);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 35);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.labelDescricao);
            this.panel1.Location = new System.Drawing.Point(30, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 80);
            this.panel1.TabIndex = 6;
            // 
            // labelDescricao
            // 
            this.labelDescricao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescricao.Location = new System.Drawing.Point(0, 0);
            this.labelDescricao.Name = "labelDescricao";
            this.labelDescricao.Padding = new System.Windows.Forms.Padding(10);
            this.labelDescricao.Size = new System.Drawing.Size(350, 80);
            this.labelDescricao.TabIndex = 0;
            this.labelDescricao.Text = "⚠️  Não foi possível conectar automaticamente ao banco de dados. \r\n\r\nSelecione o " +
    "servidor SQL Server e teste a conexão. Em seguida, crie o banco se necessário.";
            this.labelDescricao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTestarBanco
            // 
            this.btnTestarBanco.BackColor = System.Drawing.Color.Goldenrod;
            this.btnTestarBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestarBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestarBanco.ForeColor = System.Drawing.Color.White;
            this.btnTestarBanco.Location = new System.Drawing.Point(348, 85);
            this.btnTestarBanco.Name = "btnTestarBanco";
            this.btnTestarBanco.Size = new System.Drawing.Size(32, 27);
            this.btnTestarBanco.TabIndex = 7;
            this.btnTestarBanco.Text = "📋";
            this.btnTestarBanco.UseVisualStyleBackColor = false;
            this.btnTestarBanco.Click += new System.EventHandler(this.btnTestarBanco_Click);
            // 
            // btnAdicionarServidor
            // 
            this.btnAdicionarServidor.BackColor = System.Drawing.Color.Teal;
            this.btnAdicionarServidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarServidor.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarServidor.Location = new System.Drawing.Point(348, 55);
            this.btnAdicionarServidor.Name = "btnAdicionarServidor";
            this.btnAdicionarServidor.Size = new System.Drawing.Size(32, 27);
            this.btnAdicionarServidor.TabIndex = 8;
            this.btnAdicionarServidor.Text = "+";
            this.btnAdicionarServidor.UseVisualStyleBackColor = false;
            this.btnAdicionarServidor.Click += new System.EventHandler(this.btnAdicionarServidor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // FormConfiguracaoEmergência
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(404, 281);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAdicionarServidor);
            this.Controls.Add(this.btnTestarBanco);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCriarBanco);
            this.Controls.Add(this.btnTestarConexao);
            this.Controls.Add(this.comboBoxServidores);
            this.Controls.Add(this.labelServidor);
            this.Controls.Add(this.labelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfiguracaoEmergencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Banco - BookConnect";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label labelServidor;
        private System.Windows.Forms.ComboBox comboBoxServidores;
        private System.Windows.Forms.Button btnTestarConexao;
        private System.Windows.Forms.Button btnCriarBanco;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelDescricao;
        private System.Windows.Forms.Button btnTestarBanco;
        private System.Windows.Forms.Button btnAdicionarServidor;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}