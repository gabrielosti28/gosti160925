using System;
using System.Windows.Forms;
using gosti;
// connectionString="Data Source=sqlexpress;Initial Catalog=CJ3027333PR2;Integrated Security=True;MultipleActiveResultSets=True"


namespace gosti2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Verifica a estrutura do banco antes de tudo
            DatabaseHelper.VerificarEstruturaBanco();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1️⃣ CONFIGURAÇÃO DO BANCO (se necessário)
            if (!DatabaseTester.TestarConexao())
            {
                // Se não conseguiu conectar, abre tela de configuração
                using (var formConfig = new FormConfiguracaoBanco())
                {
                    if (formConfig.ShowDialog() != DialogResult.OK)
                    {
                        // Usuário cancelou a configuração
                        Application.Exit();
                        return;
                    }
                }
            }

            // 2️⃣ TELA DE BOAS-VINDAS (FormMain)
            using (var formMain = new FormMain())
            {
                if (formMain.ShowDialog() == DialogResult.OK)
                {
                    // 3️⃣ LOOP PRINCIPAL DA APLICAÇÃO
                    ExecutarAplicacao();
                }
            }
        }

        private static void ExecutarAplicacao()
        {
            while (true)
            {
                // 4️⃣ MENU PRINCIPAL
                using (var formMenu = new FormMenu())
                {
                    var resultadoMenu = formMenu.ShowDialog();

                    if (resultadoMenu == DialogResult.OK)
                    {
                        // 5️⃣ TELA PRINCIPAL (após login bem-sucedido)
                        using (var formPrincipal = new FormPrincipal())
                        {
                            Application.Run(formPrincipal);
                            UsuarioManager.Logout();
                        }
                    }
                    else if (resultadoMenu == DialogResult.Cancel)
                    {
                        // Usuário quer sair
                        break;
                    }
                }
            }
        }
    }
}