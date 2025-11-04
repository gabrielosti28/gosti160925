using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;

namespace gosti2.Data
{
    /// <summary>
    /// Gerenciador de mensagens globais do feed
    /// </summary>
    public static class MensagemGlobalManager
    {
        /// <summary>
        /// Posta uma nova mensagem no feed global (apenas texto)
        /// </summary>
        public static bool PostarMensagem(string texto)
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
                    var mensagem = new MensagemGlobal
                    {
                        UsuarioId = AppManager.UsuarioLogado.UsuarioId,
                        TextoMensagem = texto.Trim(),
                        DataPostagem = DateTime.Now
                    };

                    db.MensagensGlobais.Add(mensagem);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao postar mensagem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Posta uma nova mensagem com imagem
        /// </summary>
        public static bool PostarMensagemComImagem(string texto, byte[] imagem)
        {
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(texto) && (imagem == null || imagem.Length == 0))
            {
                MessageBox.Show("A mensagem deve conter texto ou imagem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var mensagem = new MensagemGlobal
                    {
                        UsuarioId = AppManager.UsuarioLogado.UsuarioId,
                        TextoMensagem = texto?.Trim(),
                        ImagemMensagem = imagem,
                        DataPostagem = DateTime.Now
                    };

                    db.MensagensGlobais.Add(mensagem);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao postar mensagem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Carrega as mensagens do feed global (mais recentes primeiro)
        /// </summary>
        public static List<MensagemGlobal> CarregarMensagens(int quantidade = 50)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.MensagensGlobais
                        .Include(m => m.Usuario)
                        .OrderByDescending(m => m.DataPostagem)
                        .Take(quantidade)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar mensagens: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MensagemGlobal>();
            }
        }

        /// <summary>
        /// Remove uma mensagem (apenas o autor pode remover)
        /// </summary>
        public static bool RemoverMensagem(int mensagemId)
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
                    var mensagem = db.MensagensGlobais.Find(mensagemId);

                    if (mensagem == null)
                    {
                        MessageBox.Show("Mensagem não encontrada.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Verifica se é o autor
                    if (mensagem.UsuarioId != AppManager.UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você só pode remover suas próprias mensagens.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    db.MensagensGlobais.Remove(mensagem);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover mensagem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Edita uma mensagem existente
        /// </summary>
        public static bool EditarMensagem(int mensagemId, string novoTexto)
        {
            if (!AppManager.EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(novoTexto))
            {
                MessageBox.Show("Digite uma mensagem.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var mensagem = db.MensagensGlobais.Find(mensagemId);

                    if (mensagem == null)
                    {
                        MessageBox.Show("Mensagem não encontrada.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Verifica se é o autor
                    if (mensagem.UsuarioId != AppManager.UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você só pode editar suas próprias mensagens.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    mensagem.TextoMensagem = novoTexto.Trim();
                    mensagem.Editada = true;
                    mensagem.DataEdicao = DateTime.Now;

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar mensagem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Conta o total de mensagens no feed
        /// </summary>
        public static int ContarMensagens()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.MensagensGlobais.Count();
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}