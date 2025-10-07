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

            // Configuração simples do banco
            try
            {
                DatabaseManager.CriarBancoSeNecessario();
                Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar: " + ex.Message, "Erro");
            }
        }
    }
}