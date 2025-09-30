using System;
using System.Drawing;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Models;

namespace gosti2
{
    public partial class FormMenu : Form
    {
        private Usuario usuarioLogado;

        public FormMenu()
        {
            InitializeComponent();
            ConfigurarInterface();
            VerificarUsuarioLogado();
        }

        private void ConfigurarInterface()
        {
            // Configurar título dinâmico
            this.Text = "BookConnect - Menu Principal";

            // Configurar tooltips
            var toolTip = new ToolTip();
            toolTip.SetToolTip(btnLogin, "Acesse sua conta existente");
            toolTip.SetToolTip(btnCadastro, "Crie uma nova conta");
            toolTip.SetToolTip(btnExplorar, "Explore livros sem fazer login");
            toolTip.SetToolTip(btnSobre, "Sobre o BookConnect");
            toolTip.SetToolTip(btnSair, "Sair do aplicativo");

            // Configurar eventos de hover para melhor UX
            ConfigurarEfeitosHover();

            // Verificar se há usuário logado para personalizar a interface
            AtualizarInterfaceUsuario();
        }

        private void ConfigurarEfeitosHover()
        {
            var botoes = new[] { btnLogin, btnCadastro, btnExplorar, btnSobre, btnSair };

            foreach (var botao in botoes)
            {
                botao.MouseEnter += (s, e) =>
                {
                    if (botao != btnSair)
                    {
                        botao.BackColor = Color.FromArgb(80, 150, 220);
                        botao.ForeColor = Color.White;
                    }
                };

                botao.MouseLeave += (s, e) =>
                {
                    if (botao != btnSair)
                    {
                        botao.BackColor = Color.FromArgb(70, 130, 180);
                        botao.ForeColor = Color.White;
                    }
                };
            }
        }

        private void VerificarUsuarioLogado()
        {
            usuarioLogado = UsuarioManager.UsuarioLogado;
            AtualizarInterfaceUsuario();
        }

        private void AtualizarInterfaceUsuario()
        {
            if (usuarioLogado != null)
            {
                lblBoasVindas.Text = $"🌟 Olá, {usuarioLogado.NomeUsuario}!";
                lblInstrucoes.Text = "Você já está conectado. Acesse o sistema principal.";
                btnLogin.Text = "🚀 Continuar para o Sistema";
                btnLogin.BackColor = Color.FromArgb(60, 179, 113); // Verde para ação principal
            }
            else
            {
                lblBoasVindas.Text = "🌟 Bem-vindo ao BookConnect!";
                lblInstrucoes.Text = "Faça login ou crie uma conta para começar sua jornada literária.";
                btnLogin.Text = "🔑 Fazer Login";
                btnLogin.BackColor = Color.FromArgb(70, 130, 180); // Azul padrão
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioLogado != null)
                {
                    // Usuário já logado - ir direto para o sistema principal
                    this.Hide();
                    using (var formMain = new FormMain())
                    {
                        if (formMain.ShowDialog() == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            // Usuário fez logout no FormMain
                            VerificarUsuarioLogado();
                            this.Show();
                        }
                    }
                }
                else
                {
                    // Usuário não logado - mostrar tela de login
                    this.Hide();
                    using (var formLogin = new FormLogin())
                    {
                        var resultado = formLogin.ShowDialog();

                        if (resultado == DialogResult.OK)
                        {
                            // Login bem-sucedido
                            VerificarUsuarioLogado();

                            MessageBox.Show($"✅ Login realizado com sucesso!\n\nBem-vindo de volta!",
                                "Login Bem-sucedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Ir automaticamente para o sistema principal
                            using (var formMain = new FormMain())
                            {
                                if (formMain.ShowDialog() == DialogResult.OK)
                                {
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                    return;
                                }
                            }
                        }
                        else if (resultado == DialogResult.Retry)
                        {
                            // Usuário quer ir para cadastro
                            btnCadastro.PerformClick();
                            return;
                        }

                        this.Show();
                        VerificarUsuarioLogado();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao processar login: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioLogado != null)
                {
                    MessageBox.Show("📝 Você já está logado!\n\n" +
                        "Para criar uma nova conta, faça logout primeiro.",
                        "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Hide();
                using (var formCadastro = new FormCadastro())
                {
                    if (formCadastro.ShowDialog() == DialogResult.OK)
                    {
                        // Cadastro bem-sucedido - tentar login automático
                        MessageBox.Show("🎉 Cadastro realizado com sucesso!\n\n" +
                            "Sua conta foi criada. Agora você pode fazer login.",
                            "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Preencher automaticamente o email no login
                        // e focar na senha para facilitar
                        VerificarUsuarioLogado();
                    }
                }
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro no cadastro: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
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
                VerificarUsuarioLogado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro ao explorar: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("📚 BookConnect - Rede Social Literária\n\n" +
                "Versão: 2.0.0\n" +
                "Desenvolvido por: Gabriel Osti\n\n" +
                "Recursos Principais:\n" +
                "• Biblioteca pessoal de livros\n" +
                "• Rede social para leitores\n" +
                "• Sistema de avaliações e comentários\n" +
                "• Comunidade literária ativa\n" +
                "• Recomendações personalizadas\n\n" +
                "Junte-se a milhares de leitores e compartilhe sua paixão por livros!",
                "Sobre o BookConnect", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair do BookConnect?\n\n" +
                "Todos os dados não salvos serão perdidos.",
                "Confirmação de Saída",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Fazer logout se estiver logado
                if (usuarioLogado != null)
                {
                    UsuarioManager.Logout();
                }

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void lblBoasVindas_Click(object sender, EventArgs e)
        {
            // Atualizar status quando o usuário clicar na mensagem
            VerificarUsuarioLogado();
        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
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
                    // Fazer logout se estiver logado
                    if (usuarioLogado != null)
                    {
                        UsuarioManager.Logout();
                    }
                }
            }
        }

        private void linkAjuda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("🆘 Central de Ajuda - BookConnect\n\n" +
                "Precisa de ajuda? Aqui estão algumas opções:\n\n" +
                "• 📖 Explorar: Veja livros sem fazer login\n" +
                "• 🔑 Login: Acesse com email e senha\n" +
                "• 📝 Cadastro: Crie sua conta gratuitamente\n" +
                "• 🚪 Sair: Encerre o aplicativo\n\n" +
                "Dúvidas técnicas? Entre em contato com o suporte.",
                "Ajuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}