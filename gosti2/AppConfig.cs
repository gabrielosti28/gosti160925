using System.Configuration;

namespace gosti2
{
    public static class AppConfig
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString
            ?? @"Server=.\SQLEXPRESS;Database=CJ3O27333PR2;Trusted_Connection=true;TrustServerCertificate=true;";

        public static void SalvarConfiguracaoBanco(string servidor, string banco, bool windowsAuth, string usuario = null, string senha = null)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string connectionString = windowsAuth
                ? $"Server={servidor};Database={banco};Trusted_Connection=true;TrustServerCertificate=true;"
                : $"Server={servidor};Database={banco};User Id={usuario};Password={senha};TrustServerCertificate=true;";

            if (config.ConnectionStrings.ConnectionStrings["DefaultConnection"] != null)
            {
                config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ConnectionString = connectionString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(
                    new ConnectionStringSettings("DefaultConnection", connectionString));
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}