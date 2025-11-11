namespace gosti2
{
    partial class FormMensagens
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

        private void InitializeComponent()
        {
            this.panelTopo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelConversas = new System.Windows.Forms.Panel();
            this.flowLayoutConversas = new System.Windows.Forms.FlowLayoutPanel();
            this.lblConversas = new System.Windows.Forms.Label();
            this.panelChat = new System.Windows.Forms.Panel();
            this.panelMensagens = new System.Windows.Forms.Panel();
            this.flowLayoutMensagens = new System.Windows.Forms.FlowLayoutPanel();
            this.panelEnviar = new System.Windows.Forms.Panel();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.panelHeaderChat = new System.Windows.Forms.Panel();
            this.lblNomeContato = new System.Windows.Forms.Label();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panelTopo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelConversas.SuspendLayout();
            this.panelChat.SuspendLayout();
            this.panelMensagens.SuspendLayout();
            this.panelEnviar.SuspendLayout();
            this.panelHeaderChat.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopo
            // 
            this.panelTopo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.panelTopo.Controls.Add(this.lblTitulo);
            this.panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopo.Location = new System.Drawing.Point(0, 0);
            this.panelTopo.Name = "panelTopo";
            this.panelTopo.Size = new System.Drawing.Size(1000, 60);
            this.panelTopo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(193, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "✉️ Mensagens";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 60);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelConversas);
            this.splitContainer.Panel1MinSize = 300;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelChat);
            this.splitContainer.Size = new System.Drawing.Size(1000, 570);
            this.splitContainer.SplitterDistance = 350;
            this.splitContainer.TabIndex = 1;
            // 
            // panelConversas
            // 
            this.panelConversas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelConversas.Controls.Add(this.flowLayoutConversas);
            this.panelConversas.Controls.Add(this.lblConversas);
            this.panelConversas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConversas.Location = new System.Drawing.Point(0, 0);
            this.panelConversas.Name = "panelConversas";
            this.panelConversas.Size = new System.Drawing.Size(350, 570);
            this.panelConversas.TabIndex = 0;
            // 
            // flowLayoutConversas
            // 
            this.flowLayoutConversas.AutoScroll = true;
            this.flowLayoutConversas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutConversas.Location = new System.Drawing.Point(0, 40);
            this.flowLayoutConversas.Name = "flowLayoutConversas";
            this.flowLayoutConversas.Size = new System.Drawing.Size(350, 530);
            this.flowLayoutConversas.TabIndex = 1;
            // 
            // lblConversas
            // 
            this.lblConversas.BackColor = System.Drawing.Color.White;
            this.lblConversas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblConversas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblConversas.Location = new System.Drawing.Point(0, 0);
            this.lblConversas.Name = "lblConversas";
            this.lblConversas.Padding = new System.Windows.Forms.Padding(10);
            this.lblConversas.Size = new System.Drawing.Size(350, 40);
            this.lblConversas.TabIndex = 0;
            this.lblConversas.Text = "💬 Conversas";
            this.lblConversas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelChat
            // 
            this.panelChat.BackColor = System.Drawing.Color.White;
            this.panelChat.Controls.Add(this.panelMensagens);
            this.panelChat.Controls.Add(this.panelEnviar);
            this.panelChat.Controls.Add(this.panelHeaderChat);
            this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChat.Location = new System.Drawing.Point(0, 0);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(646, 570);
            this.panelChat.TabIndex = 0;
            // 
            // panelMensagens
            // 
            this.panelMensagens.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelMensagens.Controls.Add(this.flowLayoutMensagens);
            this.panelMensagens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMensagens.Location = new System.Drawing.Point(0, 60);
            this.panelMensagens.Name = "panelMensagens";
            this.panelMensagens.Padding = new System.Windows.Forms.Padding(10);
            this.panelMensagens.Size = new System.Drawing.Size(646, 430);
            this.panelMensagens.TabIndex = 2;
            // 
            // flowLayoutMensagens
            // 
            this.flowLayoutMensagens.AutoScroll = true;
            this.flowLayoutMensagens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutMensagens.Location = new System.Drawing.Point(10, 10);
            this.flowLayoutMensagens.Name = "flowLayoutMensagens";
            this.flowLayoutMensagens.Size = new System.Drawing.Size(626, 410);
            this.flowLayoutMensagens.TabIndex = 0;
            // 
            // panelEnviar
            // 
            this.panelEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelEnviar.Controls.Add(this.btnEnviar);
            this.panelEnviar.Controls.Add(this.txtMensagem);
            this.panelEnviar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEnviar.Location = new System.Drawing.Point(0, 490);
            this.panelEnviar.Name = "panelEnviar";
            this.panelEnviar.Padding = new System.Windows.Forms.Padding(10);
            this.panelEnviar.Size = new System.Drawing.Size(646, 80);
            this.panelEnviar.TabIndex = 1;
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnEnviar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEnviar.FlatAppearance.BorderSize = 0;
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEnviar.ForeColor = System.Drawing.Color.White;
            this.btnEnviar.Location = new System.Drawing.Point(536, 10);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(100, 60);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "📤 Enviar";
            this.btnEnviar.UseVisualStyleBackColor = false;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMensagem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMensagem.Location = new System.Drawing.Point(10, 10);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(626, 60);
            this.txtMensagem.TabIndex = 0;
            // 
            // panelHeaderChat
            // 
            this.panelHeaderChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelHeaderChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeaderChat.Controls.Add(this.lblNomeContato);
            this.panelHeaderChat.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderChat.Location = new System.Drawing.Point(0, 0);
            this.panelHeaderChat.Name = "panelHeaderChat";
            this.panelHeaderChat.Size = new System.Drawing.Size(646, 60);
            this.panelHeaderChat.TabIndex = 0;
            // 
            // lblNomeContato
            // 
            this.lblNomeContato.AutoSize = true;
            this.lblNomeContato.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblNomeContato.Location = new System.Drawing.Point(20, 17);
            this.lblNomeContato.Name = "lblNomeContato";
            this.lblNomeContato.Size = new System.Drawing.Size(152, 25);
            this.lblNomeContato.TabIndex = 0;
            this.lblNomeContato.Text = "Nome Contato";
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelBotoes.Controls.Add(this.btnFechar);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(0, 630);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Size = new System.Drawing.Size(1000, 60);
            this.panelBotoes.TabIndex = 2;
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(850, 12);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(120, 35);
            this.btnFechar.TabIndex = 0;
            this.btnFechar.Text = "❌ Fechar";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // FormMensagens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 690);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelBotoes);
            this.Controls.Add(this.panelTopo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMensagens";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mensagens - BookConnect";
            this.panelTopo.ResumeLayout(false);
            this.panelTopo.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelConversas.ResumeLayout(false);
            this.panelChat.ResumeLayout(false);
            this.panelMensagens.ResumeLayout(false);
            this.panelEnviar.ResumeLayout(false);
            this.panelEnviar.PerformLayout();
            this.panelHeaderChat.ResumeLayout(false);
            this.panelHeaderChat.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTopo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelConversas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutConversas;
        private System.Windows.Forms.Label lblConversas;
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.Panel panelHeaderChat;
        private System.Windows.Forms.Label lblNomeContato;
        private System.Windows.Forms.Panel panelMensagens;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutMensagens;
        private System.Windows.Forms.Panel panelEnviar;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnFechar;
    }
}