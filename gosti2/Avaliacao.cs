using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Avaliacoes")]
    public class Avaliacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvaliacaoId { get; set; }

        [Required]
        [ForeignKey("Livro")]
        public int LivroId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        [Range(0.0, 5.0)]
        public decimal Nota { get; set; }

        [MaxLength(500)]
        public string Comentario { get; set; }

        [Required]
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        // Navegações
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}