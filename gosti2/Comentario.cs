using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComentarioId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Texto { get; set; }

        [Required]
        public DateTime DataComentario { get; set; } = DateTime.Now;

        public bool Editado { get; set; } = false;

        public DateTime? DataEdicao { get; set; }

        // Chaves estrangeiras
        [Required]
        [ForeignKey("Livro")]
        public int LivroId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // Navegações
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }

        // Propriedade calculada - Tempo relativo
        [NotMapped]
        public string TempoRelativo
        {
            get
            {
                var tempo = DateTime.Now - DataComentario;

                if (tempo.TotalMinutes < 1) return "Agora";
                if (tempo.TotalHours < 1) return $"{(int)tempo.TotalMinutes}m";
                if (tempo.TotalDays < 1) return $"{(int)tempo.TotalHours}h";
                if (tempo.TotalDays < 30) return $"{(int)tempo.TotalDays}d";

                return DataComentario.ToString("dd/MM/yy");
            }
        }
    }
}