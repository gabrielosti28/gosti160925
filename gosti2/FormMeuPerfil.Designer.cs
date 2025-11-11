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
            this.btnAlterarFoto = new System.Windows.Forms.Button();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.lblNomeUsuario = new System.Windows.Forms.Label();
            this.pictureBoxPerfil = new System.Windows.Forms.PictureBox();
            this.btnEnviarMensagem = new System.Windows.Forms.Button();
            this.panelEstatisticas = new System.Windows.Forms.Panel();
            this.lblTotalTierLists = new System.Windows.Forms.Label();
            this.lblTotalComentarios = new System.Windows.Forms.Label();
            this.lblTotalLivros = new System.Windows.Forms.Label();
            this.lblDataLogin = new System.Windows.Forms.Label();
            this.panelUltimosLivros = new System.Windows.Forms.Panel();
            this.picLivro3 = new System.Windows.Forms.PictureBox();
            this.lblLivro3 = new System.Windows.Forms.Label();
            this.picLivro2 = new System.Windows.Forms.PictureBox();
            this.lblLivro2 = new System.Windows.Forms.Label();
            this.picLivro1 = new System.Windows.Forms.PictureBox();
            this.lblLivro1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelUltimaTierList = new System.Windows.Forms.Panel();
            this.panelTierContent = new System.Windows.Forms.Panel();
            this.lblSemTier = new System.Windows.Forms.Label();
            this.lblTituloUltimaTier = new System.Windows.Forms.Label();
            this.panelPersonalizacao = new System.Windows.Forms.Panel();
            this.btnCorAzul = new System.Windows.Forms.Button();
            this.btnCorVerde = new System.Windows.Forms.Button();
            this.btnCorRoxo = new System.Windows.Forms.Button();
            this.btnCorLaranja = new System.Windows.Forms.Button();
            this.btnCorVermelho = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelTopo.SuspendLayout();
            this.panelCentral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).BeginInit();
            this.panelEstatisticas.SuspendLayout();
            this.panelUltimosLivros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro1)).BeginInit();
            this.panelUltimaTierList.SuspendLayout();
            this.panelTierContent.SuspendLayout();
            this.panelPersonalizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(908, 60);
            this.panelTopo.TabIndex = 0;
            this.panelTopo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTopo_Paint);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(169, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "👤 Meu Perfil";
            // 
            // panelCentral
            // 
            this.panelCentral.BackColor = System.Drawing.Color.White;
            this.panelCentral.Controls.Add(this.button1);
            this.panelCentral.Controls.Add(this.btnAlterarFoto);
            this.panelCentral.Controls.Add(this.txtBio);
            this.panelCentral.Controls.Add(this.lblNomeUsuario);
            this.panelCentral.Controls.Add(this.pictureBoxPerfil);
            this.panelCentral.Controls.Add(this.btnEnviarMensagem);
            this.panelCentral.Location = new System.Drawing.Point(30, 80);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(840, 150);
            this.panelCentral.TabIndex = 1;
            // 
            // btnAlterarFoto
            // 
            this.btnAlterarFoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAlterarFoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlterarFoto.FlatAppearance.BorderSize = 0;
            this.btnAlterarFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlterarFoto.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnAlterarFoto.ForeColor = System.Drawing.Color.White;
            this.btnAlterarFoto.Location = new System.Drawing.Point(24, 115);
            this.btnAlterarFoto.Name = "btnAlterarFoto";
            this.btnAlterarFoto.Size = new System.Drawing.Size(110, 25);
            this.btnAlterarFoto.TabIndex = 3;
            this.btnAlterarFoto.Text = "📷 Alterar Foto";
            this.btnAlterarFoto.UseVisualStyleBackColor = false;
            this.btnAlterarFoto.Click += new System.EventHandler(this.btnAlterarFoto_Click);
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
            // pictureBoxPerfil
            // 
            this.pictureBoxPerfil.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxPerfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPerfil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPerfil.Location = new System.Drawing.Point(24, 20);
            this.pictureBoxPerfil.Name = "pictureBoxPerfil";
            this.pictureBoxPerfil.Size = new System.Drawing.Size(110, 90);
            this.pictureBoxPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPerfil.TabIndex = 0;
            this.pictureBoxPerfil.TabStop = false;
            // 
            // btnEnviarMensagem
            // 
            this.btnEnviarMensagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.btnEnviarMensagem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviarMensagem.FlatAppearance.BorderSize = 0;
            this.btnEnviarMensagem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarMensagem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnviarMensagem.ForeColor = System.Drawing.Color.White;
            this.btnEnviarMensagem.Location = new System.Drawing.Point(160, 115);
            this.btnEnviarMensagem.Name = "btnEnviarMensagem";
            this.btnEnviarMensagem.Size = new System.Drawing.Size(140, 25);
            this.btnEnviarMensagem.TabIndex = 4;
            this.btnEnviarMensagem.Text = "✉️ Enviar Mensagem";
            this.btnEnviarMensagem.UseVisualStyleBackColor = false;
            this.btnEnviarMensagem.Click += new System.EventHandler(this.btnEnviarMensagem_Click);
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
            this.panelEstatisticas.Size = new System.Drawing.Size(840, 100);
            this.panelEstatisticas.TabIndex = 2;
            // 
            // lblTotalTierLists
            // 
            this.lblTotalTierLists.AutoSize = true;
            this.lblTotalTierLists.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalTierLists.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalTierLists.Location = new System.Drawing.Point(450, 50);
            this.lblTotalTierLists.Name = "lblTotalTierLists";
            this.lblTotalTierLists.Size = new System.Drawing.Size(98, 20);
            this.lblTotalTierLists.TabIndex = 3;
            this.lblTotalTierLists.Text = "⭐ Tier Lists: 0";
            this.lblTotalTierLists.Click += new System.EventHandler(this.lblTotalTierLists_Click);
            // 
            // lblTotalComentarios
            // 
            this.lblTotalComentarios.AutoSize = true;
            this.lblTotalComentarios.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTotalComentarios.Location = new System.Drawing.Point(30, 70);
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
            this.lblTotalLivros.Size = new System.Drawing.Size(170, 20);
            this.lblTotalLivros.TabIndex = 1;
            this.lblTotalLivros.Text = "📚 Livros adicionados: 0";
            // 
            // lblDataLogin
            // 
            this.lblDataLogin.AutoSize = true;
            this.lblDataLogin.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDataLogin.Location = new System.Drawing.Point(30, 20);
            this.lblDataLogin.Name = "lblDataLogin";
            this.lblDataLogin.Size = new System.Drawing.Size(199, 20);
            this.lblDataLogin.TabIndex = 0;
            this.lblDataLogin.Text = "📅 Último login: 00/00/0000";
            // 
            // panelUltimosLivros
            // 
            this.panelUltimosLivros.BackColor = System.Drawing.Color.White;
            this.panelUltimosLivros.Controls.Add(this.picLivro3);
            this.panelUltimosLivros.Controls.Add(this.lblLivro3);
            this.panelUltimosLivros.Controls.Add(this.picLivro2);
            this.panelUltimosLivros.Controls.Add(this.lblLivro2);
            this.panelUltimosLivros.Controls.Add(this.picLivro1);
            this.panelUltimosLivros.Controls.Add(this.lblLivro1);
            this.panelUltimosLivros.Controls.Add(this.label7);
            this.panelUltimosLivros.Location = new System.Drawing.Point(30, 370);
            this.panelUltimosLivros.Name = "panelUltimosLivros";
            this.panelUltimosLivros.Size = new System.Drawing.Size(840, 150);
            this.panelUltimosLivros.TabIndex = 3;
            // 
            // picLivro3
            // 
            this.picLivro3.BackColor = System.Drawing.Color.LightGray;
            this.picLivro3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLivro3.Location = new System.Drawing.Point(580, 40);
            this.picLivro3.Name = "picLivro3";
            this.picLivro3.Size = new System.Drawing.Size(80, 100);
            this.picLivro3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLivro3.TabIndex = 6;
            this.picLivro3.TabStop = false;
            // 
            // lblLivro3
            // 
            this.lblLivro3.AutoSize = true;
            this.lblLivro3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLivro3.Location = new System.Drawing.Point(677, 70);
            this.lblLivro3.MaximumSize = new System.Drawing.Size(140, 0);
            this.lblLivro3.Name = "lblLivro3";
            this.lblLivro3.Size = new System.Drawing.Size(42, 15);
            this.lblLivro3.TabIndex = 5;
            this.lblLivro3.Text = "Livro 3";
            // 
            // picLivro2
            // 
            this.picLivro2.BackColor = System.Drawing.Color.LightGray;
            this.picLivro2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLivro2.Location = new System.Drawing.Point(310, 40);
            this.picLivro2.Name = "picLivro2";
            this.picLivro2.Size = new System.Drawing.Size(80, 100);
            this.picLivro2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLivro2.TabIndex = 4;
            this.picLivro2.TabStop = false;
            // 
            // lblLivro2
            // 
            this.lblLivro2.AutoSize = true;
            this.lblLivro2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLivro2.Location = new System.Drawing.Point(407, 70);
            this.lblLivro2.MaximumSize = new System.Drawing.Size(140, 0);
            this.lblLivro2.Name = "lblLivro2";
            this.lblLivro2.Size = new System.Drawing.Size(42, 15);
            this.lblLivro2.TabIndex = 3;
            this.lblLivro2.Text = "Livro 2";
            // 
            // picLivro1
            // 
            this.picLivro1.BackColor = System.Drawing.Color.LightGray;
            this.picLivro1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLivro1.Location = new System.Drawing.Point(40, 40);
            this.picLivro1.Name = "picLivro1";
            this.picLivro1.Size = new System.Drawing.Size(80, 100);
            this.picLivro1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLivro1.TabIndex = 2;
            this.picLivro1.TabStop = false;
            // 
            // lblLivro1
            // 
            this.lblLivro1.AutoSize = true;
            this.lblLivro1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLivro1.Location = new System.Drawing.Point(137, 70);
            this.lblLivro1.MaximumSize = new System.Drawing.Size(140, 0);
            this.lblLivro1.Name = "lblLivro1";
            this.lblLivro1.Size = new System.Drawing.Size(42, 15);
            this.lblLivro1.TabIndex = 1;
            this.lblLivro1.Text = "Livro 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.label7.Location = new System.Drawing.Point(20, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(256, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "📚 Últimos 3 Livros Adicionados";
            // 
            // panelUltimaTierList
            // 
            this.panelUltimaTierList.BackColor = System.Drawing.Color.White;
            this.panelUltimaTierList.Controls.Add(this.panelTierContent);
            this.panelUltimaTierList.Controls.Add(this.lblTituloUltimaTier);
            this.panelUltimaTierList.Location = new System.Drawing.Point(30, 540);
            this.panelUltimaTierList.Name = "panelUltimaTierList";
            this.panelUltimaTierList.Size = new System.Drawing.Size(840, 150);
            this.panelUltimaTierList.TabIndex = 4;
            // 
            // panelTierContent
            // 
            this.panelTierContent.Controls.Add(this.lblSemTier);
            this.panelTierContent.Location = new System.Drawing.Point(15, 40);
            this.panelTierContent.Name = "panelTierContent";
            this.panelTierContent.Size = new System.Drawing.Size(810, 100);
            this.panelTierContent.TabIndex = 1;
            // 
            // lblSemTier
            // 
            this.lblSemTier.AutoSize = true;
            this.lblSemTier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblSemTier.ForeColor = System.Drawing.Color.Gray;
            this.lblSemTier.Location = new System.Drawing.Point(10, 40);
            this.lblSemTier.Name = "lblSemTier";
            this.lblSemTier.Size = new System.Drawing.Size(253, 19);
            this.lblSemTier.TabIndex = 0;
            this.lblSemTier.Text = "Você ainda não criou nenhuma tier list";
            // 
            // lblTituloUltimaTier
            // 
            this.lblTituloUltimaTier.AutoSize = true;
            this.lblTituloUltimaTier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloUltimaTier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.lblTituloUltimaTier.Location = new System.Drawing.Point(20, 15);
            this.lblTituloUltimaTier.Name = "lblTituloUltimaTier";
            this.lblTituloUltimaTier.Size = new System.Drawing.Size(197, 21);
            this.lblTituloUltimaTier.TabIndex = 0;
            this.lblTituloUltimaTier.Text = "⭐ Última Tier List Criada";
            // 
            // panelPersonalizacao
            // 
            this.panelPersonalizacao.BackColor = System.Drawing.Color.White;
            this.panelPersonalizacao.Controls.Add(this.btnCorAzul);
            this.panelPersonalizacao.Controls.Add(this.btnCorVerde);
            this.panelPersonalizacao.Controls.Add(this.btnCorRoxo);
            this.panelPersonalizacao.Controls.Add(this.btnCorLaranja);
            this.panelPersonalizacao.Controls.Add(this.btnCorVermelho);
            this.panelPersonalizacao.Controls.Add(this.label1);
            this.panelPersonalizacao.Location = new System.Drawing.Point(30, 710);
            this.panelPersonalizacao.Name = "panelPersonalizacao";
            this.panelPersonalizacao.Size = new System.Drawing.Size(840, 80);
            this.panelPersonalizacao.TabIndex = 5;
            // 
            // btnCorAzul
            // 
            this.btnCorAzul.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnCorAzul.FlatAppearance.BorderSize = 2;
            this.btnCorAzul.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorAzul.Location = new System.Drawing.Point(250, 25);
            this.btnCorAzul.Name = "btnCorAzul";
            this.btnCorAzul.Size = new System.Drawing.Size(50, 40);
            this.btnCorAzul.TabIndex = 1;
            this.btnCorAzul.UseVisualStyleBackColor = false;
            this.btnCorAzul.Click += new System.EventHandler(this.btnCorAzul_Click);
            // 
            // btnCorVerde
            // 
            this.btnCorVerde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.btnCorVerde.FlatAppearance.BorderSize = 2;
            this.btnCorVerde.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorVerde.Location = new System.Drawing.Point(320, 25);
            this.btnCorVerde.Name = "btnCorVerde";
            this.btnCorVerde.Size = new System.Drawing.Size(50, 40);
            this.btnCorVerde.TabIndex = 2;
            this.btnCorVerde.UseVisualStyleBackColor = false;
            this.btnCorVerde.Click += new System.EventHandler(this.btnCorVerde_Click);
            // 
            // btnCorRoxo
            // 
            this.btnCorRoxo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnCorRoxo.FlatAppearance.BorderSize = 2;
            this.btnCorRoxo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorRoxo.Location = new System.Drawing.Point(390, 25);
            this.btnCorRoxo.Name = "btnCorRoxo";
            this.btnCorRoxo.Size = new System.Drawing.Size(50, 40);
            this.btnCorRoxo.TabIndex = 3;
            this.btnCorRoxo.UseVisualStyleBackColor = false;
            this.btnCorRoxo.Click += new System.EventHandler(this.btnCorRoxo_Click);
            // 
            // btnCorLaranja
            // 
            this.btnCorLaranja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.btnCorLaranja.FlatAppearance.BorderSize = 2;
            this.btnCorLaranja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorLaranja.Location = new System.Drawing.Point(460, 25);
            this.btnCorLaranja.Name = "btnCorLaranja";
            this.btnCorLaranja.Size = new System.Drawing.Size(50, 40);
            this.btnCorLaranja.TabIndex = 4;
            this.btnCorLaranja.UseVisualStyleBackColor = false;
            this.btnCorLaranja.Click += new System.EventHandler(this.btnCorLaranja_Click);
            // 
            // btnCorVermelho
            // 
            this.btnCorVermelho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCorVermelho.FlatAppearance.BorderSize = 2;
            this.btnCorVermelho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorVermelho.Location = new System.Drawing.Point(530, 25);
            this.btnCorVermelho.Name = "btnCorVermelho";
            this.btnCorVermelho.Size = new System.Drawing.Size(50, 40);
            this.btnCorVermelho.TabIndex = 5;
            this.btnCorVermelho.UseVisualStyleBackColor = false;
            this.btnCorVermelho.Click += new System.EventHandler(this.btnCorVermelho_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "🎨 Cor do Perfil (tema atual):";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(750, 796);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(120, 35);
            this.btnFechar.TabIndex = 6;
            this.btnFechar.Text = "✕ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(179)))), ((int)(((byte)(113)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(570, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "✉️ editar perfil";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMeuPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(925, 726);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.panelPersonalizacao);
            this.Controls.Add(this.panelUltimaTierList);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).EndInit();
            this.panelEstatisticas.ResumeLayout(false);
            this.panelEstatisticas.PerformLayout();
            this.panelUltimosLivros.ResumeLayout(false);
            this.panelUltimosLivros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLivro1)).EndInit();
            this.panelUltimaTierList.ResumeLayout(false);
            this.panelUltimaTierList.PerformLayout();
            this.panelTierContent.ResumeLayout(false);
            this.panelTierContent.PerformLayout();
            this.panelPersonalizacao.ResumeLayout(false);
            this.panelPersonalizacao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEnviarMensagem;
        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.PictureBox pictureBoxPerfil;
        private System.Windows.Forms.Label lblNomeUsuario;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Button btnAlterarFoto;
        private System.Windows.Forms.Panel panelEstatisticas;
        private System.Windows.Forms.Label lblDataLogin;
        private System.Windows.Forms.Label lblTotalLivros;
        private System.Windows.Forms.Label lblTotalComentarios;
        private System.Windows.Forms.Panel panelUltimosLivros;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLivro1;
        private System.Windows.Forms.PictureBox picLivro1;
        private System.Windows.Forms.PictureBox picLivro2;
        private System.Windows.Forms.Label lblLivro2;
        private System.Windows.Forms.PictureBox picLivro3;
        private System.Windows.Forms.Label lblLivro3;
        private System.Windows.Forms.Panel panelUltimaTierList;
        private System.Windows.Forms.Label lblTituloUltimaTier;
        private System.Windows.Forms.Panel panelTierContent;
        private System.Windows.Forms.Label lblSemTier;
        private System.Windows.Forms.Panel panelPersonalizacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCorAzul;
        private System.Windows.Forms.Button btnCorVerde;
        private System.Windows.Forms.Button btnCorRoxo;
        private System.Windows.Forms.Button btnCorLaranja;
        private System.Windows.Forms.Button btnCorVermelho;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblTotalTierLists;
        private System.Windows.Forms.Button button1;
    }
}