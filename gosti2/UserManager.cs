using System;
using System.Linq;
using System.Windows.Forms;

namespace gosti2
{
    public static class UserManager
    {
        public static User UsuarioLogado { get; private set; }

        public static bool CadastrarUsuario(User novoUsuario)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    if (context.Users.Any(u => u.Email == novoUsuario.Email))
                    {
                        MessageBox.Show("Este e-mail já está cadastrado!", "Erro");
                        return false;
                    }

                    // Criptografar senha (implementar depois)
                    context.Users.Add(novoUsuario);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no cadastro: {ex.Message}");
                return false;
            }
        }

        public static bool Login(string email, string senha)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuario = context.Users
                        .FirstOrDefault(u => u.Email == email && u.Senha == senha);

                    if (usuario != null)
                    {
                        UsuarioLogado = usuario;
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no login: {ex.Message}");
                return false;
            }
        }

        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}