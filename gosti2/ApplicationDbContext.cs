using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using gosti2.Models;

namespace gosti2.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ✅ Nome do banco mantido conforme requerimento
        public ApplicationDbContext() : base("name=CJ3027333PR2")
        {
            // Desabilita inicializações automáticas
            Database.SetInitializer<ApplicationDbContext>(null);

            // Configurações de performance
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        // ✅ DbSets principais
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<CategoriaTier> CategoriasTier { get; set; }
        public DbSet<LikeDislike> LikesDislikes { get; set; }

        // ✅ DbSet para sistema de avaliações
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // ✅ Configuração específica para cada entidade
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
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ✅ Configuração para campos profissionais
            modelBuilder.Entity<Usuario>()
                .Property(u => u.BioProfissional)
                .IsOptional()
                .HasMaxLength(1000);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Especialidade)
                .IsOptional()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Twitter)
                .IsOptional()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Instagram)
                .IsOptional()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Website)
                .IsOptional()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Localizacao)
                .IsOptional()
                .HasMaxLength(100);

            // ✅ Relações
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Livros)
                .WithRequired(l => l.Usuario)
                .HasForeignKey(l => l.UsuarioId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Comentarios)
                .WithRequired(c => c.Usuario)
                .HasForeignKey(c => c.UsuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Avaliacoes)
                .WithRequired(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureLivro(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasKey(l => l.LivroId);

            modelBuilder.Entity<Livro>()
                .Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Livro>()
                .Property(l => l.Autor)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Livro>()
                .Property(l => l.Genero)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Livro>()
                .Property(l => l.Descricao)
                .IsOptional()
                .HasMaxLength(1000);

            // ✅ Campos para expansão futura
            modelBuilder.Entity<Livro>()
                .Property(l => l.ISBN)
                .IsOptional()
                .HasMaxLength(20);

            modelBuilder.Entity<Livro>()
                .Property(l => l.AnoPublicacao)
                .IsOptional();

            modelBuilder.Entity<Livro>()
                .Property(l => l.Editora)
                .IsOptional()
                .HasMaxLength(100);

            modelBuilder.Entity<Livro>()
                .Property(l => l.Paginas)
                .IsOptional();

            modelBuilder.Entity<Livro>()
                .Property(l => l.Avaliacao)
                .IsOptional()
                .HasPrecision(3, 2);

            // ✅ Relações
            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Comentarios)
                .WithRequired(c => c.Livro)
                .HasForeignKey(c => c.LivroId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Livro>()
                .HasMany(l => l.LikesDislikes)
                .WithRequired(ld => ld.Livro)
                .HasForeignKey(ld => ld.LivroId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Livro>()
                .HasMany(l => l.Avaliacoes)
                .WithRequired(a => a.Livro)
                .HasForeignKey(a => a.LivroId)
                .WillCascadeOnDelete(true);
        }

        private void ConfigureComentario(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comentario>()
                .HasKey(c => c.ComentarioId);

            modelBuilder.Entity<Comentario>()
                .Property(c => c.Texto)
                .IsRequired()
                .HasMaxLength(2000);

            // ✅ Campos para futuras funcionalidades
            modelBuilder.Entity<Comentario>()
                .Property(c => c.Editado)
                .IsRequired();

            modelBuilder.Entity<Comentario>()
                .Property(c => c.DataEdicao)
                .IsOptional();
        }

        private void ConfigureMensagem(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mensagem>()
                .HasKey(m => m.MensagemId);

            modelBuilder.Entity<Mensagem>()
                .Property(m => m.Conteudo)
                .IsRequired()
                .HasMaxLength(1000);
        }

        private void ConfigureLikeDislike(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LikeDislike>()
                .HasKey(ld => ld.LikeDislikeId);

            modelBuilder.Entity<LikeDislike>()
                .HasIndex(ld => new { ld.LivroId, ld.UsuarioId })
                .IsUnique();
        }

        private void ConfigureCategoriaTier(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaTier>()
                .HasKey(ct => ct.CategoriaId);

            modelBuilder.Entity<CategoriaTier>()
                .Property(ct => ct.Nome)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<CategoriaTier>()
                .Property(ct => ct.Descricao)
                .IsOptional()
                .HasMaxLength(500);
        }

        private void ConfigureAvaliacao(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avaliacao>()
                .HasKey(a => a.AvaliacaoId);

            modelBuilder.Entity<Avaliacao>()
                .Property(a => a.Nota)
                .IsRequired()
                .HasPrecision(2, 1);

            modelBuilder.Entity<Avaliacao>()
                .Property(a => a.Comentario)
                .IsOptional()
                .HasMaxLength(500);

            // ✅ Um usuário só pode avaliar um livro uma vez
            modelBuilder.Entity<Avaliacao>()
                .HasIndex(a => new { a.LivroId, a.UsuarioId })
                .IsUnique();
        }
    }
}