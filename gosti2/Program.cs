using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Linq;
using gosti2.Data;
using gosti2.Tools;

namespace gosti2
{
    internal static class Program
    {
        private static readonly string AppName = "BookConnect";
        private static readonly string AppVersion = "2.0.0";

        [STAThread]
        static void Main()
        {
            // ✅ CONFIGURAÇÃO GLOBAL DA APLICAÇÃO
            ConfigureApplicationGlobalSettings();

            // ✅ INICIALIZAÇÃO COM TRATAMENTO DE EXCEÇÕES GLOBAL
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ThreadExceptionHandler;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                LogStartup("🚀 Iniciando aplicação BookConnect...");

                // ✅ INICIALIZAÇÃO EM ETAPAS COM FALBACK
                var initializationResult = ExecuteInitializationPipeline();

                if (initializationResult.Success)
                {
                    LogSuccess("✅ Inicialização concluída com sucesso");
                    RunApplication();
                }
                else
                {
                    HandleInitializationFailure(initializationResult);
                }
            }
            catch (Exception ex)
            {
                HandleCriticalFailure(ex);
            }
            finally
            {
                LogShutdown("📋 Aplicação finalizada");
            }
        }

        #region 🔧 CONFIGURAÇÕES GLOBAIS

        private static void ConfigureApplicationGlobalSettings()
        {
            // ✅ CONFIGURAÇÃO DE CULTURA (EVITA PROBLEMAS DE FORMATAÇÃO)
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("pt-BR");

            // ✅ CONFIGURAÇÃO DE DIAGNÓSTICO
            DiagnosticContext.FormularioAtual = "Program";
            DiagnosticContext.MetodoAtual = "Main";

            // ✅ CONFIGURAÇÃO DE PERFORMANCE
            Application.UseWaitCursor = false;
        }

        #endregion

        #region 🎯 PIPELINE DE INICIALIZAÇÃO

        private class InitializationResult
        {
            public bool Success { get; set; }
            public string FailureStep { get; set; }
            public Exception Exception { get; set; }
            public bool CanRecover { get; set; }
        }

        private static InitializationResult ExecuteInitializationPipeline()
        {
            var result = new InitializationResult { Success = true };

            // ✅ ETAPA 1: VERIFICAÇÃO DE SISTEMA BÁSICO
            if (!ExecuteStep("Verificação de Sistema", BasicSystemCheck, result))
                return result;

            // ✅ ETAPA 2: VERIFICAÇÃO DE REFERÊNCIAS
            if (!ExecuteStep("Verificação de Referências", CheckReferences, result))
                return result;

            // ✅ ETAPA 3: CONFIGURAÇÃO DO BANCO DE DADOS
            if (!ExecuteStep("Configuração do Banco", DatabaseConfiguration, result))
                return result;

            // ✅ ETAPA 4: VALIDAÇÃO FINAL DO SISTEMA
            if (!ExecuteStep("Validação Final", FinalSystemValidation, result))
                return result;

            return result;
        }

