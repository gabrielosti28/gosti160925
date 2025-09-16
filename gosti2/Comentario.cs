using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComentarioId { get; set; }

        [Required]
        public string Texto { get; set; }

        [Required]
        public DateTime DataComentario { get; set; } = DateTime.Now;

        // Chave estrangeira para Livro
        [Required]
        [ForeignKey("Livro")]
        public int LivroId { get; set; }
        public virtual Livro Livro { get; set; }

        // Chave estrangeira para Usuario
        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        // Contadores de likes
        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
    }

    public class LikeDislike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeDislikeId { get; set; }

        [Required]
        public int LivroId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public bool IsLike { get; set; } // true = like, false = dislike

        [Required]
        public DateTime DataAcao { get; set; } = DateTime.Now;
    }
}