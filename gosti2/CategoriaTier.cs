using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("CategoriasTier")]
    public class CategoriaTier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaTierId { get; set; }

        [Required]
        [MaxLength(200)]
        public string NomeTier { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // 5 livros organizados por tier (1 = melhor, 5 = pior)
        [ForeignKey("Livro1")]
        public int? LivroId1 { get; set; }

        [ForeignKey("Livro2")]
        public int? LivroId2 { get; set; }

        [ForeignKey("Livro3")]
        public int? LivroId3 { get; set; }

        [ForeignKey("Livro4")]
        public int? LivroId4 { get; set; }

        [ForeignKey("Livro5")]
        public int? LivroId5 { get; set; }

        // Navegações
        public virtual Usuario Usuario { get; set; }
        public virtual Livro Livro1 { get; set; }
        public virtual Livro Livro2 { get; set; }
        public virtual Livro Livro3 { get; set; }
        public virtual Livro Livro4 { get; set; }
        public virtual Livro Livro5 { get; set; }
    }
}