using System;
using System.Linq;
using System.Windows.Forms;
using gosti2.Models;

namespace gosti2.Data
{
    public static class AppManager
    {
        public static Usuario UsuarioLogado { get; private set; }

        // === GESTÃO DE USUÁRIOS ===
        public static bool Login(string email, string senha)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // ✅ CORREÇÃO CRÍTICA: Criptografa a senha para comparar
                    string senhaCriptografada = CriptografarSenha(senha);

                    var usuario = db.Usuarios.FirstOrDefault(u =>
                        u.Email == email && u.Senha == senhaCriptografada); // ← USA SENHA CRIPTOGRAFADA

                    if (usuario != null)
                    {
                        UsuarioLogado = usuario;
                        usuario.UltimoLogin = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }

                    MessageBox.Show("Email ou senha incorretos", "Erro de Login");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro");
                return false;
            }
        }

        public static bool CadastrarUsuario(string nome, string email, string senha, DateTime dataNascimento)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Verificar se já existe
                    // Verificar se já existe
                    if (db.Usuarios.Any(u => u.Email == email))
                    {
                        MessageBox.Show("Email já cadastrado", "Aviso");
                        return false;
                    }

                    if (db.Usuarios.Any(u => u.NomeUsuario == nome))
                    {
                        MessageBox.Show("Nome de usuário já existe", "Aviso");
                        return false;
                    }

                    // Criptografar senha
                    string senhaCriptografada = CriptografarSenha(senha);

                    var usuario = new Usuario
                    {
                        NomeUsuario = nome,
                        Email = email,
                        Senha = senhaCriptografada,
                        DataNascimento = dataNascimento,
                        DataCadastro = DateTime.Now,
                        Ativo = true
                    };

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();

                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro no cadastro: {ex.Message}", "Erro");
                return false;
            }
        }



        public static void Logout()
        {
            UsuarioLogado = null;
        }

        // === GESTÃO DE LIVROS ===
        public static bool AdicionarLivro(string titulo, string autor, string genero, string descricao = null)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livro = new Livro
                    {
                        Titulo = titulo,
                        Autor = autor,
                        Genero = genero,
                        Descricao = descricao,
                        UsuarioId = UsuarioLogado.UsuarioId,
                        DataAdicao = DateTime.Now
                    };

                    db.Livros.Add(livro);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar livro: {ex.Message}", "Erro");
                return false;
            }
        }

        public static void CarregarLivrosUsuario(DataGridView grid)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livros = db.Livros
                        .Where(l => l.UsuarioId == UsuarioLogado.UsuarioId)
                        .OrderByDescending(l => l.DataAdicao)
                        .ToList();

                    grid.DataSource = livros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro");
            }
        }

        // === GESTÃO DE COMENTÁRIOS ===
        public static bool AdicionarComentario(int livroId, string texto)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var comentario = new Comentario
                    {
                        Texto = texto,
                        LivroId = livroId,
                        UsuarioId = UsuarioLogado.UsuarioId,
                        DataComentario = DateTime.Now
                    };

                    db.Comentarios.Add(comentario);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao comentar: {ex.Message}", "Erro");
                return false;
            }
        }

        // === VERIFICAÇÃO DE BANCO ===
        public static bool VerificarBanco()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Database.Connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problema no banco: {ex.Message}", "Erro de Banco");
                return false;
            }
        }
    

    private static string CriptografarSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(senha + "BookConnect2024");
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool RemoverLivro(int livroId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livro = db.Livros.Find(livroId);
                    if (livro != null && livro.UsuarioId == UsuarioLogado.UsuarioId)
                    {
                        db.Livros.Remove(livro);
                        db.SaveChanges();
                        MessageBox.Show("Livro removido com sucesso!", "Sucesso");
                        return true;
                    }
                    MessageBox.Show("Livro não encontrado ou sem permissão", "Erro");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover livro: {ex.Message}", "Erro");
                return false;
            }
        }

        public static bool AtualizarUsuario(string nome, string bio, string website, string localizacao)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var usuario = db.Usuarios.Find(UsuarioLogado.UsuarioId);
                    if (usuario != null)
                    {
                        usuario.NomeUsuario = nome;
                        usuario.Bio = bio;
                        usuario.Website = website;
                        usuario.Localizacao = localizacao;

                        db.SaveChanges();
                        UsuarioLogado = usuario; // Atualiza usuário logado
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar perfil: {ex.Message}", "Erro");
                return false;
            }
        }


    }
}