using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("PontuacoesJogo")]
    public class PontuacaoJogo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PontuacaoId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        public int Pontos { get; set; }

        [Required]
        public int Movimentos { get; set; }

        [Required]
        public int TempoSegundos { get; set; }

        [Required]
        public DateTime DataJogo { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        public string Dificuldade { get; set; } // "Fácil", "Médio", "Difícil"

        // Navegação
        public virtual Usuario Usuario { get; set; }

        // Propriedade calculada - Pontuação formatada
        [NotMapped]
        public string PontuacaoFormatada => $"{Pontos} pts ({Movimentos} movimentos em {TempoSegundos}s)";
    }
}