using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;

namespace gosti2.Data
{
    /// <summary>
    /// Gerenciador do Jogo da Memória de Livros
    /// </summary>
    public static class JogoMemoriaManager
    {
        /// <summary>
        /// Salva pontuação do jogador
        /// </summary>
        public static bool SalvarPontuacao(int pontos, int movimentos, int tempoSegundos, string dificuldade)
        {
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var pontuacao = new PontuacaoJogo
                    {
                        UsuarioId = AppManager.UsuarioLogado.UsuarioId,
                        Pontos = pontos,
                        Movimentos = movimentos,
                        TempoSegundos = tempoSegundos,
                        Dificuldade = dificuldade,
                        DataJogo = DateTime.Now
                    };

                    db.Set<PontuacaoJogo>().Add(pontuacao);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar pontuação: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Obtém ranking geral (top 50)
        /// </summary>
        public static List<dynamic> ObterRankingGeral()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var ranking = db.Set<PontuacaoJogo>()
                        .Include(p => p.Usuario)
                        .OrderByDescending(p => p.Pontos)
                        .ThenBy(p => p.Movimentos)
                        .ThenBy(p => p.TempoSegundos)
                        .Take(50)
                        .Select(p => new
                        {
                            p.Usuario.NomeUsuario,
                            p.Usuario.FotoPerfil,
                            p.Pontos,
                            p.Movimentos,
                            p.TempoSegundos,
                            p.Dificuldade,
                            p.DataJogo
                        })
                        .ToList<dynamic>();

                    return ranking;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar ranking: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<dynamic>();
            }
        }

        /// <summary>
        /// Obtém melhor pontuação do usuário logado
        /// </summary>
        public static PontuacaoJogo ObterMelhorPontuacaoUsuario()
        {
            if (!AppManager.EstaLogado)
                return null;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Set<PontuacaoJogo>()
                        .Where(p => p.UsuarioId == AppManager.UsuarioLogado.UsuarioId)
                        .OrderByDescending(p => p.Pontos)
                        .ThenBy(p => p.Movimentos)
                        .ThenBy(p => p.TempoSegundos)
                        .FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Obtém livros aleatórios para o jogo
        /// </summary>
        public static List<Livro> ObterLivrosParaJogo(int quantidade)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Busca livros COM CAPA
                    var livros = db.Livros
                        .Where(l => l.Capa != null && l.Capa.Length > 0)
                        .OrderBy(l => Guid.NewGuid()) // Randomiza
                        .Take(quantidade)
                        .ToList();

                    if (livros.Count < quantidade)
                    {
                        MessageBox.Show($"São necessários pelo menos {quantidade} livros com capa cadastrados no sistema.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return new List<Livro>();
                    }

                    return livros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Livro>();
            }
        }

        /// <summary>
        /// Calcula pontuação baseada em desempenho
        /// </summary>
        public static int CalcularPontuacao(int pares, int movimentos, int tempoSegundos)
        {
            int pontuacaoBase = pares * 100;

            // Bônus por eficiência (menos movimentos)
            int movimentosIdeais = pares * 2;
            int bonusMovimentos = Math.Max(0, (movimentosIdeais * 2 - movimentos) * 10);

            // Bônus por tempo (mais rápido)
            int bonusTempo = Math.Max(0, (300 - tempoSegundos) * 2); // 5 minutos referência

            int pontuacaoTotal = pontuacaoBase + bonusMovimentos + bonusTempo;

            return Math.Max(0, pontuacaoTotal);
        }

        /// <summary>
        /// Obtém posição do usuário no ranking
        /// </summary>
        public static int ObterPosicaoRanking()
        {
            if (!AppManager.EstaLogado)
                return 0;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var melhorPontuacao = ObterMelhorPontuacaoUsuario();
                    if (melhorPontuacao == null)
                        return 0;

                    var posicao = db.Set<PontuacaoJogo>()
                        .Where(p => p.Pontos > melhorPontuacao.Pontos ||
                                   (p.Pontos == melhorPontuacao.Pontos && p.Movimentos < melhorPontuacao.Movimentos) ||
                                   (p.Pontos == melhorPontuacao.Pontos && p.Movimentos == melhorPontuacao.Movimentos && p.TempoSegundos < melhorPontuacao.TempoSegundos))
                        .Count() + 1;

                    return posicao;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}