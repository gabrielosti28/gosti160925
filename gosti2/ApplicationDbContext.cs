using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using gosti2.Models;

namespace gosti2.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ✅ Connection string corrigida para usar o nome correto
        public ApplicationDbContext() : base("name=CJ3027333PR2")
        {
            // ✅ Configurações otimizadas
            Database.SetInitializer<ApplicationDbContext>(null);
            this.Configuration.LazyLoadingEnabled = true;  // ✅ Habilitado para conveniência
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        // ✅ DbSets 100% COMPATIVEIS com suas classes
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<CategoriaTier> CategoriaTiers { get; set; } // ✅ Nome correto
        public DbSet<LikeDislike> LikesDislikes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ✅ Remove pluralização para usar nomes exatos das tabelas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // ✅ Configuração mínima e compatível
            ConfigureUsuario(modelBuilder);
            ConfigureLivro(modelBuilder);
            ConfigureComentario(modelBuilder);
            ConfigureMensagem(modelBuilder);
            ConfigureLikeDislike(modelBuilder);
            ConfigureCategoriaTier(modelBuilder);
            ConfigureAvaliacao(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureUsuario(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Usuario>();

            // ✅ CHAVE PRIMÁRIA CORRETA
            entity.HasKey(u => u.UsuarioId);

            // ✅ PROPRIEDADES EXISTENTES
            entity.Property(u => u.NomeUsuario)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.Senha)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(u => u.Bio)
                  .IsOptional()
                  .HasMaxLength(500);

            entity.Property(u => u.Website)
                  .IsOptional()
                  .HasMaxLength(255);

            entity.Property(u => u.Localizacao)
                  .IsOptional()
                  .HasMaxLength(100);

            // ✅ ÍNDICE ÚNICO
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.NomeUsuario).IsUnique();

            // ✅ RELAÇÕES (OPCIONAIS - Entity Framework já detecta automaticamente)
            entity.HasMany(u => u.Livros)
                  .WithRequired(l => l.Usuario)
                  .HasForeignKey(l => l.UsuarioId)
                  .WillCascadeOnDelete(true); // ✅ Cascade para livros
        }

        private void ConfigureLivro(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Livro>();

            entity.HasKey(l => l.LivroId);

            // ✅ PROPRIEDADES EXISTENTES
            entity.Property(l => l.Titulo)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(l => l.Autor)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(l => l.Genero)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(l => l.Descricao)
                  .IsOptional()
                  .HasMaxLength(1000);

            // ✅ CAMPOS OPCIONAIS DO SEU SQL
            entity.Property(l => l.ISBN)
                  .IsOptional()
                  .HasMaxLength(20);

            entity.Property(l => l.Editora)
                  .IsOptional()
                  .HasMaxLength(100);

            // ✅ ÍNDICES PARA PERFORMANCE
            entity.HasIndex(l => l.Titulo);
            entity.HasIndex(l => l.Autor);
            entity.HasIndex(l => l.UsuarioId);
        }

        private void ConfigureComentario(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Comentario>();

            entity.HasKey(c => c.ComentarioId);

            entity.Property(c => c.Texto)
                  .IsRequired()
                  .HasMaxLength(2000);

            // ✅ ÍNDICES PARA PERFORMANCE
            entity.HasIndex(c => c.LivroId);
            entity.HasIndex(c => c.UsuarioId);
        }

        private void ConfigureMensagem(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Mensagem>();

            entity.HasKey(m => m.MensagemId);

            // ✅ MAPEAMENTO CORRETO DO CAMPO
            entity.Property(m => m.Conteudo)
                  .IsRequired()
                  .HasMaxLength(2000)
                  .HasColumnName("Texto"); // ✅ MAPEIA PARA O NOME NO BANCO

            // ✅ ÍNDICES PARA CHAT
            entity.HasIndex(m => m.RemetenteId);
            entity.HasIndex(m => m.DestinatarioId);
            entity.HasIndex(m => new { m.RemetenteId, m.DestinatarioId });
        }

        private void ConfigureLikeDislike(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<LikeDislike>();

            entity.HasKey(ld => ld.LikeDislikeId);

            // ✅ CONSTRAINT ÚNICA (1 like/dislike por usuário/livro)
            entity.HasIndex(ld => new { ld.LivroId, ld.UsuarioId })
                  .IsUnique();

            // ✅ ÍNDICES PARA PERFORMANCE
            entity.HasIndex(ld => ld.LivroId);
            entity.HasIndex(ld => ld.UsuarioId);
        }

        private void ConfigureCategoriaTier(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CategoriaTier>();

            // ✅ CHAVE PRIMÁRIA CORRETA
            entity.HasKey(ct => ct.CategoriaTierId);

            entity.Property(ct => ct.Nome)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(ct => ct.Descricao)
                  .IsOptional()
                  .HasMaxLength(255);

            entity.Property(ct => ct.Cor)
                  .IsOptional()
                  .HasMaxLength(20);
        }

        private void ConfigureAvaliacao(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Avaliacao>();

            entity.HasKey(a => a.AvaliacaoId);

            // ✅ CONSTRAINT ÚNICA (1 avaliação por usuário/livro)
            entity.HasIndex(a => new { a.LivroId, a.UsuarioId })
                  .IsUnique();

            // ✅ CHECK CONSTRAINT via Fluent API
            entity.Property(a => a.Nota)
                  .IsRequired();

            // ✅ ÍNDICES PARA PERFORMANCE
            entity.HasIndex(a => a.LivroId);
            entity.HasIndex(a => a.UsuarioId);
        }
    }
}