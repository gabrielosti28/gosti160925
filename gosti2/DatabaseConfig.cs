using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gosti2.Data
{
    public static class DatabaseConfig
    {
        public static string ConnectionString
        {
            get
            {
                var config = ConfigurationManager.ConnectionStrings["DefaultConnection"];
                if (config != null && !string.IsNullOrEmpty(config.ConnectionString))
                    return config.ConnectionString;

                // Fallback seguro
                return @"Server=.\SQLEXPRESS;Database=CJ3O27333PR2;Trusted_Connection=true;TrustServerCertificate=true;";
            }
        }

        public static bool TestarConexao()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha na conexão: {ex.Message}", "Erro de Banco");
                return false;
            }
        }
    }
}