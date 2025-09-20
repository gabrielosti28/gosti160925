using System;
using System.Windows.Forms;
using gosti2.Models;
using gosti2.Data;

namespace gosti2.Tools
{
    public static class ReferenceVerifier
    {
        public static void VerificarTodasReferencias()
        {
            try
            {
                // Teste se todas as classes básicas existem
                var usuario = new Usuario();
                var livro = new Livro();
                var comentario = new Comentario();

                // Teste se os managers existem
                var usuarioLogado = UsuarioManager.UsuarioLogado;

                // Teste se o contexto do banco existe
                using (var context = new ApplicationDbContext())
                {
                    // Teste básico de conexão
                    var podeConectar = context.Database.Exists();
                }

                MessageBox.Show("✅ Todas as referências estão OK!\n\n" +
                               "- Models: Usuario, Livro, Comentario ✓\n" +
                               "- Data: UsuarioManager, ApplicationDbContext ✓",
                               "Verificação de Referências",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Erro de referência encontrado:\n\n{ex.Message}\n\n" +
                               $"Tipo do erro: {ex.GetType().Name}\n" +
                               $"Inner Exception: {ex.InnerException?.Message}",
                               "Erro de Referência",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}