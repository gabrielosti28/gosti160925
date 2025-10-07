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

        public DateTime DataAdicao { get; set; } = DateTime.Now;

        public bool Favorito { get; set; }
        public bool Lido { get; set; }

        // ✅ Campos para expansão futura
        public string ISBN { get; set; }
        public int? AnoPublicacao { get; set; }
        public string Editora { get; set; }
        public int? Paginas { get; set; }
        public decimal? Avaliacao { get; set; }

        // ✅ ADICIONE ESTAS PROPRIEDADES PARA CATEGORIA TIER
        [ForeignKey("CategoriaTier")]
        public int? CategoriaTierId { get; set; } // ✅ ADICIONAR ESTA LINHA

        // Chave estrangeira
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // ✅ NAVEGAÇÕES CORRETAS (INCLUINDO CategoriaTier)
        public virtual Usuario Usuario { get; set; }
        public virtual CategoriaTier CategoriaTier { get; set; } // ✅ ADICIONAR ESTA LINHA
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<LikeDislike> LikesDislikes { get; set; }
        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
       
        public Livro()
        {
            Comentarios = new HashSet<Comentario>();
            LikesDislikes = new HashSet<LikeDislike>();
            Avaliacoes = new HashSet<Avaliacao>();
        }
    }
}