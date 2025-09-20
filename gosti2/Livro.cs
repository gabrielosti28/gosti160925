using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosti2.Models;
using gosti2.Data;
using System.Data.SqlClient;

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

        // Chave estrangeira CORRETA
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // Navegação CORRETA
        public virtual Usuario Usuario { get; set; }

        // Relações
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<LikeDislike> LikesDislikes { get; set; }

        public Livro()
        {
            Comentarios = new HashSet<Comentario>();
            LikesDislikes = new HashSet<LikeDislike>();
        }
    }
}