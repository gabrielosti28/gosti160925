using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Livros")]
    public class Livro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LivroId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Autor { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genero { get; set; }

        [MaxLength(1000)]
        public string Descricao { get; set; }

        public byte[] Capa { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; }

        public int? AnoPublicacao { get; set; }

        [MaxLength(100)]
        public string Editora { get; set; }

        public int? Paginas { get; set; }

        public DateTime DataAdicao { get; set; } = DateTime.Now;

        public bool Favorito { get; set; }

        public bool Lido { get; set; }

        // Chave estrangeira
        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // Navegações
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

        public Livro()
        {
            Comentarios = new HashSet<Comentario>();
        }
    }
}