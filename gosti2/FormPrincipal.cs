using System;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();

            // Verificar se está logado
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário fazer login primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CarregarDadosUsuario();
        }

        private void CarregarDadosUsuario()
        {
            var usuario = AppManager.UsuarioLogado;

            lblUsuario.Text = $"Bem-vindo, {usuario.NomeUsuario}!";
            lblBemVindo.Text = $"Olá, {usuario.NomeUsuario.Split(' ')[0]}!";
            lblBio.Text = string.IsNullOrEmpty(usuario.Bio)
                ? "🌟 Apaixonado por livros..."
                : usuario.Bio;

            CarregarEstatisticas();
        }

        private void CarregarEstatisticas()
        {
            try
            {
                var (totalLivros, livrosLidos, livrosFavoritos) =
                    AppManager.ObterEstatisticasUsuario();

                lblLivrosCadastrados.Text = totalLivros.ToString();
                // Estatísticas adicionais podem ser exibidas aqui
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar estatísticas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLivros_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var formLivros = new FormMeusLivros())
            {
                formLivros.ShowDialog();
            }
            this.Show();
            CarregarEstatisticas();
        }

        private void btnMensagens_Click(object sender, EventArgs e)
        {
            MessageBox.Show("✉️ Funcionalidade de mensagens em desenvolvimento!",
                "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTierList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("⭐ Funcionalidade de tier lists em desenvolvimento!",
                "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("👤 Funcionalidade de perfil em desenvolvimento!",
                "Em Breve", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair do sistema?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppManager.Logout();
                this.Close();
            }
        }
    }
}