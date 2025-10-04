using System;
using System.Windows.Forms;
using gosti2.Data;
using gosti2.Tools;

namespace gosti2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // ✅ CONFIGURAÇÃO INICIAL DO DIAGNOSTIC CONTEXT
            DiagnosticContext.FormularioAtual = "Program";
            DiagnosticContext.MetodoAtual = "Main";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DiagnosticContext.LogarInfo("Iniciando aplicação BookConnect...");
                ReferenceVerifier.VerificarTodasReferencias();

                // Para testes simples
                if (ReferenceVerifier.VerificacaoRapida())
                {
                    DiagnosticContext.LogarInfo("✅ Sistema básico operacional");
                }
                else
                {
                    DiagnosticContext.LogarErro("❌ Problemas críticos detectados nas referências",
                        new Exception("Verificação rápida de referências falhou"));
                }

                // ✅ FLUXO ÚNICO E CORRETO DE INICIALIZAÇÃO
                if (InicializarAplicacao())
                {
                    ExecutarAplicacaoPrincipal();
                }
                else
                {
                    MessageBox.Show("Falha na inicialização da aplicação. Verifique os logs para mais detalhes.",
                        "Erro de Inicialização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro fatal na inicialização da aplicação", ex);
                MessageBox.Show($"Erro fatal na inicialização: {ex.Message}",
                    "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Inicializa todos os componentes da aplicação na ordem correta
        /// </summary>
        private static bool InicializarAplicacao()
        {
            DiagnosticContext.LogarInfo("🚀 Iniciando inicialização da aplicação...");
            DiagnosticContext.FormularioAtual = "Program";
            DiagnosticContext.MetodoAtual = "InicializarAplicacao";

            // 1. ✅ VERIFICAR REFERÊNCIAS
            try
            {
                DiagnosticContext.LogarInfo("Verificando referências...");
                ReferenceVerifier.VerificarTodasReferencias();
                DiagnosticContext.LogarInfo("Verificação de referências concluída.");
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Falha na verificação de referências", ex);
                // Continua mesmo com erro (não é crítico)
            }

            // 2. ✅ INICIALIZAÇÃO BÁSICA DO BANCO
            try
            {
                DiagnosticContext.LogarInfo("Inicializando banco...");
                DatabaseInitializer.Initialize();
                DiagnosticContext.LogarInfo("Inicialização do banco concluída.");
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro crítico na inicialização do banco", ex);
                MessageBox.Show($"Erro crítico na inicialização do banco: {ex.Message}",
                    "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 3. ✅ GARANTIR QUE BANCO EXISTE
            try
            {
                DiagnosticContext.ExecutarComLog(() =>
                {
                    DatabaseManager.GarantirBancoCriado();
                    return true;
                }, "GarantirBancoCriado");
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro ao garantir criação do banco", ex);
                MessageBox.Show($"Erro ao garantir criação do banco: {ex.Message}",
                    "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 4. ✅ TESTAR CONEXÃO COM BANCO
            bool conexaoSucesso = false;
            try
            {
                conexaoSucesso = DiagnosticContext.ExecutarComLog(() =>
                    DatabaseManager.TestarConexao(), "TestarConexao");
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Exceção ao testar conexão com banco", ex);
                conexaoSucesso = false;
            }

            if (!conexaoSucesso)
            {
                DiagnosticContext.LogarErro("Conexão com banco falhou",
                    new Exception("Teste de conexão retornou false"));

                // Se não conectar, mostra tela de configuração
                using (var formConfig = new FormConfiguracaoBanco())
                {
                    if (formConfig.ShowDialog() != DialogResult.OK)
                    {
                        DiagnosticContext.LogarInfo("Usuário cancelou configuração do banco");
                        return false;
                    }
                }

                // Testa novamente após configuração
                try
                {
                    conexaoSucesso = DiagnosticContext.ExecutarComLog(() =>
                        DatabaseManager.TestarConexao(), "TestarConexaoPosConfig");
                }
                catch (Exception ex)
                {
                    DiagnosticContext.LogarErro("Exceção no segundo teste de conexão", ex);
                    conexaoSucesso = false;
                }

                if (!conexaoSucesso)
                {
                    DiagnosticContext.LogarErro("Conexão falhou mesmo após configuração",
                        new Exception("Segundo teste de conexão também falhou"));
                    MessageBox.Show("Não foi possível estabelecer conexão com o banco de dados mesmo após configuração.",
                        "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            DiagnosticContext.LogarInfo("Conexão com banco estabelecida com sucesso");

            // 5. ✅ VERIFICAR E ATUALIZAR BANCO
            try
            {
                DiagnosticContext.ExecutarComLog(() =>
                {
                    DatabaseEvolutionManager.VerificarEAtualizarBanco();
                    return true;
                }, "VerificarEAtualizarBanco");
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro na atualização do banco", ex);
                // Não bloqueia a aplicação - continua
            }

            // 6. ✅ VALIDAR ESQUEMA
            try
            {
                var tipo = typeof(DatabaseSchemaValidator);
                var metodo = tipo.GetMethod("ValidarEsquema");

                if (metodo != null)
                {
                    bool resultado = DiagnosticContext.ExecutarComLog(() =>
                        (bool)metodo.Invoke(null, null), "ValidarEsquema");

                    if (!resultado)
                    {
                        DiagnosticContext.LogarAviso("Problemas no esquema do banco detectados");
                    }
                    else
                    {
                        DiagnosticContext.LogarInfo("Esquema do banco validado com sucesso");
                    }
                }
                else
                {
                    DiagnosticContext.LogarInfo("Método ValidarEsquema não encontrado - continuando...");
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro na validação do esquema", ex);
                // Não bloqueia a aplicação
            }

            DiagnosticContext.LogarInfo("🎉 Inicialização da aplicação concluída com sucesso!");
            return true;
        }

        /// <summary>
        /// Executa o fluxo principal da aplicação
        /// </summary>
        private static void ExecutarAplicacaoPrincipal()
        {
            try
            {
                DiagnosticContext.FormularioAtual = "Program";
                DiagnosticContext.MetodoAtual = "ExecutarAplicacaoPrincipal";
                DiagnosticContext.LogarInfo("👉 Iniciando aplicação principal...");

                // ✅ OPÇÃO 1: Tela de boas-vindas inicial (FormMain)
                using (var formMain = new FormMain())
                {
                    if (formMain.ShowDialog() == DialogResult.OK)
                    {
                        // ✅ SE USUÁRIO CONFIRMOU, INICIA APLICAÇÃO PRINCIPAL
                        DiagnosticContext.LogarInfo("✅ Usuário confirmou, iniciando FormLogin...");
                        Application.Run(new FormLogin());
                    }
                    else
                    {
                        // Usuário cancelou na tela inicial
                        DiagnosticContext.LogarInfo("❌ Usuário cancelou na tela inicial.");
                        MessageBox.Show("Aplicação cancelada pelo usuário.",
                            "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro ao iniciar aplicação principal", ex);
                MessageBox.Show($"Erro ao iniciar aplicação principal: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // ✅ FALLBACK: Tentar método simplificado
                ExecutarAplicacaoSimplificada();
            }
        }

        // ✅ MÉTODO ALTERNATIVO SIMPLIFICADO
        private static void ExecutarAplicacaoSimplificada()
        {
            DiagnosticContext.FormularioAtual = "Program";
            DiagnosticContext.MetodoAtual = "ExecutarAplicacaoSimplificada";

            try
            {
                DiagnosticContext.LogarInfo("Tentando inicialização simplificada...");
                Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro na inicialização simplificada", ex);
                MessageBox.Show($"Erro: {ex.Message}\n\nTente reiniciar a aplicação.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}