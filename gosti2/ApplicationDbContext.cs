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

            // Estratégia de banco simples
            Database.SetInitializer(new SimpleDatabaseInitializer());
        }

        // DbSets
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<LikeDislike> LikesDislikes { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<CategoriaTier> CategoriaTiers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove convenção de pluralização para simplificar
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }

    // Inicializador simples do banco
    public class SimpleDatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Adiciona categorias tier padrão se necessário
            var categoriasPadrao = CategoriaTierHelper.ObterCategoriasPadrao();
            context.CategoriaTiers.AddRange(categoriasPadrao);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}