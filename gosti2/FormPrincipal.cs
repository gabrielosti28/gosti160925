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
            CarregarDadosUsuario();
        }

        private void CarregarDadosUsuario()
        {
            // CORREÇÃO: Referência direta ao AppManager
            if (AppManager.UsuarioLogado != null)
            {
                var usuario = AppManager.UsuarioLogado;

                lblUsuario.Text = $"Bem-vindo, {usuario.NomeUsuario}!";
                lblBemVindo.Text = $"Olá, {usuario.NomeUsuario.Split(' ')[0]}!";
                lblBio.Text = string.IsNullOrEmpty(usuario.Bio) ? "🌟 Apaixonado por livros..." : usuario.Bio;

                // Carrega estatísticas simples
                CarregarEstatisticas();
            }
        }

        private void CarregarEstatisticas()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuarioId = AppManager.UsuarioLogado.UsuarioId;
                    var totalLivros = context.Livros.Count(l => l.UsuarioId == usuarioId);
                    var livrosLidos = context.Livros.Count(l => l.UsuarioId == usuarioId && l.Lido);

                    lblLivrosCadastrados.Text = totalLivros.ToString();
                    // Outras estatísticas podem ser adicionadas aqui
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar estatísticas: {ex.Message}");
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
            CarregarEstatisticas(); // Atualiza estatísticas ao retornar
        }

        private void btnMensagens_Click(object sender, EventArgs e)
        {
            MessageBox.Show("✉️ Mensagens em desenvolvimento!", "Em Breve");
        }

        private void btnTierList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("⭐ Tier Lists em desenvolvimento!", "Em Breve");
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("👤 Perfil em desenvolvimento!", "Em Breve");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair?", "Confirmação",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // CORREÇÃO: Logout pelo AppManager
                AppManager.Logout();
                this.Close();
            }
        }
    }
}