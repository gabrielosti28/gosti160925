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

            try
            {
                // VERIFICAR BANCO ANTES DE INICIAR
                if (!VerificarOuConfigurarBanco())
                {
                    return; // Sai do programa se não conseguir configurar
                }

                // Tela de menu primeiro
                using (var menu = new FormMenu())
                {
                    Application.Run(menu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao iniciar o sistema:\n\n{ex.Message}\n\nDetalhes: {ex.InnerException?.Message}",
                    "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool VerificarOuConfigurarBanco()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Tenta conectar e criar o banco se não existir
                    db.Database.CreateIfNotExists();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show(
                    $"❌ Não foi possível conectar ao banco de dados.\n\n" +
                    $"Erro: {ex.Message}\n\n" +
                    $"Deseja configurar a conexão agora?",
                    "Erro de Conexão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    using (var formConfig = new FormConfiguracaoBanco())
                    {
                        if (formConfig.ShowDialog() == DialogResult.OK)
                        {
                            // Tenta novamente após configuração
                            return VerificarOuConfigurarBanco();
                        }
                    }
                }

                return false;
            }
        }
    }
}