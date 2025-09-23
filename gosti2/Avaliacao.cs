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
        [Range(1, 5, ErrorMessage = "A nota deve ser entre 1 e 5 estrelas")] // ✅ CORRIGIDO: INT (1-5)
        public int Nota { get; set; } // ✅ CORRIGIDO: decimal → int

        [MaxLength(1000)] // ✅ CORRIGIDO: Tamanho compatível com SQL (1000)
        public string Comentario { get; set; }

        [Required]
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        // Navegações
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }

        // ✅ MÉTODOS DE NEGÓCIO (OPCIONAIS)
        public string NotaEmEstrelas()
        {
            return new string('⭐', Nota) + new string('☆', 5 - Nota);
        }

        public bool EhAvaliacaoPositiva() => Nota >= 4;

        public bool EhAvaliacaoRecente() => (DateTime.Now - DataAvaliacao).TotalDays < 30;

        public string ResumoAvaliacao()
        {
            return $"{(Comentario?.Length > 50 ? Comentario.Substring(0, 47) + "..." : Comentario)}";
        }

        // ✅ VALIDAÇÃO PERSONALIZADA
        public bool ValidarAvaliacao()
        {
            return Nota >= 1 && Nota <= 5 &&
                   LivroId > 0 &&
                   UsuarioId > 0 &&
                   DataAvaliacao <= DateTime.Now;
        }
    }
}