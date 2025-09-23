using System;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;
namespace gosti2
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            CarregarDadosUsuario();
        }
       
        private void CarregarDadosUsuario()
        {
            if (UsuarioManager.UsuarioLogado != null)
            {
                var usuario = UsuarioManager.UsuarioLogado;

                // Atualiza os labels
                lblUsuario.Text = $"Bem-vindo, {usuario.Nome}!";
                lblBemVindo.Text = $"Olá, {usuario.Nome.Split(' ')[0]}!";
                lblBio.Text = string.IsNullOrEmpty(usuario.Bio) ? "🌟 Apaixonado por livros e novas histórias..." : usuario.Bio;

                // Atualiza a foto de perfil se existir
                if (usuario.FotoPerfil != null && usuario.FotoPerfil.Length > 0)
                {
                    using (var ms = new System.IO.MemoryStream(usuario.FotoPerfil))
                    {
                        pictureBoxPerfil.Image = System.Drawing.Image.FromStream(ms);
                    }
                }

                // Carrega estatísticas (exemplo)
                CarregarEstatisticas();
            }
        }

        private void CarregarEstatisticas()
        {
            // Exemplo de estatísticas - você pode implementar a lógica real
            lblLivrosCadastrados.Text = "12";
            lblAmigos.Text = "8";
            lblMensagens.Text = "24";
        }

        private void btnLivros_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var formLogin = new FormMeusLivros())
            {
                if (formLogin.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            this.Show();
        }

        private void btnMensagens_Click(object sender, EventArgs e)
        {
            MessageBox.Show("✉️ Funcionalidade de Mensagens em desenvolvimento!",
                          "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTierList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("⭐ Funcionalidade de Tier Lists em desenvolvimento!",
                          "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("👤 Funcionalidade de Perfil em desenvolvimento!",
                          "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UsuarioManager.Logout();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void listBoxAtividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pode ser implementado para mostrar detalhes da atividade selecionada
        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {
            // Mantido para compatibilidade
        }

        private void pictureBoxPerfil_Click(object sender, EventArgs e)
        {
            // Futura implementação para alterar foto de perfil
            MessageBox.Show("🖼️ Funcionalidade de alterar foto em desenvolvimento!",
                          "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}