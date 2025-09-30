using System;
using System.Windows.Forms;
using gosti2;
using gosti2.Data;
using gosti2.Tools;

namespace gosti2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ReferenceVerifier.VerificarTodasReferencias();

            // Para testes simples
            if (ReferenceVerifier.VerificacaoRapida())
            {
                Console.WriteLine("✅ Sistema básico operacional");
            }
            else
            {
                Console.WriteLine("❌ Problemas críticos detectados");
            }

            try
            {
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
            Console.WriteLine("🚀 Iniciando inicialização da aplicação...");

            // 1. ✅ VERIFICAR REFERÊNCIAS (OPCIONAL - NÃO CRÍTICO)
            try
            {
                ReferenceVerifier.VerificarTodasReferencias();
                Console.WriteLine("✅ Verificação de referências concluída.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Aviso na verificação de referências: {ex.Message}");
                // Continua mesmo com erro (não é crítico)
            }

            // 2. ✅ INICIALIZAÇÃO BÁSICA DO BANCO
            try
            {
                DatabaseInitializer.Initialize();
                Console.WriteLine("✅ Inicialização do banco concluída.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro crítico na inicialização do banco: {ex.Message}",
                    "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 3. ✅ GARANTIR QUE BANCO EXISTE E ESTÁ ACESSÍVEL
            try
            {
                DatabaseManager.GarantirBancoCriado();
                Console.WriteLine("✅ Verificação de existência do banco concluída.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao garantir criação do banco: {ex.Message}",
                    "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 4. ✅ TESTAR CONEXÃO COM BANCO
            if (!DatabaseManager.TestarConexao())
            {
                Console.WriteLine("❌ Conexão com banco falhou. Abrindo configuração...");

                // Se não conectar, mostra tela de configuração
                using (var formConfig = new FormConfiguracaoBanco())
                {
                    if (formConfig.ShowDialog() != DialogResult.OK)
                    {
                        Console.WriteLine("❌ Usuário cancelou configuração do banco.");
                        return false; // Usuário cancelou
                    }
                }

                // Testa novamente após configuração
                if (!DatabaseManager.TestarConexao())
                {
                    MessageBox.Show("Não foi possível estabelecer conexão com o banco de dados mesmo após configuração.",
                        "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            Console.WriteLine("✅ Conexão com banco estabelecida com sucesso.");

            // 5. ✅ VERIFICAR E ATUALIZAR BANCO (EVOLUÇÃO)
            try
            {
                DatabaseEvolutionManager.VerificarEAtualizarBanco();
                Console.WriteLine("✅ Verificação de evolução do banco concluída.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Aviso na atualização do banco: {ex.Message}");
                // Continua mesmo com erro (não é crítico)
            }

            // 6. ✅ VALIDAR ESQUEMA (SE O MÉTODO EXISTIR - CORRIGIDO)
            try
            {
                // ✅ CORREÇÃO: Verifica se o método existe usando reflexão
                var tipo = typeof(DatabaseSchemaValidator);
                var metodo = tipo.GetMethod("ValidarEsquema");

                if (metodo != null)
                {
                    // ✅ Agora chama o método corretamente
                    var resultado = (bool)metodo.Invoke(null, null);
                    if (!resultado)
                    {
                        Console.WriteLine("⚠️  Problemas no esquema do banco detectados.");
                    }
                    else
                    {
                        Console.WriteLine("✅ Esquema do banco validado com sucesso.");
                    }
                }
                else
                {
                    Console.WriteLine("ℹ️  Método ValidarEsquema não encontrado, continuando...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Erro na validação do esquema: {ex.Message}");
                // Não bloqueia a aplicação
            }

            Console.WriteLine("🎉 Inicialização da aplicação concluída com sucesso!");
            return true;
        }

        /// <summary>
        /// Executa o fluxo principal da aplicação
        /// </summary>
        private static void ExecutarAplicacaoPrincipal()
        {
            try
            {
                Console.WriteLine("👉 Iniciando aplicação principal...");

                // ✅ OPÇÃO 1: Tela de boas-vindas inicial (FormMain)
                using (var formMain = new FormMain())
                {
                    if (formMain.ShowDialog() == DialogResult.OK)
                    {
                        // ✅ SE USUÁRIO CONFIRMOU, INICIA APLICAÇÃO PRINCIPAL
                        Console.WriteLine("✅ Usuário confirmou, iniciando FormLogin...");
                        Application.Run(new FormLogin());
                    }
                    else
                    {
                        // Usuário cancelou na tela inicial
                        Console.WriteLine("❌ Usuário cancelou na tela inicial.");
                        MessageBox.Show("Aplicação cancelada pelo usuário.",
                            "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // ✅ OPÇÃO 2: Iniciar diretamente o FormLogin (mais simples)
                // Console.WriteLine("👉 Iniciando FormLogin diretamente...");
                // Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar aplicação principal: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"❌ Erro na aplicação principal: {ex}");
            }
        }

        // ✅ MÉTODO ALTERNATIVO SIMPLIFICADO
        private static void ExecutarAplicacaoSimplificada()
        {
            // Versão mais direta se estiver com problemas
            try
            {
                Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n\nTente reiniciar a aplicação.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}