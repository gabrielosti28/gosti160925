using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gosti2
{
    public class CategoriaTier
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; } // Ex: "Melhores Livros", "Filmes Recomendados", etc.

        public string Descricao { get; set; }

        // Relação muitos-para-muitos com Livros
        public virtual ICollection<Livro> Livros { get; set; }
    }
}