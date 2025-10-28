using System;
using System.Drawing;
using System.IO;
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
            //lblBemVindo.Text = $"Olá, {usuario.NomeUsuario.Split(' ')[0]}!";
            lblBio.Text = string.IsNullOrEmpty(usuario.Bio)
                ? "🌟 Apaixonado por livros..."
                : usuario.Bio;

            // CARREGA A FOTO DO USUÁRIO
            CarregarFotoPerfil();

            CarregarEstatisticas();
        }

        private void CarregarFotoPerfil()
        {
            var usuario = AppManager.UsuarioLogado;

            try
            {
                if (usuario.FotoPerfil != null && usuario.FotoPerfil.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(usuario.FotoPerfil))
                    {
                        pictureBoxPerfil.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    // Foto padrão (cinza)
                    pictureBoxPerfil.BackColor = Color.LightGray;
                }
            }
            catch
            {
                pictureBoxPerfil.BackColor = Color.LightGray;
            }
        }

        private void CarregarEstatisticas()
        {
            try
            {
                var (totalLivros, livrosLidos, livrosFavoritos) =
                    AppManager.ObterEstatisticasUsuario();

                lblLivrosCadastrados.Text = totalLivros.ToString();
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
            this.Hide();
            using (var FormTierList = new FormTierList())
            {
                FormTierList.ShowDialog();
            }
            this.Show();
            CarregarEstatisticas();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var FormMeuPerfil = new FormMeuPerfil())
            {
                FormMeuPerfil.ShowDialog();
            }
            this.Show();
            CarregarEstatisticas();
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

        private void listBoxAtividades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxPerfil_Click(object sender, EventArgs e)
        {

        }

        private void lblLivrosCadastrados_Click(object sender, EventArgs e)
        {

        }
    }
}