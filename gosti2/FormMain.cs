using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormMain : Form
    {
        private Usuario usuarioLogado;

        public FormMain()
        {
            InitializeComponent();
            ConfigurarInterface();
            CarregarDadosUsuario();
        }

        private void ConfigurarInterface()
        {
            // Configurar título dinâmico
            this.Text = $"BookConnect - Rede Social Literária";

            // Configurar tooltips
            var toolTip = new ToolTip();
            toolTip.SetToolTip(btnMeusLivros, "Acesse sua biblioteca pessoal");
            toolTip.SetToolTip(btnExplorar, "Descubra novos livros e usuários");
            toolTip.SetToolTip(btnPerfil, "Configure seu perfil e preferências");
            toolTip.SetToolTip(btnSair, "Sair do sistema");

            // Configurar eventos de hover para melhor UX
            ConfigurarEfeitosHover();

            // Verificar se o banco está inicializado
            VerificarEstadoBanco();
        }

        private void ConfigurarEfeitosHover()
        {
            var botoes = new[] { btnMeusLivros, btnExplorar, btnPerfil, btnSair };

            foreach (var botao in botoes)
            {
                botao.MouseEnter += (s, e) =>
                {
                    botao.BackColor = Color.FromArgb(80, 150, 220);
                    botao.ForeColor = Color.White;
                };

                botao.MouseLeave += (s, e) =>
                {
                    botao.BackColor = Color.FromArgb(70, 130, 180);
                    botao.ForeColor = Color.White;
                };
            }
        }

        private void CarregarDadosUsuario()
        {
            try
            {
                usuarioLogado = UsuarioManager.UsuarioLogado;

                if (usuarioLogado != null)
                {
                    lblBoasVindas.Text = $"🌟 Bem-vindo(a), {usuarioLogado.NomeUsuario}!";
                    lblEmail.Text = $"📧 {usuarioLogado.Email}";

                    // Carregar estatísticas do usuário
                    CarregarEstatisticas();
                }
                else
                {
                    lblBoasVindas.Text = "🌟 Bem-vindo ao BookConnect!";
                    lblEmail.Text = "📧 Faça login para acessar todos os recursos";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados do usuário: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarregarEstatisticas()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuarioId = usuarioLogado.UsuarioId;

                    // CORREÇÃO: Usar System.Linq para Count()
                    var totalLivros = context.Livros.Count(l => l.UsuarioId == usuarioId);
                    var livrosLidos = context.Livros.Count(l => l.UsuarioId == usuarioId && l.Lido);
                    var livrosFavoritos = context.Livros.Count(l => l.UsuarioId == usuarioId && l.Favorito);

                    lblEstatisticas.Text = $"📚 {totalLivros} Livros | ✅ {livrosLidos} Lidos | ⭐ {livrosFavoritos} Favoritos";
                }
            }
            catch (Exception ex)
            {
                lblEstatisticas.Text = "📚 Estatísticas indisponíveis";
                Console.WriteLine($"Erro ao carregar estatísticas: {ex.Message}");
            }
        }

        private void VerificarEstadoBanco()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // CORREÇÃO: Usar System.Linq para Count()
                    var usuariosCount = context.Usuarios.Count();
                    lblStatusBanco.Text = "✅ Banco de dados conectado";
                    lblStatusBanco.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblStatusBanco.Text = "❌ Problema com o banco de dados";
                lblStatusBanco.ForeColor = Color.Red;

                MessageBox.Show($"Problema de conexão com o banco: {ex.Message}\n\n" +
                    "Verifique as configurações do banco de dados.",
                    "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMeusLivros_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioLogado == null)
                {
                    MessageBox.Show("Faça login para acessar sua biblioteca.",
                        "Login Necessário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Hide();
                using (var formMeusLivros = new FormMeusLivros())
                {
                    formMeusLivros.ShowDialog();
                }
                this.Show();

                // Atualizar estatísticas após retornar
                CarregarEstatisticas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir biblioteca: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExplorar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                using (var formPrincipal = new FormPrincipal())
                {
                    formPrincipal.ShowDialog();
                }
                this.Show();

                // Atualizar estatísticas após retornar
                CarregarEstatisticas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao explorar livros: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioLogado == null)
                {
                    MessageBox.Show("Faça login para acessar seu perfil.",
                        "Login Necessário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // TODO: Implementar FormPerfil quando disponível
                MessageBox.Show("📋 Funcionalidade de perfil em desenvolvimento!\n\n" +
                    "Em breve você poderá:\n" +
                    "• Editar seu perfil\n" +
                    "• Alterar foto\n" +
                    "• Configurar preferências\n" +
                    "• Ver seu histórico",
                    "Em Desenvolvimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao acessar perfil: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do BookConnect?\n\n" +
                "Você será desconectado e retornará à tela de login.",
                "Confirmar Saída",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Limpar sessão do usuário
                UsuarioManager.Logout();

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                using (var formConfig = new FormConfiguracaoBanco())
                {
                    if (formConfig.ShowDialog() == DialogResult.OK)
                    {
                        // Recarregar dados se a configuração foi alterada
                        CarregarDadosUsuario();
                        VerificarEstadoBanco();
                    }
                }
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir configurações: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblBoasVindas_Click(object sender, EventArgs e)
        {
            // Atualizar dados quando o usuário clicar na boas-vindas
            CarregarDadosUsuario();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Confirmar saída se o usuário fechar a janela
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Deseja realmente sair do BookConnect?",
                    "Confirmar Saída",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    UsuarioManager.Logout();
                }
            }
        }

        private void timerAtualizacao_Tick(object sender, EventArgs e)
        {
            // Atualizar hora atual
            lblHoraAtual.Text = DateTime.Now.ToString("HH:mm:ss");

            // Atualizar estatísticas periodicamente (a cada 30 segundos)
            if (DateTime.Now.Second % 30 == 0)
            {
                CarregarEstatisticas();
            }
        }

        private void linkSobre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("📚 BookConnect - Rede Social Literária\n\n" +
                "Versão: 2.0.0\n" +
                "Desenvolvido por: Gabriel Osti\n\n" +
                "Recursos:\n" +
                "• Biblioteca pessoal de livros\n" +
                "• Rede social literária\n" +
                "• Sistema de avaliações\n" +
                "• Comunidade de leitores\n\n" +
                "© 2024 - Todos os direitos reservados",
                "Sobre o BookConnect", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}