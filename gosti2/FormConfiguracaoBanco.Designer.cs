namespace gosti2
{
    partial class FormConfiguracaoBanco
    {
        private System.ComponentModel.IContainer components = null;
        //private System.Windows.Forms.RadioButton rbWindowsAuth;
        //private System.Windows.Forms.RadioButton rbSqlAuth;
        //private System.Windows.Forms.GroupBox groupBoxAutenticacao;
        //private System.Windows.Forms.Button btnDetectarServidores;
        //private System.Windows.Forms.Panel panelStatus;
        //private System.Windows.Forms.Label lblStatus;

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
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.btnTestarConexao = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkAjuda = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.rbWindowsAuth = new System.Windows.Forms.RadioButton();
            this.rbSqlAuth = new System.Windows.Forms.RadioButton();
            this.groupBoxAutenticacao = new System.Windows.Forms.GroupBox();
            this.btnDetectarServidores = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBoxAutenticacao.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServidor
            // 
            this.txtServidor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidor.Location = new System.Drawing.Point(120, 80);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(250, 23);
            this.txtServidor.TabIndex = 1;
            this.txtServidor.Text = ".\\SQLEXPRESS";
            // 
            // txtBanco
            // 
            this.txtBanco.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBanco.Location = new System.Drawing.Point(120, 110);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(250, 23);
            this.txtBanco.TabIndex = 2;
            this.txtBanco.Text = "CJ3027333PR2";
            // 
            // btnTestarConexao
            // 
            this.btnTestarConexao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnTestarConexao.FlatAppearance.BorderSize = 0;
            this.btnTestarConexao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(110)))), ((int)(((byte)(160)))));
            this.btnTestarConexao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestarConexao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestarConexao.ForeColor = System.Drawing.Color.White;
            this.btnTestarConexao.Location = new System.Drawing.Point(20, 320);
            this.btnTestarConexao.Name = "btnTestarConexao";
            this.btnTestarConexao.Size = new System.Drawing.Size(120, 35);
            this.btnTestarConexao.TabIndex = 7;
            this.btnTestarConexao.Text = "🔍 Testar Conexão";
            this.btnTestarConexao.UseVisualStyleBackColor = false;
            this.btnTestarConexao.Click += new System.EventHandler(this.btnTestarConexao_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.btnSalvar.Enabled = false;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(159)))), ((int)(((byte)(103)))));
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(150, 320);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 35);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "💾 Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(260, 320);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "🚪 Sair";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "🖥️ Servidor SQL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "🗃️ Banco Dados:";
            // 
            // linkAjuda
            // 
            this.linkAjuda.AutoSize = true;
            this.linkAjuda.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkAjuda.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.linkAjuda.Location = new System.Drawing.Point(320, 360);
            this.linkAjuda.Name = "linkAjuda";
            this.linkAjuda.Size = new System.Drawing.Size(49, 13);
            this.linkAjuda.TabIndex = 10;
            this.linkAjuda.TabStop = true;
            this.linkAjuda.Text = "Ajuda 💡";
            this.linkAjuda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAjuda_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Configuração do Banco Dados";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "🔒 Password:";
            // 
            // txtSenha
            // 
            this.txtSenha.Enabled = false;
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(120, 240);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '•';
            this.txtSenha.Size = new System.Drawing.Size(250, 23);
            this.txtSenha.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "👤 Username:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(120, 210);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(250, 23);
            this.txtUsuario.TabIndex = 5;
            // 
            // rbWindowsAuth
            // 
            this.rbWindowsAuth.AutoSize = true;
            this.rbWindowsAuth.Checked = true;
            this.rbWindowsAuth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWindowsAuth.Location = new System.Drawing.Point(20, 20);
            this.rbWindowsAuth.Name = "rbWindowsAuth";
            this.rbWindowsAuth.Size = new System.Drawing.Size(177, 19);
            this.rbWindowsAuth.TabIndex = 3;
            this.rbWindowsAuth.TabStop = true;
            this.rbWindowsAuth.Text = "🔓 Autenticação do Windows";
            this.rbWindowsAuth.UseVisualStyleBackColor = true;
            this.rbWindowsAuth.CheckedChanged += new System.EventHandler(this.rbWindowsAuth_CheckedChanged);
            // 
            // rbSqlAuth
            // 
            this.rbSqlAuth.AutoSize = true;
            this.rbSqlAuth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSqlAuth.Location = new System.Drawing.Point(20, 45);
            this.rbSqlAuth.Name = "rbSqlAuth";
            this.rbSqlAuth.Size = new System.Drawing.Size(186, 19);
            this.rbSqlAuth.TabIndex = 4;
            this.rbSqlAuth.Text = "🔑 Autenticação do SQL Server";
            this.rbSqlAuth.UseVisualStyleBackColor = true;
            this.rbSqlAuth.CheckedChanged += new System.EventHandler(this.rbSqlAuth_CheckedChanged);
            // 
            // groupBoxAutenticacao
            // 
            this.groupBoxAutenticacao.Controls.Add(this.rbWindowsAuth);
            this.groupBoxAutenticacao.Controls.Add(this.rbSqlAuth);
            this.groupBoxAutenticacao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAutenticacao.Location = new System.Drawing.Point(20, 140);
            this.groupBoxAutenticacao.Name = "groupBoxAutenticacao";
            this.groupBoxAutenticacao.Size = new System.Drawing.Size(350, 75);
            this.groupBoxAutenticacao.TabIndex = 3;
            this.groupBoxAutenticacao.TabStop = false;
            this.groupBoxAutenticacao.Text = "🔐 Tipo de Autenticação";
            // 
            // btnDetectarServidores
            // 
            this.btnDetectarServidores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(160)))), ((int)(((byte)(90)))));
            this.btnDetectarServidores.FlatAppearance.BorderSize = 0;
            this.btnDetectarServidores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(140)))), ((int)(((byte)(70)))));
            this.btnDetectarServidores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetectarServidores.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetectarServidores.ForeColor = System.Drawing.Color.White;
            this.btnDetectarServidores.Location = new System.Drawing.Point(290, 280);
            this.btnDetectarServidores.Name = "btnDetectarServidores";
            this.btnDetectarServidores.Size = new System.Drawing.Size(80, 25);
            this.btnDetectarServidores.TabIndex = 11;
            this.btnDetectarServidores.Text = "🔎 Detectar";
            this.btnDetectarServidores.UseVisualStyleBackColor = false;
            this.btnDetectarServidores.Click += new System.EventHandler(this.btnDetectarServidores_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatus.Controls.Add(this.lblStatus);
            this.panelStatus.Location = new System.Drawing.Point(20, 280);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(260, 25);
            this.panelStatus.TabIndex = 17;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(5, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(183, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Configure a conexão com o banco";
            // 
            // FormConfiguracaoBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(394, 381);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.btnDetectarServidores);
            this.Controls.Add(this.groupBoxAutenticacao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkAjuda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnTestarConexao);
            this.Controls.Add(this.txtBanco);
            this.Controls.Add(this.txtServidor);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfiguracaoBanco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração do Banco - BookConnect";
            this.groupBoxAutenticacao.ResumeLayout(false);
            this.groupBoxAutenticacao.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Button btnTestarConexao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkAjuda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.RadioButton rbWindowsAuth;
        private System.Windows.Forms.RadioButton rbSqlAuth;
        private System.Windows.Forms.GroupBox groupBoxAutenticacao;
        private System.Windows.Forms.Button btnDetectarServidores;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}