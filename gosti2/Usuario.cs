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
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)] // ✅ Aumentado para hash de senha
        public string Senha { get; set; }

        [Required]
        [MaxLength(10)]
        public string DataNascimento { get; set; }

        public byte[] FotoPerfil { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        // ✅ CAMPOS PARA BIO PROFISSIONAL (com tamanhos adequados)
        [MaxLength(1000)]
        public string BioProfissional { get; set; }

        [MaxLength(100)]
        public string Especialidade { get; set; }

        [MaxLength(100)]
        public string Twitter { get; set; }

        [MaxLength(100)]
        public string Instagram { get; set; }

        // ✅ NOVOS CAMPOS PARA SISTEMA PROFISSIONAL
        [MaxLength(100)]
        public string Website { get; set; }

        [MaxLength(100)]
        public string Localizacao { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? UltimoLogin { get; set; }
        public bool Ativo { get; set; } = true;

        // ✅ RELAÇÕES COMPLETAS
        public virtual ICollection<Livro> Livros { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Mensagem> MensagensEnviadas { get; set; }
        public virtual ICollection<Mensagem> MensagensRecebidas { get; set; }
        public virtual ICollection<LikeDislike> LikesDislikes { get; set; }
        public virtual ICollection<Avaliacao> Avaliacoes { get; set; } // ✅ ADICIONADO

        public Usuario()
        {
            // ✅ INICIALIZAÇÃO COMPLETA DE TODAS AS COLEÇÕES
            Livros = new HashSet<Livro>();
            Comentarios = new HashSet<Comentario>();
            MensagensEnviadas = new HashSet<Mensagem>();
            MensagensRecebidas = new HashSet<Mensagem>();
            LikesDislikes = new HashSet<LikeDislike>();
            Avaliacoes = new HashSet<Avaliacao>(); // ✅ ADICIONADO
        }
    }
}