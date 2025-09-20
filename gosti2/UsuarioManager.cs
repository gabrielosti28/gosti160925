using System;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;

namespace gosti2.Data
{
    public static class UsuarioManager
    {
        public static Usuario UsuarioLogado { get; private set; }

        public static bool CadastrarUsuario(Usuario novoUsuario)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Verifica se email já existe
                    if (context.Usuarios.Any(u => u.Email == novoUsuario.Email))
                    {
                        MessageBox.Show("Este e-mail já está cadastrado!", "Erro");
                        return false;
                    }

                    context.Usuarios.Add(novoUsuario);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no cadastro: {ex.Message}\nInner: {ex.InnerException?.Message}");
                return false;
            }
        }

        public static bool Login(string email, string senha)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuario = context.Usuarios
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

        public static bool VerificarEmailExistente(string email)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    return context.Usuarios.Any(u => u.Email == email);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar email: {ex.Message}");
                return false;
            }
        }
    }
}