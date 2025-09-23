using System;
using System.Linq;
using gosti2.Data;

namespace gosti2.Tools  // ✅ CORRIGIDO: Namespace organizado
{
    /// <summary>
    /// Utilitários para operações comuns no banco de dados
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Verifica se o banco está acessível e com estrutura básica
        /// </summary>
        public static bool VerificarConexaoEstrutura()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // ✅ TESTE MAIS ESPECÍFICO E SEGURO
                    var tabelasExistem = context.Database.SqlQuery<int>(
                        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Usuarios', 'Livros')")
                        .FirstOrDefault() == 2;

                    if (!tabelasExistem)
                    {
                        Console.WriteLine("⚠️  Tabelas básicas não encontradas");
                        return false;
                    }

                    // ✅ OPERAÇÃO SIMPLES PARA TESTAR CONEXÃO
                    var existeUsuarios = context.Usuarios.Any();
                    Console.WriteLine("✅ Estrutura do banco verificada com sucesso");
                    return true;
                }
            }
            catch (Exception ex)
            {
                // ✅ LOG APPROPRIADO (SEM MessageBox)
                Console.WriteLine($"❌ Erro na verificação do banco: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtém estatísticas básicas do banco
        /// </summary>
        public static string ObterEstatisticasBanco()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var estatisticas = new System.Text.StringBuilder();
                    estatisticas.AppendLine("📊 ESTATÍSTICAS DO BANCO:");
                    estatisticas.AppendLine("========================");

                    var contadores = new[]
                    {
                        new { Tabela = "Usuarios", Count = context.Usuarios.Count() },
                        new { Tabela = "Livros", Count = context.Livros.Count() },
                        new { Tabela = "Comentarios", Count = context.Comentarios.Count() },
                        new { Tabela = "Avaliacoes", Count = context.Avaliacoes.Count() }
                    };

                    foreach (var contador in contadores)
                    {
                        estatisticas.AppendLine($"• {contador.Tabela}: {contador.Count} registros");
                    }

                    return estatisticas.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"❌ Erro ao obter estatísticas: {ex.Message}";
            }
        }

        /// <summary>
        /// Limpa dados de teste (apenas para desenvolvimento)
        /// </summary>
        public static void LimparDadosTeste()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // ✅ APENAS PARA DESENVOLVIMENTO
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        context.Database.ExecuteSqlCommand("DELETE FROM Avaliacoes");
                        context.Database.ExecuteSqlCommand("DELETE FROM Comentarios");
                        context.Database.ExecuteSqlCommand("DELETE FROM LikesDislikes");
                        Console.WriteLine("✅ Dados de teste limpos");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Erro ao limpar dados: {ex.Message}");
            }
        }

        /// <summary>
        /// Backup simples da estrutura (para debugging)
        /// </summary>
        public static string ExportarEstruturaSimplificada()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var estrutura = new System.Text.StringBuilder();
                    estrutura.AppendLine("// ESTRUTURA DO BANCO - " + DateTime.Now);

                    var tabelas = context.Database.SqlQuery<string>(
                        "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME");

                    foreach (var tabela in tabelas)
                    {
                        estrutura.AppendLine($"// {tabela}");
                    }

                    return estrutura.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"// Erro: {ex.Message}";
            }
        }
    }
}