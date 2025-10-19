using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using gosti2.Models;

namespace gosti2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(DatabaseConfig.ConnectionString)
        {
            // Configurações otimizadas
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;

            // Não criar banco automaticamente - usar o script SQL fornecido
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        // DbSets - APENAS O QUE É USADO
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove convenção de pluralização
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configurações específicas se necessário
            modelBuilder.Entity<Mensagem>()
                .HasRequired(m => m.Remetente)
                .WithMany(u => u.MensagensEnviadas)
                .HasForeignKey(m => m.RemetenteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mensagem>()
                .HasRequired(m => m.Destinatario)
                .WithMany(u => u.MensagensRecebidas)
                .HasForeignKey(m => m.DestinatarioId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}