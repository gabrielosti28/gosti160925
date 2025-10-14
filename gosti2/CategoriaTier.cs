using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("CategoriaTiers")]
    public class CategoriaTier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaTierId { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(255)]
        public string Descricao { get; set; }

        [Required, Range(1, 10)]
        public int Nivel { get; set; } = 1;

        [MaxLength(20)]
        public string Cor { get; set; } = "#000000";

        // Navegação
        public virtual ICollection<Livro> Livros { get; set; } = new HashSet<Livro>();

        // === MÉTODOS ÚTEIS SIMPLIFICADOS ===

        [NotMapped]
        public string NomeComNivel => $"{Nome} (Nível {Nivel})";

        [NotMapped]
        public string CorSegura => string.IsNullOrWhiteSpace(Cor) ? "#000000" : Cor;

        // Método único para obter informações do nível
        public (string nome, string icone, string cor) ObterInfoNivel()
        {
            switch (Nivel)
            {
                case 1: return ("Iniciante", "🌱", "#4CAF50");
                case 2: return ("Intermediário", "📚", "#2196F3");
                case 3: return ("Avançado", "⭐", "#FF9800");
                case 4: return ("Expert", "🏆", "#F44336");
                default: return ($"Nível {Nivel}", "📊", "#000000");
            }
        }

        // Validação básica
        public bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Nome) &&
                   Nome.Length <= 50 &&
                   Nivel >= 1 && Nivel <= 10 &&
                   (string.IsNullOrEmpty(Cor) || ValidarCor());
        }

        private bool ValidarCor()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Cor,
                "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
        }

        // ToString útil
        public override string ToString() => NomeComNivel;
    }

    public static class CategoriaTierHelper
    {
        public static List<CategoriaTier> ObterCategoriasPadrao()
        {
            return new List<CategoriaTier>
        {
            new CategoriaTier { Nome = "Iniciante", Descricao = "Leitor iniciante", Nivel = 1, Cor = "#4CAF50" },
            new CategoriaTier { Nome = "Intermediário", Descricao = "Leitor frequente", Nivel = 2, Cor = "#2196F3" },
            new CategoriaTier { Nome = "Avançado", Descricao = "Leitor experiente", Nivel = 3, Cor = "#FF9800" },
            new CategoriaTier { Nome = "Expert", Descricao = "Crítico literário", Nivel = 4, Cor = "#F44336" }
        };

        }




    }
}