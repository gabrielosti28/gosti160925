using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using gosti2.Models;

namespace gosti2.Data
{
    /// <summary>
    /// Gerenciador central de toda a lógica de negócios do BookConnect.
    /// Responsável por: Autenticação, Usuários, Livros, Comentários, Likes e Verificações.
    /// </summary>
    public static class AppManager
    {
        #region ===== PROPRIEDADES =====

        /// <summary>
        /// Usuário atualmente logado no sistema
        /// </summary>
        public static Usuario UsuarioLogado { get; private set; }

        /// <summary>
        /// Verifica se há um usuário logado
        /// </summary>
        public static bool EstaLogado => UsuarioLogado != null;

        #endregion

        #region ===== AUTENTICAÇÃO =====

        /// <summary>
        /// Realiza login de usuário com email e senha
        /// </summary>
        public static bool Login(string email, string senha)
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Email e senha são obrigatórios.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Criptografa a senha para comparar
                    string senhaCriptografada = CriptografarSenha(senha);

                    // Busca usuário com email e senha correspondentes
                    var usuario = db.Usuarios.FirstOrDefault(u =>
                        u.Email == email &&
                        u.Senha == senhaCriptografada &&
                        u.Ativo);

                    if (usuario != null)
                    {
                        UsuarioLogado = usuario;
                        usuario.UltimoLogin = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }

