using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using gosti2.Models;
using gosti2.Tools;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace gosti2.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ✅ Connection string corrigida para usar o nome correto
        public ApplicationDbContext() : base("name=CJ3027333PR2")
        {
            // ✅ Configurações otimizadas para performance
            Database.SetInitializer<ApplicationDbContext>(null);
            this.Configuration.LazyLoadingEnabled = false;  // ✅ Desabilitado para melhor performance
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;

            // ✅ LOG de criação do contexto
            DiagnosticContext.LogarInfo("ApplicationDbContext criado - Conexão estabelecida");
        }

        // ✅ DbSets 100% COMPATIVEIS com suas classes e com o banco
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<CategoriaTier> CategoriaTiers { get; set; }
        public DbSet<LikeDislike> LikesDislikes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        // ✅ SOBRESCREVER SaveChanges para logging
        public override int SaveChanges()
        {
            try
            {
                var entidadesAlteradas = ChangeTracker.Entries()
                    .Count(e => e.State == EntityState.Added ||
                               e.State == EntityState.Modified ||
                               e.State == EntityState.Deleted);

                var resultado = base.SaveChanges();

                DiagnosticContext.LogarInfo($"SaveChanges executado: {resultado} registros afetados ({entidadesAlteradas} entidades alteradas)");

                return resultado;
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Erro durante SaveChanges no ApplicationDbContext", ex);
                throw; // Repropaga a exceção
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ✅ Remove pluralização para usar nomes exatos das tabelas do SQL
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // ✅ Configuração compatível com o schema do banco
            ConfigureUsuario(modelBuilder);
            ConfigureLivro(modelBuilder);
            ConfigureComentario(modelBuilder);
            ConfigureMensagem(modelBuilder);
            ConfigureLikeDislike(modelBuilder);
            ConfigureCategoriaTier(modelBuilder);
            ConfigureAvaliacao(modelBuilder);

            base.OnModelCreating(modelBuilder);

            DiagnosticContext.LogarInfo("Modelo do Entity Framework configurado com sucesso");
        }

        private void ConfigureUsuario(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Usuario>();

            // ✅ CHAVE PRIMÁRIA CORRETA
            entity.HasKey(u => u.UsuarioId);

            // ✅ PROPRIEDADES EXISTENTES - COMPATÍVEL COM BANCO
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

            entity.Property(u => u.DataNascimento)
                  .IsRequired()
                  .HasColumnType("date");

            entity.Property(u => u.DataCadastro)
                  .IsRequired()
                  .HasColumnType("datetime2");

            entity.Property(u => u.UltimoLogin)
                  .IsOptional()
                  .HasColumnType("datetime2");

            // ✅ ÍNDICES ÚNICOS - COMPATÍVEL COM SQL
            entity.HasIndex(u => u.Email)
                  .IsUnique()
                  .HasName("IX_Usuarios_Email");

            entity.HasIndex(u => u.NomeUsuario)
                  .IsUnique()
                  .HasName("IX_Usuarios_NomeUsuario");
        }

        private void ConfigureLivro(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Livro>();

            entity.HasKey(l => l.LivroId);

            // ✅ PROPRIEDADES EXISTENTES - COMPATÍVEL COM BANCO
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

            entity.Property(l => l.DataAdicao)
                  .IsRequired()
                  .HasColumnType("datetime2");

            // ✅ ÍNDICES PARA PERFORMANCE - COMPATÍVEL COM SQL
            entity.HasIndex(l => l.Titulo)
                  .HasName("IX_Livros_Titulo");

            entity.HasIndex(l => l.Autor)
                  .HasName("IX_Livros_Autor");

            entity.HasIndex(l => l.UsuarioId)
                  .HasName("IX_Livros_UsuarioId");

            // ✅ RELACIONAMENTOS
            entity.HasRequired(l => l.Usuario)
                  .WithMany(u => u.Livros)
                  .HasForeignKey(l => l.UsuarioId)
                  .WillCascadeOnDelete(true);
        }

        private void ConfigureComentario(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Comentario>();

            entity.HasKey(c => c.ComentarioId);

            entity.Property(c => c.Texto)
                  .IsRequired()
                  .HasMaxLength(2000);

            entity.Property(c => c.DataComentario)
                  .IsRequired()
                  .HasColumnType("datetime2");

            entity.Property(c => c.DataEdicao)
                  .IsOptional()
                  .HasColumnType("datetime2");

            // ✅ ÍNDICES PARA PERFORMANCE - COMPATÍVEL COM SQL
            entity.HasIndex(c => c.LivroId)
                  .HasName("IX_Comentarios_LivroId");

            entity.HasIndex(c => c.UsuarioId);

            // ✅ RELACIONAMENTOS
            entity.HasRequired(c => c.Livro)
                  .WithMany()
                  .HasForeignKey(c => c.LivroId)
                  .WillCascadeOnDelete(true);

            entity.HasRequired(c => c.Usuario)
                  .WithMany()
                  .HasForeignKey(c => c.UsuarioId);
        }

        private void ConfigureMensagem(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Mensagem>();

            entity.HasKey(m => m.MensagemId);

            // ✅ CORREÇÃO: A classe usa "Conteudo" mapeado para "Texto" no banco
            entity.Property(m => m.Conteudo)
                  .IsRequired()
                  .HasMaxLength(2000)
                  .HasColumnName("Texto"); // ✅ MAPEIA CORRETAMENTE PARA A COLUNA NO BANCO

            entity.Property(m => m.DataEnvio)
                  .IsRequired()
                  .HasColumnType("datetime2");

            // ✅ ÍNDICES PARA CHAT - COMPATÍVEL COM SQL
            entity.HasIndex(m => m.RemetenteId)
                  .HasName("IX_Mensagens_RemetenteDestinatario");

            entity.HasIndex(m => m.DestinatarioId)
                  .HasName("IX_Mensagens_RemetenteDestinatario");

            // ✅ RELACIONAMENTOS
            entity.HasRequired(m => m.Remetente)
                  .WithMany()
                  .HasForeignKey(m => m.RemetenteId)
                  .WillCascadeOnDelete(false);

            entity.HasRequired(m => m.Destinatario)
                  .WithMany()
                  .HasForeignKey(m => m.DestinatarioId)
                  .WillCascadeOnDelete(false);
        }

        private void ConfigureLikeDislike(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<LikeDislike>();

            entity.HasKey(ld => ld.LikeDislikeId);

            entity.Property(ld => ld.DataAcao)
                  .IsRequired()
                  .HasColumnType("datetime2");

            // ✅ CONSTRAINT ÚNICA (1 like/dislike por usuário/livro)
            entity.HasIndex(ld => new { ld.LivroId, ld.UsuarioId })
                  .IsUnique()
                  .HasName("UK_LivroUsuario");

            // ✅ RELACIONAMENTOS
            entity.HasRequired(ld => ld.Livro)
                  .WithMany()
                  .HasForeignKey(ld => ld.LivroId)
                  .WillCascadeOnDelete(true);

            entity.HasRequired(ld => ld.Usuario)
                  .WithMany()
                  .HasForeignKey(ld => ld.UsuarioId);
        }

        private void ConfigureCategoriaTier(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CategoriaTier>();

            entity.HasKey(ct => ct.CategoriaTierId);

            entity.Property(ct => ct.Nome)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(ct => ct.Descricao)
                  .IsOptional()
                  .HasMaxLength(255);

            // ✅ CORREÇÃO: Valor padrão removido - deve ser definido na classe
            entity.Property(ct => ct.Cor)
                  .IsOptional()
                  .HasMaxLength(20);
        }

        private void ConfigureAvaliacao(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Avaliacao>();

            entity.HasKey(a => a.AvaliacaoId);

            entity.Property(a => a.Comentario)
                  .IsOptional()
                  .HasMaxLength(1000);

            entity.Property(a => a.DataAvaliacao)
                  .IsRequired()
                  .HasColumnType("datetime2");

            // ✅ CONSTRAINT ÚNICA (1 avaliação por usuário/livro)
            entity.HasIndex(a => new { a.LivroId, a.UsuarioId })
                  .IsUnique()
                  .HasName("UK_Avaliacao_UsuarioLivro");

            // ✅ VALIDAÇÃO DE NOTA (1-5)
            entity.Property(a => a.Nota)
                  .IsRequired();

            // ✅ RELACIONAMENTOS
            entity.HasRequired(a => a.Livro)
                  .WithMany()
                  .HasForeignKey(a => a.LivroId)
                  .WillCascadeOnDelete(true);

            entity.HasRequired(a => a.Usuario)
                  .WithMany()
                  .HasForeignKey(a => a.UsuarioId);
        }

        // ✅ MÉTODO PARA VERIFICAR CONEXÃO
        public bool TestarConexao()
        {
            try
            {
                return Database.Exists();
            }
            catch (Exception ex)
            {
                DiagnosticContext.LogarErro("Falha ao testar conexão no ApplicationDbContext", ex);
                return false;
            }
        }

        // ✅ DESTRUTOR PARA LOG
        ~ApplicationDbContext()
        {
            DiagnosticContext.LogarInfo("ApplicationDbContext finalizado");
        }
    }
}