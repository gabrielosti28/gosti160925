using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2
{
    [Table("Livro")] // Força o nome da tabela
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
        //[MaxLength(50)]
        public int CategoriaId { get; set; }
        //public string Categoria { get; set; }

        [MaxLength(1000)]
        public string Descricao { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] Capa { get; set; }

        [Required]
        public DateTime DataAdicao { get; set; } = DateTime.Now;

        [Required]
        public bool Favorito { get; set; } = false;

        [Required]
        public bool Lido { get; set; } = false;

        // Chave estrangeira
        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}