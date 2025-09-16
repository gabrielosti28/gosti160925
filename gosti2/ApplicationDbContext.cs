using gosti2;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base("name=DefaultConnection")
    {
        // Desabilita inicializações automáticas para evitar conflitos
        Database.SetInitializer<ApplicationDbContext>(null);
   
    }
   

    // Tabelas
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<CategoriaTier> CategoriasTier { get; set; }
    public DbSet<Mensagem> Mensagens { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");

        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        // Configuração específica para a entidade Livro
        modelBuilder.Entity<Livro>()
            .Property(l => l.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Livro>()
            .Property(l => l.Autor)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Livro>()
            .Property(l => l.CategoriaId)
            .IsRequired();

        modelBuilder.Entity<Livro>()
            .Property(l => l.Descricao)
            .HasMaxLength(1000);  

        modelBuilder.Entity<Livro>()  
            .Property(l => l.DataAdicao)
            .IsRequired();

        modelBuilder.Entity<Livro>()
            .Property(l => l.Favorito)
            .IsRequired();

        modelBuilder.Entity<Livro>()
            .Property(l => l.Lido)
            .IsRequired();

        modelBuilder.Entity<Livro>()
            .HasRequired(l => l.Usuario)
            .WithMany(u => u.Livros)
            .HasForeignKey(l => l.UsuarioId)
            .WillCascadeOnDelete(false);

        base.OnModelCreating(modelBuilder);
    
    }
}