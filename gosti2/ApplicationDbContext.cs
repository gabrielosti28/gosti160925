using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using gosti2.Models;
using gosti2.Tools;
using System.Data.Entity.Infrastructure.Annotations;

namespace gosti2.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region ✅ CONSTRUTORES

        // ✅ CONSTRUTOR PRINCIPAL - USA CONNECTION STRING DO APP.CONFIG
        public ApplicationDbContext() : base("name=ApplicationDbContext")
        {
            ConfigureContext();
        }

        // ✅ CONSTRUTOR ALTERNATIVO - PARA CONEXÃO DINÂMICA
        public ApplicationDbContext(string connectionString) : base(connectionString)
        {
            ConfigureContext();
        }

        // ✅ CONSTRUTOR COM CONTROLE DE INITIALIZER
        public ApplicationDbContext(string connectionString, bool enableInitializer) : base(connectionString)
        {
            if (!enableInitializer)
            {
                Database.SetInitializer<ApplicationDbContext>(null);
            }
            ConfigureContext();
        }

        #endregion

        #region ✅ DBSETS - MAPEAMENTO COMPLETO

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SistemaLog> SistemaLogs { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<CategoriaTier> CategoriaTiers { get; set; }
        public DbSet<LikeDislike> LikesDislikes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        #endregion

        #region 🔧 CONFIGURAÇÃO DO CONTEXTO

        private void ConfigureContext()
        {
            // ✅ CONFIGURAÇÕES DE PERFORMANCE
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;

            // ✅ CONFIGURAÇÃO DE TIMEOUT (30 segundos)
            this.Database.CommandTimeout = 30;

            // ✅ LOG DE CRIAÇÃO (SEGURO)
            SafeLogInfo("ApplicationDbContext criado - Configuração aplicada");
        }

        #endregion

        #region 💾 SOBRESCRITA DE SAVECHANGES

        public override int SaveChanges()
        {
            try
            {
                // ✅ VALIDAÇÃO DE ENTIDADES ANTES DE SALVAR
                ValidateEntities();

                // ✅ TIMESTAMP AUTOMÁTICO PARA ENTIDADES COM DATACADASTRO
                UpdateTimestamps();

                var entidadesAlteradas = ChangeTracker.Entries()
                    .Count(e => e.State == EntityState.Added ||
                               e.State == EntityState.Modified ||
                               e.State == EntityState.Deleted);

                var resultado = base.SaveChanges();

                SafeLogInfo($"SaveChanges executado: {resultado} registros afetados ({entidadesAlteradas} entidades alteradas)");

                return resultado;
            }
            catch (Exception ex)
            {
                SafeLogError("Erro durante SaveChanges no ApplicationDbContext", ex);
                throw; // Repropaga a exceção
            }
        }

        // ✅ MÉTODO ASSÍNCRONO PARA OPERAÇÕES EM LOTE
        public async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            return await System.Threading.Tasks.Task.Run(() => SaveChanges());
        }

        private void ValidateEntities()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                // ✅ VALIDAÇÃO BÁSICA DE ENTIDADES
                if (entity is Usuario usuario)
                {
                    if (string.IsNullOrWhiteSpace(usuario.NomeUsuario))
                        throw new InvalidOperationException("Nome de usuário é obrigatório");
                }
            }
        }

        private void UpdateTimestamps()
        {
            var currentTime = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Usuario usuario && entry.State == EntityState.Added)
                {
                    if (usuario.DataCadastro == default)
                        usuario.DataCadastro = currentTime;
                }

                if (entry.Entity is Livro livro && entry.State == EntityState.Added)
                {
                    if (livro.DataAdicao == default)
                        livro.DataAdicao = currentTime;
                }

                if (entry.Entity is SistemaLog log && entry.State == EntityState.Added)
                {
                    if (log.DataHora == default)
                        log.DataHora = currentTime;
                }
            }
        }

        #endregion

        #region 🗄️ CONFIGURAÇÃO DO MODELO (FLUENT API)

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ✅ CONFIGURAÇÕES GLOBAIS
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // ✅ CONFIGURAÇÕES ESPECÍFICAS DE CADA ENTIDADE
            ConfigureUsuario(modelBuilder);
            ConfigureSistemaLog(modelBuilder);
            ConfigureLivro(modelBuilder);
            ConfigureComentario(modelBuilder);
            ConfigureMensagem(modelBuilder);
            ConfigureLikeDislike(modelBuilder);
            ConfigureCategoriaTier(modelBuilder);
            ConfigureAvaliacao(modelBuilder);

            base.OnModelCreating(modelBuilder);

            SafeLogInfo("Modelo do Entity Framework configurado com sucesso");
        }

        private void ConfigureUsuario(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Usuario>();

            entity.HasKey(u => u.UsuarioId);

            // ✅ PROPRIEDADES COM CONFIGURAÇÃO PRECISA
            entity.Property(u => u.NomeUsuario)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Usuarios_NomeUsuario") { IsUnique = true }));

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Usuarios_Email") { IsUnique = true }));

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

            // ✅ RELACIONAMENTOS
            entity.HasMany(u => u.Livros)
                  .WithRequired(l => l.Usuario)
                  .HasForeignKey(l => l.UsuarioId)
                  .WillCascadeOnDelete(true);
        }

        private void ConfigureSistemaLog(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<SistemaLog>();

            entity.HasKey(l => l.LogId);

            entity.Property(l => l.Nivel)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(l => l.Mensagem)
                  .IsRequired()
                  .HasColumnType("nvarchar(MAX)");

            entity.Property(l => l.StackTrace)
                  .IsOptional()
                  .HasColumnType("nvarchar(MAX)");

            entity.Property(l => l.Formulario)
                  .IsOptional()
                  .HasMaxLength(100);

            entity.Property(l => l.Metodo)
                  .IsOptional()
                  .HasMaxLength(200);

            entity.Property(l => l.ExceptionType)
                  .IsOptional()
                  .HasMaxLength(200);

            entity.Property(l => l.InnerException)
                  .IsOptional()
                  .HasColumnType("nvarchar(MAX)");

            entity.Property(l => l.DataHora)
                  .IsRequired()
                  .HasColumnType("datetime2")
                  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); // ✅ CONTROLE MANUAL

            // ✅ ÍNDICES PARA PERFORMANCE
            entity.HasIndex(l => l.DataHora)
                  .HasName("IX_SistemaLogs_DataHora");

            entity.HasIndex(l => l.Nivel)
                  .HasName("IX_SistemaLogs_Nivel");

            entity.HasIndex(l => l.Formulario)
                  .HasName("IX_SistemaLogs_Formulario");

            // ✅ RELACIONAMENTO OPCIONAL COM USUÁRIO
            entity.HasOptional(l => l.Usuario)
                  .WithMany()
                  .HasForeignKey(l => l.UsuarioId)
                  .WillCascadeOnDelete(false);
        }

        private void ConfigureLivro(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Livro>();

            entity.HasKey(l => l.LivroId);

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

            entity.Property(l => l.ISBN)
                  .IsOptional()
                  .HasMaxLength(20);

            entity.Property(l => l.Editora)
                  .IsOptional()
                  .HasMaxLength(100);

            entity.Property(l => l.DataAdicao)
                  .IsRequired()
                  .HasColumnType("datetime2");

            // ✅ ÍNDICES
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

            //entity.HasOptional(l => l.CategoriaTier)
            //      .WithMany()
            //      .HasForeignKey(l => l.CategoriaTierId)
            //      .WillCascadeOnDelete(false);
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

            // ✅ ÍNDICES
            entity.HasIndex(c => c.LivroId)
                  .HasName("IX_Comentarios_LivroId");

            // ✅ RELACIONAMENTOS
            entity.HasRequired(c => c.Livro)
                  .WithMany()
                  .HasForeignKey(c => c.LivroId)
                  .WillCascadeOnDelete(true);

            entity.HasRequired(c => c.Usuario)
                  .WithMany()
                  .HasForeignKey(c => c.UsuarioId)
                  .WillCascadeOnDelete(false);
        }

        private void ConfigureMensagem(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Mensagem>();

            entity.HasKey(m => m.MensagemId);

            // ✅ CORREÇÃO: MAPEAMENTO CORRETO ENTRE CLASSE E BANCO
            entity.Property(m => m.Conteudo)
                  .IsRequired()
                  .HasMaxLength(2000)
                  .HasColumnName("Texto"); // ✅ NOME DA COLUNA NO BANCO

            entity.Property(m => m.DataEnvio)
                  .IsRequired()
                  .HasColumnType("datetime2");

            // ✅ ÍNDICES COMPOSTOS
            entity.HasIndex(m => new { m.RemetenteId, m.DestinatarioId })
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

            // ✅ CONSTRAINT ÚNICA
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
                  .HasForeignKey(ld => ld.UsuarioId)
                  .WillCascadeOnDelete(false);
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

            // ✅ CONSTRAINT ÚNICA E CHECK CONSTRAINT
            entity.HasIndex(a => new { a.LivroId, a.UsuarioId })
                  .IsUnique()
                  .HasName("UK_Avaliacao_UsuarioLivro");

            // ✅ VALIDAÇÃO DE NOTA VIA FLUENT API
            entity.Property(a => a.Nota)
                  .IsRequired();

            // ✅ RELACIONAMENTOS
            entity.HasRequired(a => a.Livro)
                  .WithMany()
                  .HasForeignKey(a => a.LivroId)
                  .WillCascadeOnDelete(true);

            entity.HasRequired(a => a.Usuario)
                  .WithMany()
                  .HasForeignKey(a => a.UsuarioId)
                  .WillCascadeOnDelete(false);
        }

        #endregion

        #region 🔍 MÉTODOS UTILITÁRIOS

        // ✅ TESTE DE CONEXÃO ROBUSTO
        public bool TestarConexao()
        {
            try
            {
                return this.Database.Exists();
            }
            catch (Exception ex)
            {
                SafeLogError("Falha ao testar conexão no ApplicationDbContext", ex);
                return false;
            }
        }

        // ✅ MÉTODO PARA EXECUTAR SQL DIRETO
        public int ExecutarSqlCommand(string sql, params object[] parameters)
        {
            try
            {
                return this.Database.ExecuteSqlCommand(sql, parameters);
            }
            catch (Exception ex)
            {
                SafeLogError($"Erro ao executar SQL: {sql}", ex);
                throw;
            }
        }

        // ✅ MÉTODO PARA LIMPAR CONTEXTO
        public void LimparContexto()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }

            SafeLogInfo($"Contexto limpo: {entries.Count} entidades desanexadas");
        }

        #endregion

        #region 📝 LOGGING SEGURO

        private void SafeLogInfo(string message)
        {
            try
            {
                DiagnosticContext.LogarInfo(message);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine($"INFO: {message}");
            }
        }

        private void SafeLogError(string message, Exception ex)
        {
            try
            {
                DiagnosticContext.LogarErro(message, ex);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine($"ERRO: {message} - {ex.Message}");
            }
        }

        #endregion

        #region 🧹 DESTRUTOR E DISPOSE

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SafeLogInfo("ApplicationDbContext sendo descartado");
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}