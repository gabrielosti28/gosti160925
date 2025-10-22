namespace gosti2
{
    partial class FormCadastro
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                errorProvider?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblEstatistica = new System.Windows.Forms.Label();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.btnSelecionarFoto = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDataNascimento = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConfirmarSenha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNomeUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.panelRodape = new System.Windows.Forms.Panel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBoxFotoPerfil = new System.Windows.Forms.PictureBox();
            this.panelSuperior.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.panelRodape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFotoPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelSuperior.Controls.Add(this.lblEstatistica);
            this.panelSuperior.Controls.Add(this.labelTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(900, 100);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblEstatistica
            // 
            this.lblEstatistica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstatistica.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstatistica.ForeColor = System.Drawing.Color.White;
            this.lblEstatistica.Location = new System.Drawing.Point(500, 60);
            this.lblEstatistica.Name = "lblEstatistica";
            this.lblEstatistica.Size = new System.Drawing.Size(380, 20);
            this.lblEstatistica.TabIndex = 2;
            this.lblEstatistica.Text = "Junte-se a nossa comunidade de leitores!";
            this.lblEstatistica.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.ForeColor = System.Drawing.Color.White;
            this.labelTitulo.Location = new System.Drawing.Point(100, 35);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(332, 32);
            this.labelTitulo.TabIndex = 1;
            this.labelTitulo.Text = "📚 Cadastro - BookConnect";
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelConteudo.Controls.Add(this.panelFormulario);
            this.panelConteudo.Controls.Add(this.panelBotoes);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 100);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(20);
            this.panelConteudo.Size = new System.Drawing.Size(900, 500);
            this.panelConteudo.TabIndex = 1;
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.pictureBoxFotoPerfil);
            this.panelFormulario.Controls.Add(this.btnSelecionarFoto);
            this.panelFormulario.Controls.Add(this.label7);
            this.panelFormulario.Controls.Add(this.txtBio);
            this.panelFormulario.Controls.Add(this.label6);
            this.panelFormulario.Controls.Add(this.txtDataNascimento);
            this.panelFormulario.Controls.Add(this.label5);
            this.panelFormulario.Controls.Add(this.txtConfirmarSenha);
            this.panelFormulario.Controls.Add(this.label4);
            this.panelFormulario.Controls.Add(this.txtSenha);
            this.panelFormulario.Controls.Add(this.label3);
            this.panelFormulario.Controls.Add(this.txtEmail);
            this.panelFormulario.Controls.Add(this.label2);
            this.panelFormulario.Controls.Add(this.txtNomeUsuario);
            this.panelFormulario.Controls.Add(this.label1);
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormulario.Location = new System.Drawing.Point(20, 20);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(600, 460);
            this.panelFormulario.TabIndex = 2;
            // 
            // btnSelecionarFoto
            // 
            this.btnSelecionarFoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.btnSelecionarFoto.FlatAppearance.BorderSize = 0;
            this.btnSelecionarFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarFoto.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSelecionarFoto.ForeColor = System.Drawing.Color.White;
            this.btnSelecionarFoto.Location = new System.Drawing.Point(402, 351);
            this.btnSelecionarFoto.Name = "btnSelecionarFoto";
            this.btnSelecionarFoto.Size = new System.Drawing.Size(100, 25);
            this.btnSelecionarFoto.TabIndex = 13;
            this.btnSelecionarFoto.Text = "Escolher Foto";
            this.btnSelecionarFoto.UseVisualStyleBackColor = false;
            this.btnSelecionarFoto.Click += new System.EventHandler(this.btnSelecionarFoto_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(398, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "📷 Foto Perfil:";
            // 
            // txtBio
            // 
            this.txtBio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBio.Location = new System.Drawing.Point(350, 120);
            this.txtBio.MaxLength = 500;
            this.txtBio.Multiline = true;
            this.txtBio.Name = "txtBio";
            this.txtBio.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBio.Size = new System.Drawing.Size(200, 80);
            this.txtBio.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(350, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "📝 Biografia (Opc):";
            // 
            // txtDataNascimento
            // 
            this.txtDataNascimento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDataNascimento.Location = new System.Drawing.Point(350, 50);
            this.txtDataNascimento.Mask = "00/00/0000";
            this.txtDataNascimento.Name = "txtDataNascimento";
            this.txtDataNascimento.Size = new System.Drawing.Size(200, 25);
            this.txtDataNascimento.TabIndex = 2;
            this.txtDataNascimento.ValidatingType = typeof(System.DateTime);
            this.txtDataNascimento.Enter += new System.EventHandler(this.txtDataNascimento_Enter);
            this.txtDataNascimento.Leave += new System.EventHandler(this.txtDataNascimento_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(350, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "🎂 Data Nascimento:";
            // 
            // txtConfirmarSenha
            // 
            this.txtConfirmarSenha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmarSenha.Location = new System.Drawing.Point(50, 350);
            this.txtConfirmarSenha.MaxLength = 255;
            this.txtConfirmarSenha.Name = "txtConfirmarSenha";
            this.txtConfirmarSenha.PasswordChar = '•';
            this.txtConfirmarSenha.Size = new System.Drawing.Size(200, 25);
            this.txtConfirmarSenha.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "🔒 Confirmar Senha:";
            // 
            // txtSenha
            // 
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSenha.Location = new System.Drawing.Point(50, 250);
            this.txtSenha.MaxLength = 255;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '•';
            this.txtSenha.Size = new System.Drawing.Size(200, 25);
            this.txtSenha.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "🔑 Senha:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(50, 150);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 25);
            this.txtEmail.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "📧 Email:";
            // 
            // txtNomeUsuario
            // 
            this.txtNomeUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNomeUsuario.Location = new System.Drawing.Point(50, 50);
            this.txtNomeUsuario.MaxLength = 100;
            this.txtNomeUsuario.Name = "txtNomeUsuario";
            this.txtNomeUsuario.Size = new System.Drawing.Size(200, 25);
            this.txtNomeUsuario.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "👤 Nome de Usuário:";
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.panelBotoes.Controls.Add(this.btnCadastrar);
            this.panelBotoes.Controls.Add(this.btnSair);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBotoes.Location = new System.Drawing.Point(620, 20);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(20);
            this.panelBotoes.Size = new System.Drawing.Size(260, 460);
            this.panelBotoes.TabIndex = 1;
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.btnCadastrar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCadastrar.FlatAppearance.BorderSize = 0;
            this.btnCadastrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(159)))), ((int)(((byte)(103)))));
            this.btnCadastrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(205)))), ((int)(((byte)(50)))));
            this.btnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCadastrar.ForeColor = System.Drawing.Color.White;
            this.btnCadastrar.Location = new System.Drawing.Point(20, 20);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(220, 80);
            this.btnCadastrar.TabIndex = 6;
            this.btnCadastrar.Text = "✅ Cadastrar";
            this.btnCadastrar.UseVisualStyleBackColor = false;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60)))));
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(40)))));
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(0)))));
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(20, 360);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(220, 80);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "🚪 Voltar ao Login";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // panelRodape
            // 
            this.panelRodape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelRodape.Controls.Add(this.labelCopyright);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodape.Location = new System.Drawing.Point(0, 600);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(900, 40);
            this.panelRodape.TabIndex = 2;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.labelCopyright.ForeColor = System.Drawing.Color.White;
            this.labelCopyright.Location = new System.Drawing.Point(0, 0);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(900, 40);
            this.labelCopyright.TabIndex = 0;
            this.labelCopyright.Text = "© 2024 BookConnect - Sua rede social literária favorita";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // pictureBoxFotoPerfil
            // 
            this.pictureBoxFotoPerfil.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxFotoPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxFotoPerfil.Location = new System.Drawing.Point(391, 245);
            this.pictureBoxFotoPerfil.Name = "pictureBoxFotoPerfil";
            this.pictureBoxFotoPerfil.Size = new System.Drawing.Size(121, 100);
            this.pictureBoxFotoPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFotoPerfil.TabIndex = 14;
            this.pictureBoxFotoPerfil.TabStop = false;
            // 
            // FormCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelRodape);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro - BookConnect";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            this.panelRodape.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFotoPerfil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelRodape;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Panel panelFormulario;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txtDataNascimento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtConfirmarSenha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNomeUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblEstatistica;
        private System.Windows.Forms.PictureBox pictureBoxFotoPerfil;
        private System.Windows.Forms.Button btnSelecionarFoto;
        private System.Windows.Forms.Label label7;
    }
}