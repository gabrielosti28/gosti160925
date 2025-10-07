using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using gosti2.Models;

namespace gosti2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        // DbSets SIMPLES
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<LikeDislike> LikesDislikes { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<CategoriaTier> CategoriaTiers { get; set; }
        public DbSet<SistemaLog> SistemaLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}