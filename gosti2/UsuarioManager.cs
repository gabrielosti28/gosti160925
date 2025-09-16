using System;
using System.Linq;
using System.Windows.Forms;

namespace gosti2
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
                    // DEBUG: Verifique se a tabela existe
                    try
                    {
                        var teste = context.Usuarios.FirstOrDefault();
                        MessageBox.Show("✅ Tabela Usuarios encontrada!");
                    }
                    catch
                    {
                        MessageBox.Show("❌ Tabela Usuarios não encontrada!");
                    }

                    if (context.Usuarios.Any(u => u.Email == novoUsuario.Email))
                    {
                        MessageBox.Show("Este e-mail já está cadastrado!", "Erro");
                        return false;
                    }

                    context.Usuarios.Add(novoUsuario);
                    context.SaveChanges(); // ← O erro ocorre aqui

                    MessageBox.Show("✅ Usuário cadastrado com sucesso!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                // MOSTRE O ERRO COMPLETO
                string erroCompleto = $"Erro: {ex.Message}\n";
                if (ex.InnerException != null)
                    erroCompleto += $"Inner: {ex.InnerException.Message}\n";
                if (ex.InnerException?.InnerException != null)
                    erroCompleto += $"Inner2: {ex.InnerException.InnerException.Message}";

                MessageBox.Show(erroCompleto, "Erro Detalhado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //  debug:
        public static bool Login(string email, string senha)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Debug: veja se há usuários na tabela
                    var totalUsuarios = context.Usuarios.Count();
                    MessageBox.Show($"Total de usuários no banco: {totalUsuarios}");

                    // Debug: veja todos os emails
                    var emails = context.Usuarios.Select(u => u.Email).ToList();
                    MessageBox.Show($"Emails cadastrados: {string.Join(", ", emails)}");

                    var usuario = context.Usuarios
                        .AsNoTracking() // ← ADICIONE ESTA LINHA
                        .FirstOrDefault(u => u.Email == email && u.Senha == senha);

                    if (usuario != null)
                    {
                        UsuarioLogado = usuario;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Usuário não encontrado. Email: {email}, Senha: {senha}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                string erroDetalhado = $"Erro completo: {ex.Message}\n";
                if (ex.InnerException != null)
                    erroDetalhado += $"Inner Exception: {ex.InnerException.Message}\n";

                MessageBox.Show(erroDetalhado, "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}