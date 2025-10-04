using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace gosti2.Tools
{
    public static class DiagnosticContext
    {
        public static int? UsuarioId { get; set; }
        public static string FormularioAtual { get; set; }
        public static string MetodoAtual { get; set; }

        private static string GetConnectionString()
        {
            // Tenta pegar do App.config ou usa uma padrão
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback para a string padrão do seu projeto
                connectionString = "Server=localhost;Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;";
            }
            return connectionString;
        }

        // Log rápido para arquivo (fallback)
        private static void LogarParaArquivo(string nivel, string mensagem, Exception ex = null)
        {
            var logEntry = $"[{nivel}] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}";
            if (ex != null)
            {
                logEntry += $"\nException: {ex.GetType().Name} - {ex.Message}";
                logEntry += $"\nStackTrace: {ex.StackTrace}";
            }

            Trace.WriteLine(logEntry);

            // Também escreve em arquivo físico
            try
            {
                var caminhoLog = AppDomain.CurrentDomain.BaseDirectory + "logs.txt";
                System.IO.File.AppendAllText(caminhoLog, logEntry + Environment.NewLine + Environment.NewLine);
            }
            catch
            {
                // Ignora erro de arquivo
            }
        }

        // Método principal de log
        public static void LogarErro(string mensagem, Exception ex)
        {
            var stackTrace = ex.StackTrace ?? new StackTrace(true).ToString();

            // 1. Log em arquivo (sempre funciona)
            LogarParaArquivo("ERROR", mensagem, ex);

            // 2. Tentar log no banco (se conectado)
            try
            {
                using (var conexao = new SqlConnection(GetConnectionString()))
                {
                    conexao.Open();

                    var comando = new SqlCommand(
                        "INSERT INTO SistemaLogs (Nivel, Mensagem, StackTrace, UsuarioId, Formulario, Metodo, ExceptionType, InnerException) " +
                        "VALUES (@Nivel, @Mensagem, @StackTrace, @UsuarioId, @Formulario, @Metodo, @ExceptionType, @InnerException)",
                        conexao);

                    comando.Parameters.AddWithValue("@Nivel", "ERROR");
                    comando.Parameters.AddWithValue("@Mensagem", mensagem);
                    comando.Parameters.AddWithValue("@StackTrace", stackTrace ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@UsuarioId", UsuarioId ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Formulario", FormularioAtual ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Metodo", MetodoAtual ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@ExceptionType", ex.GetType().Name);
                    comando.Parameters.AddWithValue("@InnerException", ex.InnerException?.Message ?? (object)DBNull.Value);

                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception logEx)
            {
                // Se falhar o log no banco, só arquivo
                LogarParaArquivo("DEBUG", "Falha ao logar no banco: " + logEx.Message);
            }
        }

        public static void LogarInfo(string mensagem)
        {
            LogarParaArquivo("INFO", mensagem);

            try
            {
                using (var conexao = new SqlConnection(GetConnectionString()))
                {
                    conexao.Open();

                    var comando = new SqlCommand(
                        "INSERT INTO SistemaLogs (Nivel, Mensagem, UsuarioId, Formulario, Metodo) " +
                        "VALUES (@Nivel, @Mensagem, @UsuarioId, @Formulario, @Metodo)",
                        conexao);

                    comando.Parameters.AddWithValue("@Nivel", "INFO");
                    comando.Parameters.AddWithValue("@Mensagem", mensagem);
                    comando.Parameters.AddWithValue("@UsuarioId", UsuarioId ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Formulario", FormularioAtual ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Metodo", MetodoAtual ?? (object)DBNull.Value);

                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception logEx)
            {
                // Se falhar, apenas log no arquivo
                LogarParaArquivo("DEBUG", "Falha ao logar INFO no banco: " + logEx.Message);
            }
        }

        // Método para avisos
        public static void LogarAviso(string mensagem, Exception ex = null)
        {
            LogarParaArquivo("WARN", mensagem, ex);

            try
            {
                using (var conexao = new SqlConnection(GetConnectionString()))
                {
                    conexao.Open();

                    var comando = new SqlCommand(
                        "INSERT INTO SistemaLogs (Nivel, Mensagem, UsuarioId, Formulario, Metodo, ExceptionType) " +
                        "VALUES (@Nivel, @Mensagem, @UsuarioId, @Formulario, @Metodo, @ExceptionType)",
                        conexao);

                    comando.Parameters.AddWithValue("@Nivel", "WARN");
                    comando.Parameters.AddWithValue("@Mensagem", mensagem);
                    comando.Parameters.AddWithValue("@UsuarioId", UsuarioId ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Formulario", FormularioAtual ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@Metodo", MetodoAtual ?? (object)DBNull.Value);
                    comando.Parameters.AddWithValue("@ExceptionType", ex?.GetType().Name ?? (object)DBNull.Value);

                    comando.ExecuteNonQuery();
                }
            }
            catch { /* Ignora erro no log de aviso */ }
        }

        // Método para operações com monitoramento de tempo
        public static T ExecutarComLog<T>(Func<T> operacao, string nomeOperacao)
        {
            var stopwatch = Stopwatch.StartNew();
            MetodoAtual = nomeOperacao;

            try
            {
                var resultado = operacao();
                LogarInfo($"{nomeOperacao} concluído em {stopwatch.ElapsedMilliseconds}ms");
                return resultado;
            }
            catch (Exception ex)
            {
                LogarErro($"Falha em {nomeOperacao} após {stopwatch.ElapsedMilliseconds}ms", ex);
                throw; // Repropaga mantendo stack trace
            }
        }

        // Método para operações void (sem retorno)
        public static void ExecutarComLog(Action operacao, string nomeOperacao)
        {
            var stopwatch = Stopwatch.StartNew();
            MetodoAtual = nomeOperacao;

            try
            {
                operacao();
                LogarInfo($"{nomeOperacao} concluído em {stopwatch.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                LogarErro($"Falha em {nomeOperacao} após {stopwatch.ElapsedMilliseconds}ms", ex);
                throw;
            }
        }
    }
}