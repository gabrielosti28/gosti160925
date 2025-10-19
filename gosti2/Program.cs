using System;
using System.Windows.Forms;
using gosti2.Data;

namespace gosti2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicialização direta
            try
            {
                // Tela de login primeiro
                using (var login = new FormMenu())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new FormPrincipal());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar: {ex.Message}", "Erro");
            }
        }
    }
}