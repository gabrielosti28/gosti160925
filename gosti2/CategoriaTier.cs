using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models  // ✅ CORRIGIDO: Namespace correto
{
   
    
    
    
    [Table("CategoriaTiers")]  // ✅ CORRIGIDO: Nome da tabela no plural
    public class CategoriaTier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaTierId { get; set; }  // ✅ CORRIGIDO: CategoriaId → CategoriaTierId

        [Required]
        [MaxLength(50)]  // ✅ CORRIGIDO: Tamanho compatível com SQL (50, não 100)
        public string Nome { get; set; }

        [MaxLength(255)]  // ✅ CORRIGIDO: Tamanho compatível com SQL
        public string Descricao { get; set; }

        [Required]
        public int Nivel { get; set; } = 1;  // ✅ NOVO: Campo obrigatório do SQL

        [MaxLength(20)]
        public string Cor { get; set; } = "#000000";  // ✅ NOVO: Campo do SQL

        // ✅ RELAÇÃO CORRETA (opcional - o EF detecta automaticamente)
        public virtual ICollection<Livro> Livros { get; set; }

        // ✅ CONSTRUTOR PARA INICIALIZAÇÃO
        public CategoriaTier()
        {
            Livros = new HashSet<Livro>();
        }

        // ✅ MÉTODOS DE NEGÓCIO (OPCIONAIS)
        public string NomeComNivel => $"{Nome} (Nível {Nivel})";

        public bool EhNivelMaximo() => Nivel >= 4; // Considerando 4 como nível máximo

        public string ObterCorOuPadrao() => string.IsNullOrEmpty(Cor) ? "#000000" : Cor;

        public bool PodeSerEditada() => Nivel > 1; // Exemplo: níveis > 1 podem ser editados
    
        
    
    }
}