                    MessageBox.Show("Email ou senha incorretos.", "Erro de Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar login: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Realiza logout do usuário atual
        /// </summary>
        public static void Logout()
        {
            UsuarioLogado = null;
        }

        /// <summary>
        /// Cadastra novo usuário no sistema
        /// </summary>
        public static bool CadastrarUsuario(string nome, string email, string senha, DateTime dataNascimento, string bio = null)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(nome) || nome.Length < 3)
            {
                MessageBox.Show("Nome deve ter pelo menos 3 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                MessageBox.Show("Email inválido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            {
                MessageBox.Show("Senha deve ter pelo menos 6 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar idade (mínimo 13 anos)
            int idade = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < 13)
            {
                MessageBox.Show("É necessário ter pelo menos 13 anos para se cadastrar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    // Verificar se email já existe
                    if (db.Usuarios.Any(u => u.Email == email))
                    {
                        MessageBox.Show("Este email já está cadastrado.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Verificar se nome de usuário já existe
                    if (db.Usuarios.Any(u => u.NomeUsuario == nome))
                    {
                        MessageBox.Show("Este nome de usuário já existe.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Criar novo usuário
                    var usuario = new Usuario
                    {
                        NomeUsuario = nome,
                        Email = email.ToLower().Trim(),
                        Senha = CriptografarSenha(senha),
                        DataNascimento = dataNascimento,
                        Bio = bio,
                        DataCadastro = DateTime.Now,
                        Ativo = true
                    };

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();

                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar usuário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Atualiza informações do perfil do usuário logado
        /// </summary>
        public static bool AtualizarPerfil(string nome, string bio, string website, string localizacao)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

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

                        // Atualiza usuário logado em memória
                        UsuarioLogado = usuario;

                        MessageBox.Show("Perfil atualizado com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar perfil: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region ===== GESTÃO DE LIVROS =====

        /// <summary>
        /// Adiciona novo livro para o usuário logado
        /// </summary>
        public static bool AdicionarLivro(Livro livro)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado para adicionar livros.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validações
            if (string.IsNullOrWhiteSpace(livro.Titulo))
            {
                MessageBox.Show("O título do livro é obrigatório.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(livro.Autor))
            {
                MessageBox.Show("O autor do livro é obrigatório.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    livro.UsuarioId = UsuarioLogado.UsuarioId;
                    livro.DataAdicao = DateTime.Now;

                    db.Livros.Add(livro);
                    db.SaveChanges();

                    MessageBox.Show("Livro adicionado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Atualiza informações de um livro existente
        /// </summary>
        public static bool AtualizarLivro(Livro livro)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livroExistente = db.Livros.Find(livro.LivroId);

                    if (livroExistente == null)
                    {
                        MessageBox.Show("Livro não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Verificar se o usuário é o dono do livro
                    if (livroExistente.UsuarioId != UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você não tem permissão para editar este livro.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Atualizar propriedades
                    livroExistente.Titulo = livro.Titulo;
                    livroExistente.Autor = livro.Autor;
                    livroExistente.Genero = livro.Genero;
                    livroExistente.Descricao = livro.Descricao;
                    livroExistente.Capa = livro.Capa;
                    livroExistente.Lido = livro.Lido;
                    livroExistente.Favorito = livro.Favorito;
                    livroExistente.ISBN = livro.ISBN;
                    livroExistente.AnoPublicacao = livro.AnoPublicacao;
                    livroExistente.Editora = livro.Editora;
                    livroExistente.Paginas = livro.Paginas;

                    db.SaveChanges();

                    MessageBox.Show("Livro atualizado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Remove um livro do sistema
        /// </summary>
        public static bool RemoverLivro(int livroId)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var livro = db.Livros.Find(livroId);

                    if (livro == null)
                    {
                        MessageBox.Show("Livro não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Verificar permissão
                    if (livro.UsuarioId != UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você não tem permissão para remover este livro.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    db.Livros.Remove(livro);
                    db.SaveChanges();

                    MessageBox.Show("Livro removido com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Obtém um livro específico por ID
        /// </summary>
        public static Livro ObterLivro(int livroId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Livros
                        .Include(l => l.Usuario)
                        .FirstOrDefault(l => l.LivroId == livroId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar livro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Carrega todos os livros de um usuário
        /// </summary>
        public static void CarregarLivrosUsuario(DataGridView grid, int? usuarioId = null)
        {
            try
            {
                int idBusca = usuarioId ?? UsuarioLogado?.UsuarioId ?? 0;

                if (idBusca == 0)
                {
                    MessageBox.Show("Usuário não identificado.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new ApplicationDbContext())
                {
                    var livros = db.Livros
                        .Where(l => l.UsuarioId == idBusca)
                        .OrderByDescending(l => l.DataAdicao)
                        .ToList();

                    grid.DataSource = livros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Busca livros por título ou autor
        /// </summary>
        public static void BuscarLivros(DataGridView grid, string termo, int? usuarioId = null)
        {
            try
            {
                int idBusca = usuarioId ?? UsuarioLogado?.UsuarioId ?? 0;

                using (var db = new ApplicationDbContext())
                {
                    var query = db.Livros.AsQueryable();

                    if (idBusca > 0)
                    {
                        query = query.Where(l => l.UsuarioId == idBusca);
                    }

                    if (!string.IsNullOrWhiteSpace(termo))
                    {
                        query = query.Where(l =>
                            l.Titulo.Contains(termo) ||
                            l.Autor.Contains(termo));
                    }

                    var livros = query.OrderByDescending(l => l.DataAdicao).ToList();
                    grid.DataSource = livros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar livros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ===== GESTÃO DE COMENTÁRIOS =====

        /// <summary>
        /// Adiciona comentário a um livro
        /// </summary>
        public static bool AdicionarComentario(int livroId, string texto)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado para comentar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(texto) || texto.Length < 5)
            {
                MessageBox.Show("O comentário deve ter pelo menos 5 caracteres.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var comentario = new Comentario
                    {
                        Texto = texto.Trim(),
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
                MessageBox.Show($"Erro ao adicionar comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Remove um comentário
        /// </summary>
        public static bool RemoverComentario(int comentarioId)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var comentario = db.Comentarios.Find(comentarioId);

                    if (comentario == null)
                    {
                        MessageBox.Show("Comentário não encontrado.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Verificar permissão
                    if (comentario.UsuarioId != UsuarioLogado.UsuarioId)
                    {
                        MessageBox.Show("Você não tem permissão para remover este comentário.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    db.Comentarios.Remove(comentario);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover comentário: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Carrega comentários de um livro
        /// </summary>
        public static System.Collections.Generic.List<Comentario> CarregarComentarios(int livroId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Comentarios
                        .Include(c => c.Usuario)
                        .Where(c => c.LivroId == livroId)
                        .OrderByDescending(c => c.DataComentario)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar comentários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new System.Collections.Generic.List<Comentario>();
            }
        }

        #endregion

        #region ===== GESTÃO DE LIKES/DISLIKES =====

        /// <summary>
        /// Registra ou atualiza voto (like/dislike) em um livro
        /// </summary>
        public static bool VotarLivro(int livroId, bool isLike)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado para votar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var votoExistente = db.LikesDislikes
                        .FirstOrDefault(ld =>
                            ld.LivroId == livroId &&
                            ld.UsuarioId == UsuarioLogado.UsuarioId);

                    if (votoExistente != null)
                    {
                        // Atualizar voto existente
                        votoExistente.IsLike = isLike;
                        votoExistente.DataAcao = DateTime.Now;
                    }
                    else
                    {
                        // Criar novo voto
                        var novoVoto = new LikeDislike
                        {
                            LivroId = livroId,
                            UsuarioId = UsuarioLogado.UsuarioId,
                            IsLike = isLike,
                            DataAcao = DateTime.Now
                        };
                        db.LikesDislikes.Add(novoVoto);
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Remove voto de um livro
        /// </summary>
        public static bool RemoverVoto(int livroId)
        {
            if (!EstaLogado)
            {
                MessageBox.Show("É necessário estar logado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var voto = db.LikesDislikes
                        .FirstOrDefault(ld =>
                            ld.LivroId == livroId &&
                            ld.UsuarioId == UsuarioLogado.UsuarioId);

                    if (voto != null)
                    {
                        db.LikesDislikes.Remove(voto);
                        db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover voto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Obtém estatísticas de votos de um livro
        /// </summary>
        public static (int likes, int dislikes, bool? votoUsuario) ObterVotosLivro(int livroId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var likes = db.LikesDislikes.Count(ld => ld.LivroId == livroId && ld.IsLike);
                    var dislikes = db.LikesDislikes.Count(ld => ld.LivroId == livroId && !ld.IsLike);

                    bool? votoUsuario = null;
                    if (EstaLogado)
                    {
                        var voto = db.LikesDislikes
                            .FirstOrDefault(ld =>
                                ld.LivroId == livroId &&
                                ld.UsuarioId == UsuarioLogado.UsuarioId);

                        if (voto != null)
                        {
                            votoUsuario = voto.IsLike;
                        }
                    }

                    return (likes, dislikes, votoUsuario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter votos: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, 0, null);
            }
        }

        #endregion

        #region ===== ESTATÍSTICAS =====

        /// <summary>
        /// Obtém estatísticas gerais do usuário
        /// </summary>
        public static (int totalLivros, int livrosLidos, int livrosFavoritos) ObterEstatisticasUsuario(int? usuarioId = null)
        {
            try
            {
                int idBusca = usuarioId ?? UsuarioLogado?.UsuarioId ?? 0;

                if (idBusca == 0)
                    return (0, 0, 0);

                using (var db = new ApplicationDbContext())
                {
                    var totalLivros = db.Livros.Count(l => l.UsuarioId == idBusca);
                    var livrosLidos = db.Livros.Count(l => l.UsuarioId == idBusca && l.Lido);
                    var livrosFavoritos = db.Livros.Count(l => l.UsuarioId == idBusca && l.Favorito);

                    return (totalLivros, livrosLidos, livrosFavoritos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter estatísticas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, 0, 0);
            }
        }

        #endregion

        #region ===== VERIFICAÇÕES E UTILIDADES =====

        /// <summary>
        /// Verifica conexão com o banco de dados
        /// </summary>
        public static bool VerificarBanco()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Database.Connection.Open();
                    db.Database.Connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problema na conexão com o banco: {ex.Message}",
                    "Erro de Banco", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Testa conexão e retorna mensagem detalhada
        /// </summary>
        public static (bool sucesso, string mensagem) TestarConexao()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Database.Connection.Open();
                    var servidor = db.Database.Connection.DataSource;
                    var banco = db.Database.Connection.Database;
                    db.Database.Connection.Close();

                    return (true, $"✅ Conectado com sucesso!\nServidor: {servidor}\nBanco: {banco}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"❌ Falha na conexão:\n{ex.Message}");
            }
        }

        /// <summary>
        /// Criptografa senha usando SHA256 + salt
        /// </summary>
        private static string CriptografarSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(senha + "BookConnect2024");
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        #endregion
    }
}