using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;

namespace gosti2.Tools
{
    public static class ReferenceVerifier
    {
        public static void VerificarTodasReferencias()
        {
            var resultado = new StringBuilder();
            resultado.AppendLine("🔍 VERIFICAÇÃO DE REFERÊNCIAS");
            resultado.AppendLine("==============================");

            try
            {
                // ✅ 1. VERIFICAR ASSEMBLY PRINCIPAL
                VerificarAssemblyPrincipal(resultado);

                // ✅ 2. VERIFICAR CLASSES DE MODEL (CRÍTICAS)
                VerificarClassesModel(resultado);

                // ✅ 3. VERIFICAR CLASSES DE DATA (IMPORTANTES)
                VerificarClassesData(resultado);

                // ✅ 4. VERIFICAR CLASSES DE TOOLS (OPCIONAIS)
                VerificarClassesTools(resultado);

                // ✅ 5. VERIFICAÇÃO DE CONEXÃO (SEGURA)
                VerificarConexaoBanco(resultado);

                MessageBox.Show(resultado.ToString(),
                    "Verificação de Referências - CONCLUÍDA",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ LOG NO CONSOLE PARA DEBUG
                Console.WriteLine(resultado.ToString());
            }
            catch (Exception ex)
            {
                var mensagemErro = $"❌ ERRO CRÍTICO NA VERIFICAÇÃO:\n\n{ex.Message}";
                MessageBox.Show(mensagemErro,
                    "Erro na Verificação",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(mensagemErro);
            }
        }

        private static void VerificarAssemblyPrincipal(StringBuilder resultado)
        {
            resultado.AppendLine("\n📦 ASSEMBLY PRINCIPAL:");

            var assembly = Assembly.GetExecutingAssembly();
            resultado.AppendLine($"   • Nome: {assembly.GetName().Name}");
            resultado.AppendLine($"   • Versão: {assembly.GetName().Version}");
            resultado.AppendLine($"   • Local: {assembly.Location}");
            resultado.AppendLine($"   • Referências: {assembly.GetReferencedAssemblies().Length}");
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
        }

        private static void VerificarClassesTools(StringBuilder resultado)
        {
            resultado.AppendLine("\n🛠️ CLASSES DE TOOLS (OPCIONAIS):");

            // ✅ VERIFICAR CLASSES DE TOOLS (SE EXISTIREM)
            VerificarClassePorNome("gosti2.Tools.DatabaseExporter", "DatabaseExporter", resultado);
            VerificarClassePorNome("gosti2.Tools.DatabaseSchemaValidator", "DatabaseSchemaValidator", resultado);
            VerificarClassePorNome("gosti2.Tools.ReferenceVerifier", "ReferenceVerifier", resultado);
        }

        private static void VerificarConexaoBanco(StringBuilder resultado)
        {
            resultado.AppendLine("\n🗄️ VERIFICAÇÃO DE BANCO:");

            try
            {
                // ✅ TESTE SEGURO DE CONEXÃO
                using (var context = new ApplicationDbContext())
                {
                    var existe = context.Database.Exists();
                    resultado.AppendLine(existe ?
                        "   ✅ Banco de dados ACESSÍVEL" :
                        "   ⚠️ Banco de dados NÃO ACESSÍVEL");

                    if (existe)
                    {
                        // ✅ TENTAR VER TABELAS (SE POSSÍVEL)
                        try
                        {
                            var tabelas = context.Database.SqlQuery<string>(
                                "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'").ToList();

                            resultado.AppendLine($"   • Tabelas encontradas: {tabelas.Count}");
                            foreach (var tabela in tabelas.Take(5)) // Mostrar apenas 5
                            {
                                resultado.AppendLine($"     - {tabela}");
                            }
                            if (tabelas.Count > 5)
                                resultado.AppendLine($"     ... e mais {tabelas.Count - 5} tabelas");
                        }
                        catch
                        {
                            resultado.AppendLine("   ℹ️ Não foi possível listar tabelas");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ Erro na conexão: {ex.GetType().Name}");
            }
        }

        private static void VerificarClasse(Type tipo, string nomeAmigavel, StringBuilder resultado)
        {
            try
            {
                // ✅ TENTAR CRIAR INSTÂNCIA (SE POSSÍVEL)
                if (tipo.IsClass && !tipo.IsAbstract)
                {
                    var instancia = Activator.CreateInstance(tipo);
                    resultado.AppendLine($"   ✅ {nomeAmigavel} - OK");
                }
                else
                {
                    resultado.AppendLine($"   ✅ {nomeAmigavel} - Tipo válido");
                }
            }
            catch (Exception ex)
            {
                resultado.AppendLine($"   ❌ {nomeAmigavel} - Erro: {ex.Message}");
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
                    resultado.AppendLine($"   ⚠️ {nomeAmigavel} - Não encontrada (opcional)");
                }
            }
            catch
            {
                resultado.AppendLine($"   ⚠️ {nomeAmigavel} - Não disponível");
            }
        }

        // ✅ MÉTODO SIMPLIFICADO PARA USO RÁPIDO
        public static bool VerificacaoRapida()
        {
            try
            {
                // ✅ VERIFICAÇÃO MÍNIMA DAS CLASSES CRÍTICAS
                var criticalTypes = new[]
                {
                    typeof(Usuario),
                    typeof(Livro),
                    typeof(ApplicationDbContext)
                };

                foreach (var type in criticalTypes)
                {
                    if (type == null) return false;
                }

                // ✅ VERIFICAÇÃO BÁSICA DE BANCO
                using (var context = new ApplicationDbContext())
                {
                    return context.Database.Exists();
                }
            }
            catch
            {
                return false;
            }
        }
    }
}