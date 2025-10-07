namespace gosti2
{
    partial class FormDiagnosticos
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewLogs;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnLimparLogs;
        private System.Windows.Forms.TextBox txtDetalhes;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.Label lblDetalhes;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.ComboBox cmbNivelFiltro;
        private System.Windows.Forms.Label lblFiltrarPor;
        private System.Windows.Forms.Button btnTestarConexao;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewLogs = new System.Windows.Forms.DataGridView();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnLimparLogs = new System.Windows.Forms.Button();
            this.txtDetalhes = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblLogs = new System.Windows.Forms.Label();
            this.lblDetalhes = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.cmbNivelFiltro = new System.Windows.Forms.ComboBox();
            this.lblFiltrarPor = new System.Windows.Forms.Label();
            this.btnTestarConexao = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewLogs
            // 
            this.dataGridViewLogs.AllowUserToAddRows = false;
            this.dataGridViewLogs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridViewLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLogs.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewLogs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLogs.Location = new System.Drawing.Point(12, 80);
            this.dataGridViewLogs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewLogs.MultiSelect = false;
            this.dataGridViewLogs.Name = "dataGridViewLogs";
            this.dataGridViewLogs.ReadOnly = true;
            this.dataGridViewLogs.RowHeadersVisible = false;
            this.dataGridViewLogs.RowHeadersWidth = 51;
            this.dataGridViewLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLogs.Size = new System.Drawing.Size(800, 300);
            this.dataGridViewLogs.TabIndex = 0;
            this.dataGridViewLogs.SelectionChanged += new System.EventHandler(this.dataGridViewLogs_SelectionChanged);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(12, 388);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(100, 35);
            this.btnAtualizar.TabIndex = 1;
            this.btnAtualizar.Text = "🔄 Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnLimparLogs
            // 
            this.btnLimparLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnLimparLogs.FlatAppearance.BorderSize = 0;
            this.btnLimparLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimparLogs.ForeColor = System.Drawing.Color.White;
            this.btnLimparLogs.Location = new System.Drawing.Point(118, 388);
            this.btnLimparLogs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLimparLogs.Name = "btnLimparLogs";
            this.btnLimparLogs.Size = new System.Drawing.Size(120, 35);
            this.btnLimparLogs.TabIndex = 2;
            this.btnLimparLogs.Text = "🗑️ Limpar Logs";
            this.btnLimparLogs.UseVisualStyleBackColor = false;
            this.btnLimparLogs.Click += new System.EventHandler(this.btnLimparLogs_Click);
            // 
            // txtDetalhes
            // 
            this.txtDetalhes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtDetalhes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDetalhes.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetalhes.Location = new System.Drawing.Point(12, 430);
            this.txtDetalhes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDetalhes.Multiline = true;
            this.txtDetalhes.Name = "txtDetalhes";
            this.txtDetalhes.ReadOnly = true;
            this.txtDetalhes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalhes.Size = new System.Drawing.Size(800, 150);
            this.txtDetalhes.TabIndex = 3;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(260, 30);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "🔍 Diagnóstico Sistema";
            // 
            // lblLogs
            // 
            this.lblLogs.AutoSize = true;
            this.lblLogs.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogs.Location = new System.Drawing.Point(12, 56);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(122, 19);
            this.lblLogs.TabIndex = 5;
            this.lblLogs.Text = "Logs do Sistema:";
            // 
            // lblDetalhes
            // 
            this.lblDetalhes.AutoSize = true;
            this.lblDetalhes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalhes.Location = new System.Drawing.Point(13, 430);
            this.lblDetalhes.Name = "lblDetalhes";
            this.lblDetalhes.Size = new System.Drawing.Size(70, 19);
            this.lblDetalhes.TabIndex = 6;
            this.lblDetalhes.Text = "Detalhes:";
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Location = new System.Drawing.Point(244, 388);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 35);
            this.btnExportar.TabIndex = 7;
            this.btnExportar.Text = "📤 Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
           // this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnFiltrar.FlatAppearance.BorderSize = 0;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.White;
            this.btnFiltrar.Location = new System.Drawing.Point(612, 45);
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(90, 30);
            this.btnFiltrar.TabIndex = 8;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            //this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // cmbNivelFiltro
            // 
            this.cmbNivelFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNivelFiltro.FormattingEnabled = true;
            this.cmbNivelFiltro.Items.AddRange(new object[] {
            "TODOS",
            "ERROR",
            "WARN",
            "INFO",
            "DEBUG"});
            this.cmbNivelFiltro.Location = new System.Drawing.Point(490, 48);
            this.cmbNivelFiltro.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbNivelFiltro.Name = "cmbNivelFiltro";
            this.cmbNivelFiltro.Size = new System.Drawing.Size(116, 23);
            this.cmbNivelFiltro.TabIndex = 9;
            // 
            // lblFiltrarPor
            // 
            this.lblFiltrarPor.AutoSize = true;
            this.lblFiltrarPor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltrarPor.Location = new System.Drawing.Point(420, 52);
            this.lblFiltrarPor.Name = "lblFiltrarPor";
            this.lblFiltrarPor.Size = new System.Drawing.Size(61, 15);
            this.lblFiltrarPor.TabIndex = 10;
            this.lblFiltrarPor.Text = "Filtrar por:";
            // 
            // btnTestarConexao
            // 
            this.btnTestarConexao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnTestarConexao.FlatAppearance.BorderSize = 0;
            this.btnTestarConexao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestarConexao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestarConexao.ForeColor = System.Drawing.Color.White;
            this.btnTestarConexao.Location = new System.Drawing.Point(708, 45);
            this.btnTestarConexao.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTestarConexao.Name = "btnTestarConexao";
            this.btnTestarConexao.Size = new System.Drawing.Size(104, 30);
            this.btnTestarConexao.TabIndex = 11;
            this.btnTestarConexao.Text = "🔗 Testar BD";
            this.btnTestarConexao.UseVisualStyleBackColor = false;
            this.btnTestarConexao.Click += new System.EventHandler(this.btnTestarConexao_Click);
            // 
            // FormDiagnosticos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(824, 601);
            this.Controls.Add(this.btnTestarConexao);
            this.Controls.Add(this.lblFiltrarPor);
            this.Controls.Add(this.cmbNivelFiltro);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.lblDetalhes);
            this.Controls.Add(this.lblLogs);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtDetalhes);
            this.Controls.Add(this.btnLimparLogs);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.dataGridViewLogs);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDiagnosticos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Diagnóstico - Rede Social Literária";
            this.Load += new System.EventHandler(this.FormDiagnosticos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}