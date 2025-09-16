using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gosti2
{
    public static class DatabaseHelper
    {
        public static void VerificarEstruturaBanco()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Tenta uma operação simples para verificar se a estrutura está OK
                    var count = context.Livros.Count();
                    Console.WriteLine("Estrutura do banco verificada com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problema na estrutura do banco: {ex.Message}\n\nExecute o script SQL de correção.",
                    "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}