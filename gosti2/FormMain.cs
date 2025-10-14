using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            ConfigurarInterface();
            CarregarDadosUsuario();
        }

        private void ConfigurarInterface()
        {
            this.Text = $"BookConnect - Rede Social Literária";
            VerificarEstadoBanco();
        }

        private void CarregarDadosUsuario()
        {
            try
            {
                // CORREÇÃO: Referência direta ao AppManager
                var usuarioLogado = AppManager.UsuarioLogado;

                if (usuarioLogado != null)
                {
                    lblBoasVindas.Text = $"🌟 Bem-vindo(a), {usuarioLogado.NomeUsuario}!";
                    lblEmail.Text = $"📧 {usuarioLogado.Email}";
                    CarregarEstatisticas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro");
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
                    var livrosFavoritos = context.Livros.Count(l => l.UsuarioId == usuarioId && l.Favorito);

                    lblEstatisticas.Text = $"📚 {totalLivros} Livros | ✅ {livrosLidos} Lidos | ⭐ {livrosFavoritos} Favoritos";
                }
            }
            catch (Exception ex)
            {
                lblEstatisticas.Text = "📚 Estatísticas indisponíveis";
            }
        }

        private void VerificarEstadoBanco()
        {
            try
            {
                // CORREÇÃO: Usa o método simplificado do AppManager
                if (AppManager.VerificarBanco())
                {
                    lblStatusBanco.Text = "✅ Banco de dados conectado";
                    lblStatusBanco.ForeColor = Color.Green;
                }
                else
                {
                    lblStatusBanco.Text = "❌ Problema com o banco";
                    lblStatusBanco.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatusBanco.Text = "❌ Erro na verificação";
                lblStatusBanco.ForeColor = Color.Red;
            }
        }

        private void btnMeusLivros_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppManager.UsuarioLogado == null)
                {
                    MessageBox.Show("Faça login para acessar sua biblioteca.", "Login Necessário");
                    return;
                }

                this.Hide();
                using (var formMeusLivros = new FormMeusLivros())
                {
                    formMeusLivros.ShowDialog();
                }
                this.Show();
                CarregarEstatisticas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair?", "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // CORREÇÃO: Logout pelo AppManager
                AppManager.Logout();
                this.Close();
            }
        }
    }
}