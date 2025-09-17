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
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<LikeDislike> LikesDislikes { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Comentario>().ToTable("Comentarios"); // ← Adicione esta linha
        

        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        // Configuração para LikeDislike
        modelBuilder.Entity<LikeDislike>()
            .ToTable("LikesDislikes")
            .HasKey(ld => ld.LikeDislikeId);

        modelBuilder.Entity<LikeDislike>()
            .HasRequired(ld => ld.Livro)
            .WithMany(l => l.LikesDislikes) // Certifique-se que Livro tem ICollection<LikeDislike>
            .HasForeignKey(ld => ld.LivroId)
            .WillCascadeOnDelete(true);

        //modelBuilder.Entity<LikeDislike>()
            //.HasRequired(ld => ld.Usuario)
           // .WithMany(u => u.LikesDislikes) // Certifique-se que Usuario tem ICollection<LikeDislike>
            //.HasForeignKey(ld => ld.UsuarioId)
           // .WillCascadeOnDelete(false);

        modelBuilder.Entity<LikeDislike>()
            .HasIndex(ld => new { ld.LivroId, ld.UsuarioId })
            .IsUnique();
        // Configuração para Usuario
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");

        // Configuração para Livro
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
            .WillCascadeOnDelete(true);

        // Configuração para Comentario
        modelBuilder.Entity<Comentario>()
            .HasRequired(c => c.Livro)
            .WithMany()
            .HasForeignKey(c => c.LivroId)
            .WillCascadeOnDelete(true);

        modelBuilder.Entity<Comentario>()
            .HasRequired(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.UsuarioId)
            .WillCascadeOnDelete(false);

        // Configuração para LikeDislike
        modelBuilder.Entity<LikeDislike>()
            .HasKey(ld => ld.LikeDislikeId);

        modelBuilder.Entity<LikeDislike>()
            .HasRequired(ld => ld.Livro)
            .WithMany()
            .HasForeignKey(ld => ld.LivroId)
            .WillCascadeOnDelete(true);

        modelBuilder.Entity<LikeDislike>()
            .HasRequired(ld => ld.Usuario)
            .WithMany()
            .HasForeignKey(ld => ld.UsuarioId)
            .WillCascadeOnDelete(false);

        modelBuilder.Entity<LikeDislike>()
            .HasIndex(ld => new { ld.LivroId, ld.UsuarioId })
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}