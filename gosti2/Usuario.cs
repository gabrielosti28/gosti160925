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
        [MaxLength(100)]
        public string Senha { get; set; }

        [Required]
        [MaxLength(10)]
        public string DataNascimento { get; set; }

        public byte[] FotoPerfil { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Relações CORRETAS
        public virtual ICollection<Livro> Livros { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Mensagem> MensagensEnviadas { get; set; }
        public virtual ICollection<Mensagem> MensagensRecebidas { get; set; }
        public virtual ICollection<LikeDislike> LikesDislikes { get; set; }

        public Usuario()
        {
            Livros = new HashSet<Livro>();
            Comentarios = new HashSet<Comentario>();
            MensagensEnviadas = new HashSet<Mensagem>();
            MensagensRecebidas = new HashSet<Mensagem>();
            LikesDislikes = new HashSet<LikeDislike>();
        }
    }
}