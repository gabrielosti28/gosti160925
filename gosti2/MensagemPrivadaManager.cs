using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;

namespace gosti2.Data
{
    /// <summary>
    /// Gerenciador de mensagens privadas entre usuários
    /// </summary>
    public static class MensagemPrivadaManager
    {
        /// <summary>
        /// Envia uma mensagem privada para outro usuário
        /// </summary>
        public static bool EnviarMensagem(int destinatarioId, string texto)
        {
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("Digite uma mensagem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (texto.Length > 2000)
            {
                MessageBox.Show("Mensagem muito longa (máximo 2000 caracteres).", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var mensagem = new Mensagem
                    {
                        RemetenteId = AppManager.UsuarioLogado.UsuarioId,
                        DestinatarioId = destinatarioId,
                        Texto = texto.Trim(),
                        DataEnvio = DateTime.Now,
                        Lida = false
                    };

                    db.Mensagens.Add(mensagem);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar mensagem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Carrega conversas do usuário logado (últimas mensagens de cada conversa)
        /// </summary>
        public static List<dynamic> CarregarConversas()
        {
            if (!AppManager.EstaLogado)
                return new List<dynamic>();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    int userId = AppManager.UsuarioLogado.UsuarioId;

                    // Busca todas as mensagens do usuário (enviadas e recebidas)
                    var mensagens = db.Mensagens
                        .Include(m => m.Remetente)
                        .Include(m => m.Destinatario)
                        .Where(m => m.RemetenteId == userId || m.DestinatarioId == userId)
                        .OrderByDescending(m => m.DataEnvio)
                        .ToList();

                    // Agrupa por contato (pega o outro usuário da conversa)
                    var conversas = mensagens
                        .GroupBy(m => m.RemetenteId == userId ? m.DestinatarioId : m.RemetenteId)
                        .Select(g => new
                        {
                            ContatoId = g.Key,
                            UltimaMensagem = g.First(),
                            NaoLidas = g.Count(m => m.DestinatarioId == userId && !m.Lida)
                        })
                        .ToList<dynamic>();

                    return conversas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar conversas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<dynamic>();
            }
        }

        /// <summary>
        /// Carrega histórico de mensagens entre dois usuários
        /// </summary>
        public static List<Mensagem> CarregarHistorico(int outroUsuarioId)
        {
            if (!AppManager.EstaLogado)
                return new List<Mensagem>();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    int userId = AppManager.UsuarioLogado.UsuarioId;

                    var mensagens = db.Mensagens
                        .Include(m => m.Remetente)
                        .Include(m => m.Destinatario)
                        .Where(m =>
                            (m.RemetenteId == userId && m.DestinatarioId == outroUsuarioId) ||
                            (m.RemetenteId == outroUsuarioId && m.DestinatarioId == userId))
                        .OrderBy(m => m.DataEnvio)
                        .ToList();

                    return mensagens;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar histórico: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Mensagem>();
            }
        }

        /// <summary>
        /// Marca mensagens como lidas
        /// </summary>
        public static void MarcarComoLidas(int remetenteId)
        {
            if (!AppManager.EstaLogado)
                return;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    int userId = AppManager.UsuarioLogado.UsuarioId;

                    var mensagensNaoLidas = db.Mensagens
                        .Where(m => m.RemetenteId == remetenteId &&
                                    m.DestinatarioId == userId &&
                                    !m.Lida)
                        .ToList();

                    foreach (var msg in mensagensNaoLidas)
                    {
                        msg.Lida = true;
                    }

                    db.SaveChanges();
                }
            }
            catch { }
        }

        /// <summary>
        /// Conta mensagens não lidas
        /// </summary>
        public static int ContarNaoLidas()
        {
            if (!AppManager.EstaLogado)
                return 0;

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Mensagens
                        .Count(m => m.DestinatarioId == AppManager.UsuarioLogado.UsuarioId && !m.Lida);
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}