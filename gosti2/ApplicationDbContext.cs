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

            // CRIAR BANCO AUTOMATICAMENTE SE NÃO EXISTIR
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

            // Garantir que o banco existe
            Database.Initialize(force: false);
        }

        // DbSets - TODAS AS ENTIDADES DO MODELO
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<CategoriaTier> CategoriasTier { get; set; } // ✅ ADICIONADO

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove convenção de pluralização
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configurações de relacionamento para Mensagens
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