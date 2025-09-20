using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosti2.Models;
using gosti2.Data;
using System.Data.SqlClient;

namespace gosti2.Models
{
    [Table("LikesDislikes")]
    public class LikeDislike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeDislikeId { get; set; }

        [Required]
        [ForeignKey("Livro")]
        public int LivroId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required]
        public bool IsLike { get; set; } // true = like, false = dislike

        [Required]
        public DateTime DataAcao { get; set; } = DateTime.Now;

        // Navegações
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}