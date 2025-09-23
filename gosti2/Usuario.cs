using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }  // ✅ CORRIGIDO: UserId → UsuarioId

        [Required]
        [MaxLength(100)]
        [Column("NomeUsuario")] // ✅ CORRIGIDO: Mapeia para o nome correto no SQL
        public string NomeUsuario { get; set; } // ✅ CORRIGIDO: Nome → NomeUsuario

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Senha { get; set; }

        [Required]
        [Column(TypeName = "DATE")] // ✅ CORRIGIDO: Tipo correto para SQL
        public DateTime DataNascimento { get; set; } // ✅ CORRIGIDO: string → DateTime

        public byte[] FotoPerfil { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        // ✅ REMOVIDOS CAMPOS QUE NÃO EXISTEM NO SQL:
        // BioProfissional, Especialidade, Twitter, Instagram (não criados no SQL)

        // ✅ CAMPOS EXISTENTES NO SQL:
        [MaxLength(255)]
        public string Website { get; set; }

        [MaxLength(100)]
        public string Localizacao { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? UltimoLogin { get; set; }

        public bool Ativo { get; set; } = true;

        // ✅ RELAÇÕES COMPATIVEIS
        public virtual ICollection<Livro> Livros { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

        [InverseProperty("Remetente")]
        public virtual ICollection<Mensagem> MensagensEnviadas { get; set; }

        [InverseProperty("Destinatario")]
        public virtual ICollection<Mensagem> MensagensRecebidas { get; set; }

        public virtual ICollection<LikeDislike> LikesDislikes { get; set; }
        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }

        public Usuario()
        {
            // ✅ INICIALIZAÇÃO SEGURA
            Livros = new HashSet<Livro>();
            Comentarios = new HashSet<Comentario>();
            MensagensEnviadas = new HashSet<Mensagem>();
            MensagensRecebidas = new HashSet<Mensagem>();
            LikesDislikes = new HashSet<LikeDislike>();
            Avaliacoes = new HashSet<Avaliacao>();
        }

        // ✅ MÉTODOS DE NEGÓCIO (OPCIONAIS)
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }

        public bool EhMaiorDeIdade() => Idade >= 18;

        public string NomeCompleto => NomeUsuario; // Para compatibilidade com código existente

        public void AtualizarUltimoLogin()
        {
            UltimoLogin = DateTime.Now;
        }

        public bool PodeRealizarAcao() => Ativo && EhMaiorDeIdade();
    }
}