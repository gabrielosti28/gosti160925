using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gosti2.Data
{
    public static class DatabaseManager
    {
        public static bool TestarConexao()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return context.Database.Exists();
                }
            }
            catch
            {
                return false;
            }
        }

        public static void CriarBancoSeNecessario()
        {
            try
            {
                if (!TestarConexao())
                {
                    // Tenta criar o banco
                    var connectionString = "Server=(localdb)\\MSSQLLocalDB;Trusted_Connection=true;TrustServerCertificate=true;";

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        var command = new SqlCommand(
                            "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CJ3027333PR2') " +
                            "CREATE DATABASE [CJ3027333PR2]", connection);
                        command.ExecuteNonQuery();
                    }

                    // Cria as tabelas
                    using (var context = new ApplicationDbContext())
                    {
                        context.Database.CreateIfNotExists();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar banco: " + ex.Message, "Erro");
            }
        }
    }
}