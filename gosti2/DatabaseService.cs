using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gosti2.Data
{
    public static class DatabaseService
    {
        // ✅ 1. CONNECTION STRING DIRETA - sem configurações complexas
        private static string ConnectionString =
            @"Server=.\SQLEXPRESS;Database=CJ3027333PR2;Trusted_Connection=true;TrustServerCertificate=true;";

        // ✅ 2. MÉTODO UNIVERSAL para queries
        public static SqlDataReader Query(string sql, params SqlParameter[] parameters)
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                var command = new SqlCommand(sql, connection);

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no banco: {ex.Message}", "Erro");
                return null;
            }
        }

        // ✅ 3. MÉTODO UNIVERSAL para comandos (INSERT, UPDATE, DELETE)
        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no banco: {ex.Message}", "Erro");
                return -1;
            }
        }

        // ✅ 4. LOGIN SIMPLES E FUNCIONAL
        public static bool Login(string email, string senha)
        {
            // ✅ CORREÇÃO CRÍTICA: Criptografa AMBAS as vezes
            string senhaCriptografada = CriptografarSenha(senha);

            string sql = "SELECT UsuarioId, NomeUsuario FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

            using (var reader = Query(sql,
                new SqlParameter("@Email", email),
                new SqlParameter("@Senha", senhaCriptografada))) // ← USA SENHA CRIPTOGRAFADA
            {
                if (reader != null && reader.Read())
                {
                    // Login bem-sucedido
                    return true;
                }
            }
            return false;
        }

        // ✅ 5. CADASTRO SIMPLES
        public static bool CadastrarUsuario(string nome, string email, string senha, DateTime dataNascimento)
        {
            string senhaCriptografada = CriptografarSenha(senha);

            string sql = @"INSERT INTO Usuarios 
                         (NomeUsuario, Email, Senha, DataNascimento, DataCadastro, Ativo) 
                         VALUES 
                         (@Nome, @Email, @Senha, @DataNasc, @DataCadastro, 1)";

            int result = Execute(sql,
                new SqlParameter("@Nome", nome),
                new SqlParameter("@Email", email),
                new SqlParameter("@Senha", senhaCriptografada),
                new SqlParameter("@DataNasc", dataNascimento),
                new SqlParameter("@DataCadastro", DateTime.Now));

            return result > 0;
        }

        // ✅ 6. TESTE DE CONEXÃO SIMPLES
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
                MessageBox.Show($"Não conectou ao banco: {ex.Message}", "Erro de Conexão");
                return false;
            }
        }

        // ✅ 7. CRIPTOGRAFIA (MESMA DO AppManager - para compatibilidade)
        private static string CriptografarSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(senha + "BookConnect2024");
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}