        private static bool ExecuteStep(string stepName, Func<bool> stepAction, InitializationResult result)
        {
            try
            {
                LogStepStart(stepName);

                var success = stepAction();

                if (success)
                {
                    LogStepSuccess(stepName);
                    return true;
                }
                else
                {
                    result.Success = false;
                    result.FailureStep = stepName;
                    result.CanRecover = false;
                    LogStepFailure(stepName, "Ação retornou false");
                    return false;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.FailureStep = stepName;
                result.Exception = ex;
                result.CanRecover = IsRecoverableError(ex);
                LogStepFailure(stepName, ex.Message);
                return false;
            }
        }

        #endregion

        #region 🔍 ETAPAS DE INICIALIZAÇÃO

        private static bool BasicSystemCheck()
        {
            // ✅ VERIFICAÇÕES CRÍTICAS DO SISTEMA
            if (!Environment.Is64BitProcess)
            {
                LogWarning("Aplicação executando em 32-bit - pode afetar performance");
            }

            // Verifica memória disponível
            var memoryAvailable = GC.GetTotalMemory(false) > 1000000; // ~1MB
            if (!memoryAvailable)
            {
                LogError("Memória insuficiente para executar a aplicação");
                return false;
            }

            return true;
        }

        private static bool CheckReferences()
        {
            try
            {
                // ✅ VERIFICAÇÃO COMPLETA DE REFERÊNCIAS
                ReferenceVerifier.VerificarTodasReferencias();

                // Verificação rápida adicional
                if (!ReferenceVerifier.VerificacaoRapida())
                {
                    LogWarning("Verificação rápida detectou possíveis problemas");
                    // Não bloqueia, apenas registra aviso
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError("Falha na verificação de referências", ex);
                // Referências são críticas - falha na inicialização
                return false;
            }
        }

        private static bool DatabaseConfiguration()
        {
            // ✅ CONFIGURAÇÃO ROBUSTA DO BANCO
            try
            {
                // 1. Inicialização básica
                DatabaseInitializer.Initialize();

                // 2. Garantir que banco existe
                DatabaseManager.GarantirBancoCriado();

                // 3. Testar conexão
                if (!DatabaseManager.TestarConexao())
                {
                    LogWarning("Conexão inicial com banco falhou - tentando recuperação");

                    // ✅ TENTATIVA DE RECUPERAÇÃO AUTOMÁTICA
                    if (!AttemptDatabaseRecovery())
                    {
                        return false;
                    }
                }

                // 4. Atualização do schema
                DatabaseEvolutionManager.VerificarEAtualizarBanco();

                // 5. Validação final
                return ValidateDatabaseSchema();
            }
            catch (Exception ex)
            {
                LogError("Falha crítica na configuração do banco", ex);
                return false;
            }
        }

        private static bool AttemptDatabaseRecovery()
        {
            try
            {
                LogInfo("Tentando recuperação automática do banco...");

                // Mostra diálogo de configuração
                using (var configForm = new FormConfiguracaoBanco())
                {
                    var result = configForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Testa novamente após configuração
                        if (DatabaseManager.TestarConexao())
                        {
                            LogSuccess("Recuperação do banco bem-sucedida");
                            return true;
                        }
                    }
                }

                LogError("Recuperação do banco falhou");
                return false;
            }
            catch (Exception ex)
            {
                LogError("Erro durante recuperação do banco", ex);
                return false;
            }
        }

        private static bool ValidateDatabaseSchema()
        {
            try
            {
                // ✅ VALIDAÇÃO FLEXÍVEL DO SCHEMA
                var validatorType = typeof(DatabaseSchemaValidator);
                var validateMethod = validatorType.GetMethod("ValidarEsquema");

                if (validateMethod != null)
                {
                    var isValid = (bool)validateMethod.Invoke(null, null);

                    if (!isValid)
                    {
                        LogWarning("Problemas não-críticos detectados no schema do banco");
                        // Continua mesmo com problemas não-críticos
                    }

                    return true; // Schema validation não é crítica
                }

                LogInfo("Validador de schema não disponível - continuando...");
                return true;
            }
            catch (Exception ex)
            {
                //LogWarning("Validação de schema falhou", ex);
                return true; // Não crítica
            }
        }

        private static bool FinalSystemValidation()
        {
            // ✅ VERIFICAÇÕES FINAIS ANTES DE INICIAR
            try
            {
                // Verifica se forms principais podem ser instanciados
                var testForms = new Form[]
                {
                    new FormLogin(),
                    new FormMain()
                };

                foreach (var form in testForms)
                {
                    form.Dispose();
                }

                LogSuccess("Todos os componentes validados com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                LogError("Falha na validação final", ex);
                return false;
            }
        }

        #endregion

        #region 🎮 EXECUÇÃO DA APLICAÇÃO

        private static void RunApplication()
        {
            try
            {
                LogInfo("Iniciando interface do usuário...");

                // ✅ FLUXO PRINCIPAL COM FALLBACK
                var mainForm = CreateMainForm();

                if (mainForm != null)
                {
                    Application.Run(mainForm);
                }
                else
                {
                    throw new InvalidOperationException("Não foi possível criar o formulário principal");
                }
            }
            catch (Exception ex)
            {
                LogError("Erro ao executar aplicação", ex);
                FallbackToSimpleMode();
            }
        }

        private static Form CreateMainForm()
        {
            // ✅ LÓGICA INTELIGENTE DE SELEÇÃO DO FORM PRINCIPAL
            try
            {
                // Tenta mostrar tela de boas-vindas primeiro
                using (var welcomeForm = new FormMain())
                {
                    if (welcomeForm.ShowDialog() == DialogResult.OK)
                    {
                        LogInfo("Usuário confirmou na tela de boas-vindas");
                        return new FormLogin();
                    }
                    else
                    {
                        LogInfo("Usuário cancelou na tela de boas-vindas");
                        return null; // Encerra aplicação
                    }
                }
            }
            catch (Exception ex)
            {
                //LogWarning("Tela de boas-vindas falhou", ex);
                // Fallback para login direto
                return new FormLogin();
            }
        }

        private static void FallbackToSimpleMode()
        {
            try
            {
                LogWarning("Iniciando modo de fallback simplificado...");

                // ✅ MODO DE EMERGÊNCIA - MÍNIMO FUNCIONAL
                Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                LogError("Modo de fallback também falhou", ex);
                ShowFinalErrorMessage(ex);
            }
        }

        #endregion

        #region 🛡️ TRATAMENTO DE ERROS

        private static void HandleInitializationFailure(InitializationResult result)
        {
            var errorMessage = "Falha na inicialização na etapa: " + result.FailureStep;

            if (result.Exception != null)
            {
                errorMessage += "\n\nErro: " + result.Exception.Message;
            }

            if (result.CanRecover)
            {
                LogWarning("Inicialização falhou mas é recuperável: " + result.FailureStep);

                var userChoice = MessageBox.Show(
                    errorMessage + "\n\nDeseja tentar executar em modo de segurança?",
                    "Problema na Inicialização",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (userChoice == DialogResult.Yes)
                {
                    RunSafeMode();
                    return;
                }
            }

            ShowFinalErrorMessage(result.Exception ?? new Exception(errorMessage));
        }

        private static void RunSafeMode()
        {
            try
            {
                LogWarning("Executando em modo de segurança...");

                // ✅ MODO SEGURO - FUNCIONALIDADES BÁSICAS
                Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                HandleCriticalFailure(ex);
            }
        }

        private static void HandleCriticalFailure(Exception ex)
        {
            LogError("Falha crítica: " + ex.ToString(), ex);

            var detailedMessage = "Ocorreu um erro crítico na aplicação.\n\n" +
                                "Erro: " + ex.Message + "\n\n" +
                                "Detalhes técnicos foram registrados para análise.\n" +
                                "A aplicação será encerrada.\n\n" +
                                "Versão: " + AppVersion + " " + AppName;

            MessageBox.Show(detailedMessage,
                "Erro Crítico - Aplicação Será Encerrada",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Environment.Exit(1);
        }

        private static void ShowFinalErrorMessage(Exception ex)
        {
            var message = "Não foi possível iniciar a aplicação.\n\n" +
                         "Erro: " + ex.Message + "\n\n" +
                         "Tente:\n" +
                         "• Reiniciar a aplicação\n" +
                         "• Verificar se o SQL Server está executando\n" +
                         "• Contatar o suporte técnico\n\n" +
                         "Versão: " + AppVersion;

            MessageBox.Show(message,
                "Erro de Inicialização",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            LogError("Exceção não tratada (Global): " + ex?.ToString(), ex);
        }

        private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            LogError("Exceção de thread não tratada: " + e.Exception.ToString(), e.Exception);
        }

        #endregion

        #region 📝 SISTEMA DE LOGGING MELHORADO

        private static void LogStartup(string message)
        {
            DiagnosticContext.LogarInfo(message + " - " + AppName + " v" + AppVersion);
            Console.WriteLine("🔔 " + DateTime.Now.ToString("HH:mm:ss") + " " + message);
        }

        private static void LogShutdown(string message)
        {
            DiagnosticContext.LogarInfo(message);
            Console.WriteLine("🔔 " + DateTime.Now.ToString("HH:mm:ss") + " " + message);
        }

        private static void LogStepStart(string stepName)
        {
            DiagnosticContext.LogarInfo("Iniciando: " + stepName);
            Console.WriteLine("⏳ " + DateTime.Now.ToString("HH:mm:ss") + " Iniciando: " + stepName);
        }

        private static void LogStepSuccess(string stepName)
        {
            DiagnosticContext.LogarInfo("Concluído: " + stepName);
            Console.WriteLine("✅ " + DateTime.Now.ToString("HH:mm:ss") + " Concluído: " + stepName);
        }

        private static void LogStepFailure(string stepName, string error)
        {
            DiagnosticContext.LogarErro("Falha em " + stepName + ": " + error, new Exception(error));
            Console.WriteLine("❌ " + DateTime.Now.ToString("HH:mm:ss") + " Falha em " + stepName + ": " + error);
        }

        private static void LogInfo(string message)
        {
            DiagnosticContext.LogarInfo(message);
            Console.WriteLine("ℹ️ " + DateTime.Now.ToString("HH:mm:ss") + " " + message);
        }

        private static void LogSuccess(string message)
        {
            DiagnosticContext.LogarInfo(message);
            Console.WriteLine("✅ " + DateTime.Now.ToString("HH:mm:ss") + " " + message);
        }

        private static void LogWarning(string message)
        {
            DiagnosticContext.LogarAviso(message);
            Console.WriteLine("⚠️ " + DateTime.Now.ToString("HH:mm:ss") + " " + message);
        }

        private static void LogError(string message)
        {
            // ✅ CORREÇÃO: Crie uma exceção genérica para o log
            DiagnosticContext.LogarErro(message, new Exception(message));
            Console.WriteLine("❌ " + DateTime.Now.ToString("HH:mm:ss") + " " + message);
        }

        private static void LogError(string message, Exception ex)
        {
            // ✅ SOBRECARGA: Aceita exceção específica
            DiagnosticContext.LogarErro(message, ex);
            Console.WriteLine("❌ " + DateTime.Now.ToString("HH:mm:ss") + " " + message + " - " + ex.Message);
        }

        private static bool IsRecoverableError(Exception ex)
        {
            // ✅ LÓGICA PARA DETERMINAR SE ERRO É RECUPERÁVEL
            var nonRecoverableErrors = new[]
            {
                "System.OutOfMemoryException",
                "System.IO.FileNotFoundException",
                "System.BadImageFormatException"
            };

            return !nonRecoverableErrors.Contains(ex.GetType().FullName);
        }

        #endregion
        private static void SafeLogError(string message, Exception ex = null)
        {
            try
            {
                if (ex != null)
                    DiagnosticContext.LogarErro(message, ex);
                else
                    DiagnosticContext.LogarErro(message, new Exception(message));
            }
            catch
            {
                // Fallback para console se DiagnosticContext falhar
                Console.WriteLine("❌ ERRO: " + message);
                if (ex != null)
                    Console.WriteLine("   Exceção: " + ex.Message);
            }
        }


    }
}