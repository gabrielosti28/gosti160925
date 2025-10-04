using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace gosti2.Tools
{
    public static class ReferenceVerifier
    {
        public static void VerificarTodasReferencias()
        {
            var resultado = new StringBuilder();
            resultado.AppendLine("🔍 VERIFICAÇÃO DE REFERÊNCIAS E SISTEMA");
            resultado.AppendLine("==========================================");

            try
            {
                // ✅ REGISTRAR INÍCIO DA VERIFICAÇÃO
                DiagnosticContext.LogarInfo("Iniciando verificação de referências e sistema");

                // ✅ 1. VERIFICAR ASSEMBLY PRINCIPAL
                VerificarAssemblyPrincipal(resultado);

                // ✅ 2. VERIFICAR CLASSES DE MODEL (CRÍTICAS)
                VerificarClassesModel(resultado);

                // ✅ 3. VERIFICAR CLASSES DE DATA (IMPORTANTES)
                VerificarClassesData(resultado);

                // ✅ 4. VERIFICAR CLASSES DE TOOLS (OPCIONAIS)
                VerificarClassesTools(resultado);

                // ✅ 5. VERIFICAÇÃO DE CONEXÃO (SEGURA - ADO.NET)
                VerificarConexaoBanco(resultado);

                // ✅ 6. VERIFICAR SISTEMA DE DIAGNÓSTICO
                VerificarSistemaDiagnostico(resultado);

                var resultadoFinal = resultado.ToString();

                // ✅ LOG DO RESULTADO NO SISTEMA DE DIAGNÓSTICO
                DiagnosticContext.LogarInfo($"Verificação concluída:\n{resultadoFinal}");

                MessageBox.Show(resultadoFinal,
                    "Verificação de Sistema - CONCLUÍDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                var mensagemErro = $"❌ ERRO CRÍTICO NA VERIFICAÇÃO:\n\n{ex.Message}";

                // ✅ REGISTRAR ERRO NO SISTEMA DE DIAGNÓSTICO
                DiagnosticContext.LogarErro("Falha na verificação de referências", ex);

                MessageBox.Show(mensagemErro,
                    "Erro na Verificação",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void VerificarAssemblyPrincipal(StringBuilder resultado)
        {
            resultado.AppendLine("\n📦 ASSEMBLY PRINCIPAL:");

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                resultado.AppendLine($"   • Nome: {assembly.GetName().Name}");
                resultado.AppendLine($"   • Versão: {assembly.GetName().Version}");
                resultado.AppendLine($"   • Local: {assembly.Location}");
                resultado.AppendLine($"   • Referências: {assembly.GetReferencedAssemblies().Length}");

                DiagnosticContext.LogarInfo("Assembly principal verificado com sucesso");
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ Erro ao verificar assembly: {ex.Message}");
                DiagnosticContext.LogarErro("Erro ao verificar assembly principal", ex);
            }
        }

        private static void VerificarClassesModel(StringBuilder resultado)
        {
            resultado.AppendLine("\n👤 CLASSES DE MODEL (CRÍTICAS):");

            // ✅ CLASSES QUE DEFINITIVAMENTE EXISTEM
            VerificarClasse(typeof(Usuario), "Usuario", resultado);
            VerificarClasse(typeof(Livro), "Livro", resultado);
            VerificarClasse(typeof(Comentario), "Comentario", resultado);
            VerificarClasse(typeof(Avaliacao), "Avaliacao", resultado);
            VerificarClasse(typeof(LikeDislike), "LikeDislike", resultado);
            VerificarClasse(typeof(Mensagem), "Mensagem", resultado);
            VerificarClasse(typeof(CategoriaTier), "CategoriaTier", resultado);
        }

        private static void VerificarClassesData(StringBuilder resultado)
        {
            resultado.AppendLine("\n🗃️ CLASSES DE DATA (IMPORTANTES):");

            // ✅ APPLICATIONDBCONTEXT (CRÍTICO)
            VerificarClasse(typeof(ApplicationDbContext), "ApplicationDbContext", resultado);

            // ✅ MANAGERS (VERIFICAR SE EXISTEM)
            VerificarClassePorNome("gosti2.Data.UsuarioManager", "UsuarioManager", resultado);
            VerificarClassePorNome("gosti2.Data.DatabaseManager", "DatabaseManager", resultado);
            VerificarClassePorNome("gosti2.Data.DatabaseEvolutionManager", "DatabaseEvolutionManager", resultado);
            VerificarClassePorNome("gosti2.Data.DatabaseInitializer", "DatabaseInitializer", resultado);
        }

        private static void VerificarClassesTools(StringBuilder resultado)
        {
            resultado.AppendLine("\n🛠️ CLASSES DE TOOLS (OPCIONAIS):");

            // ✅ VERIFICAR CLASSES DE TOOLS (SE EXISTIREM)
            VerificarClassePorNome("gosti2.Tools.DatabaseExporter", "DatabaseExporter", resultado);
            VerificarClassePorNome("gosti2.Tools.DatabaseSchemaValidator", "DatabaseSchemaValidator", resultado);
            VerificarClassePorNome("gosti2.Tools.ReferenceVerifier", "ReferenceVerifier", resultado);

            // ✅ VERIFICAR DIAGNOSTIC CONTEXT (NOVO)
            VerificarClasse(typeof(DiagnosticContext), "DiagnosticContext", resultado);
        }

        private static void VerificarConexaoBanco(StringBuilder resultado)
        {
            resultado.AppendLine("\n🗄️ VERIFICAÇÃO DE BANCO (ADO.NET):");

            try
            {
                // ✅ TESTE SEGURO DE CONEXÃO USANDO ADO.NET DIRETO
                string connectionString = GetConnectionString();

                using (var conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    resultado.AppendLine("   ✅ Conexão com banco estabelecida");

                    // ✅ VERIFICAR TABELAS PRINCIPAIS
                    VerificarTabelasBanco(conexao, resultado);

                    conexao.Close();
                }

                DiagnosticContext.LogarInfo("Conexão com banco verificada com sucesso");
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ Erro na conexão: {ex.GetType().Name} - {ex.Message}");
                DiagnosticContext.LogarErro("Falha na verificação de conexão com banco", ex);
            }
        }

        private static void VerificarSistemaDiagnostico(StringBuilder resultado)
        {
            resultado.AppendLine("\n🔧 SISTEMA DE DIAGNÓSTICO:");

            try
            {
                // ✅ VERIFICAR SE A TABELA DE LOGS EXISTE
                string connectionString = GetConnectionString();
                using (var conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    var comando = new SqlCommand(
                        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SistemaLogs'",
                        conexao);

                    var tabelaExiste = (int)comando.ExecuteScalar() > 0;

                    if (tabelaExiste)
                    {
                        resultado.AppendLine("   ✅ Tabela SistemaLogs - OK");

                        // ✅ VERIFICAR SE PODE INSERIR LOG
                        try
                        {
                            var comandoInsert = new SqlCommand(
                                "INSERT INTO SistemaLogs (Nivel, Mensagem, Formulario, Metodo) VALUES ('INFO', @Mensagem, 'ReferenceVerifier', 'VerificarSistemaDiagnostico')",
                                conexao);
                            comandoInsert.Parameters.AddWithValue("@Mensagem", "Teste de verificação do sistema de diagnósticos");
                            comandoInsert.ExecuteNonQuery();
                            resultado.AppendLine("   ✅ Sistema de logs operacional");
                        }
                        catch (Exception ex)
                        {
                            resultado.AppendLine($"   ⚠️ Tabela existe mas erro ao inserir: {ex.Message}");
                        }
                    }
                    else
                    {
                        resultado.AppendLine("   ❌ Tabela SistemaLogs não encontrada");
                    }

                    conexao.Close();
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ Erro ao verificar sistema de diagnóstico: {ex.Message}");
            }
        }

        private static void VerificarTabelasBanco(SqlConnection conexao, StringBuilder resultado)
        {
            try
            {
                var comando = new SqlCommand(
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME",
                    conexao);

                using (var reader = comando.ExecuteReader())
                {
                    var tabelas = new System.Collections.Generic.List<string>();
                    while (reader.Read())
                    {
                        tabelas.Add(reader.GetString(0));
                    }

                    resultado.AppendLine($"   • Tabelas encontradas: {tabelas.Count}");

                    // ✅ VERIFICAR TABELAS ESSENCIAIS
                    var tabelasEssenciais = new[] { "Usuarios", "Livros", "Comentarios", "Avaliacoes", "SistemaLogs" };
                    foreach (var tabela in tabelasEssenciais)
                    {
                        if (tabelas.Contains(tabela))
                            resultado.AppendLine($"     ✅ {tabela}");
                        else
                            resultado.AppendLine($"     ❌ {tabela} - AUSENTE");
                    }

                    // Mostrar outras tabelas (máximo 10)
                    var outrasTabelas = tabelas.Where(t => !tabelasEssenciais.Contains(t)).Take(10);
                    foreach (var tabela in outrasTabelas)
                    {
                        resultado.AppendLine($"     📋 {tabela}");
                    }

                    if (tabelas.Count > tabelasEssenciais.Length + 10)
                        resultado.AppendLine($"     ... e mais {tabelas.Count - tabelasEssenciais.Length - 10} tabelas");
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ⚠️ Erro ao listar tabelas: {ex.Message}");
                DiagnosticContext.LogarErro("Erro ao verificar tabelas do banco", ex);
            }
        }

        private static string GetConnectionString()
        {
            // ✅ MESMA LÓGICA DO DIAGNOSTICCONTEXT
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=localhost;Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;";
            }
            return connectionString;
        }

        private static void VerificarClasse(Type tipo, string nomeAmigavel, StringBuilder resultado)
        {
            try
            {
                // ✅ VERIFICAÇÃO SIMPLES - APENAS SE O TIPO EXISTE
                if (tipo != null)
                {
                    resultado.AppendLine($"   ✅ {nomeAmigavel} - OK");
                }
                else
                {
                    resultado.AppendLine($"   ❌ {nomeAmigavel} - Tipo nulo");
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ {nomeAmigavel} - Erro: {ex.Message}");
                DiagnosticContext.LogarErro($"Erro ao verificar classe {nomeAmigavel}", ex);
            }
        }

        private static void VerificarClassePorNome(string nomeCompleto, string nomeAmigavel, StringBuilder resultado)
        {
            try
            {
                var tipo = Type.GetType(nomeCompleto);
                if (tipo != null)
                {
                    resultado.AppendLine($"   ✅ {nomeAmigavel} - OK");
                }
                else
                {
                    resultado.AppendLine($"   ⚠️ {nomeAmigavel} - Não encontrada");
                    DiagnosticContext.LogarAviso($"Classe {nomeAmigavel} não encontrada");
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ {nomeAmigavel} - Erro: {ex.Message}");
                DiagnosticContext.LogarErro($"Erro ao verificar classe por nome: {nomeAmigavel}", ex);
            }
        }

        // ✅ MÉTODO SIMPLIFICADO PARA USO RÁPIDO
        public static bool VerificacaoRapida()
        {
            try
            {
                DiagnosticContext.LogarInfo("Executando verificação rápida do sistema");

                // ✅ VERIFICAÇÃO MÍNIMA DAS CLASSES CRÍTICAS
                var criticalTypes = new[]
                {
                    typeof(Usuario),
                    typeof(Livro),
                    typeof(ApplicationDbContext),
                    typeof(DiagnosticContext) // ✅ AGORA INCLUI DIAGNOSTICCONTEXT
                };

                foreach (var type in criticalTypes)
                {
                    if (type == null)
                    {
                        DiagnosticContext.LogarErro("Tipo crítico nulo na verificação rápida",
                            new Exception($"Tipo {type} é nulo"));
                        return false;
                    }
                }

                // ✅ VERIFICAÇÃO BÁSICA DE BANCO (ADO.NET)
                string connectionString = GetConnectionString();
                using (var conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    var comando = new SqlCommand("SELECT 1", conexao);
                    var resultado = comando.ExecuteScalar();
                    conexao.Close();

                    bool sucesso = resultado != null && resultado.ToString() == "1";

                    if (sucesso)
                        DiagnosticContext.LogarInfo("Verificação rápida concluída com sucesso");
                    else
                        DiagnosticContext.LogarErro("Verificação rápida falhou - teste de banco retornou resultado inesperado",
                            new Exception($"Resultado do teste: {resultado}"));

                    return sucesso;
                }
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Falha na verificação rápida do sistema", ex);
                return false;
            }
        }
    }
}