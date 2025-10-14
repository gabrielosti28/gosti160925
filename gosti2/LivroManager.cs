//using System;
//using System.Linq;
//using System.Windows.Forms;
//using gosti2.Models;

//namespace gosti2.Data
//{
//    public static class LivroManager
//    {
//        public static bool AdicionarLivro(Livro livro)
//        {
//            try
//            {
//                using (var context = new ApplicationDbContext())
//                {
//                    livro.DataAdicao = DateTime.Now;
//                    context.Livros.Add(livro);
//                    context.SaveChanges();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Erro ao adicionar livro: {ex.Message}", "Erro");
//                return false;
//            }
//        }

//        public static bool RemoverLivro(int livroId)
//        {
//            try
//            {
//                using (var context = new ApplicationDbContext())
//                {
//                    var livro = context.Livros.Find(livroId);
//                    if (livro != null)
//                    {
//                        context.Livros.Remove(livro);
//                        context.SaveChanges();
//                        return true;
//                    }
//                    return false;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Erro ao remover livro: {ex.Message}", "Erro");
//                return false;
//            }
//        }

//        public static IQueryable<Livro> ObterLivrosUsuario(int usuarioId)
//        {
//            var context = new ApplicationDbContext();
//            return context.Livros
//                .Where(l => l.UsuarioId == usuarioId)
//                .OrderByDescending(l => l.DataAdicao);
//        }
//    }
//}