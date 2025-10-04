using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace gosti2.Models
{
    [Table("CategoriaTiers")]
    public class CategoriaTier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaTierId { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [MaxLength(50, ErrorMessage = "O nome não pode ter mais de 50 caracteres")]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; }

        [MaxLength(255, ErrorMessage = "A descrição não pode ter mais de 255 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O nível é obrigatório")]
        [Range(1, 10, ErrorMessage = "O nível deve estar entre 1 e 10")]
        [Display(Name = "Nível")]
        public int Nivel { get; set; } = 1;

        [MaxLength(20, ErrorMessage = "A cor não pode ter mais de 20 caracteres")]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Formato de cor hexadecimal inválido")]
        [Display(Name = "Cor")]
        public string Cor { get; set; } = "#000000";

        // ✅ RELAÇÃO CORRETA (opcional - o EF detecta automaticamente)
        public virtual ICollection<Livro> Livros { get; set; }

        // ✅ CONSTRUTOR PARA INICIALIZAÇÃO
        public CategoriaTier()
        {
            Livros = new HashSet<Livro>();
        }

        // ✅ PROPRIEDADES CALCULADAS (MELHORADAS)
        [NotMapped]
        [Display(Name = "Nome com Nível")]
        public string NomeComNivel => $"{Nome} (Nível {Nivel})";

        [NotMapped]
        [Display(Name = "Cor com Fallback")]
        public string CorSegura => string.IsNullOrWhiteSpace(Cor) ? "#000000" : Cor;

        [NotMapped]
        [Display(Name = "Categoria Ativa")]
        public bool EstaAtiva => Nivel > 0;

        // ✅ MÉTODOS DE NEGÓCIO (MELHORADOS)
        public bool EhNivelMaximo() => Nivel >= 4;

        public bool EhNivelIniciante() => Nivel == 1;

        public bool EhNivelIntermediario() => Nivel == 2;

        public bool EhNivelAvancado() => Nivel == 3;

        public bool EhNivelExpert() => Nivel >= 4;

        public bool PodeSerEditada() => Nivel > 1;

        public bool PodeSerExcluida()
        {
            // ✅ CORREÇÃO: Verifica se há livros associados de forma segura
            return (Livros == null || !Livros.Any()) && Nivel > 1;
        }

        public string ObterDescricaoNivel()
        {
            // ✅ CORREÇÃO: Substituído switch expression por switch tradicional
            switch (Nivel)
            {
                case 1:
                    return "Iniciante";
                case 2:
                    return "Intermediário";
                case 3:
                    return "Avançado";
                case 4:
                    return "Expert";
                default:
                    return $"Nível {Nivel}";
            }
        }

        public string ObterIconeNivel()
        {
            // ✅ CORREÇÃO: Substituído switch expression por switch tradicional
            switch (Nivel)
            {
                case 1:
                    return "🌱";
                case 2:
                    return "📚";
                case 3:
                    return "⭐";
                case 4:
                    return "🏆";
                default:
                    return "📊";
            }
        }

        public string ObterClasseCssBootstrap()
        {
            // ✅ CORREÇÃO: Substituído switch expression por switch tradicional
            switch (Nivel)
            {
                case 1:
                    return "badge bg-success";
                case 2:
                    return "badge bg-info";
                case 3:
                    return "badge bg-warning";
                case 4:
                    return "badge bg-danger";
                default:
                    return "badge bg-secondary";
            }
        }

        // ✅ MÉTODOS DE VALIDAÇÃO
        public bool ValidarCor()
        {
            if (string.IsNullOrWhiteSpace(Cor))
                return true;

            return System.Text.RegularExpressions.Regex.IsMatch(Cor,
                "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");
        }

        public IEnumerable<ValidationResult> Validar()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            // Validação padrão do DataAnnotations
            Validator.TryValidateObject(this, context, results, true);

            // Validações customizadas
            if (!ValidarCor())
            {
                results.Add(new ValidationResult("Formato de cor hexadecimal inválido",
                    new[] { nameof(Cor) }));
            }

            if (Nivel < 1 || Nivel > 10)
            {
                results.Add(new ValidationResult("O nível deve estar entre 1 e 10",
                    new[] { nameof(Nivel) }));
            }

            return results;
        }

        // ✅ MÉTODOS DE FORMATAÇÃO
        public string ToStringCompleto()
        {
            return $"{NomeComNivel} - {Descricao ?? "Sem descrição"}";
        }

        public string ObterEstiloCss()
        {
            return $"color: {CorSegura}; font-weight: bold;";
        }

        // ✅ MÉTODOS ESTÁTICOS ÚTEIS
        public static List<CategoriaTier> ObterCategoriasPadrao()
        {
            return new List<CategoriaTier>
            {
                new CategoriaTier
                {
                    Nome = "Iniciante",
                    Descricao = "Leitor iniciante",
                    Nivel = 1,
                    Cor = "#4CAF50"
                },
                new CategoriaTier
                {
                    Nome = "Intermediário",
                    Descricao = "Leitor frequente",
                    Nivel = 2,
                    Cor = "#2196F3"
                },
                new CategoriaTier
                {
                    Nome = "Avançado",
                    Descricao = "Leitor experiente",
                    Nivel = 3,
                    Cor = "#FF9800"
                },
                new CategoriaTier
                {
                    Nome = "Expert",
                    Descricao = "Crítico literário",
                    Nivel = 4,
                    Cor = "#F44336"
                }
            };
        }

        public static string ObterNomePorNivel(int nivel)
        {
            // ✅ CORREÇÃO: Substituído switch expression por switch tradicional
            switch (nivel)
            {
                case 1:
                    return "Iniciante";
                case 2:
                    return "Intermediário";
                case 3:
                    return "Avançado";
                case 4:
                    return "Expert";
                default:
                    return "Desconhecido";
            }
        }

        public static string ObterCorPadraoPorNivel(int nivel)
        {
            // ✅ CORREÇÃO: Substituído switch expression por switch tradicional
            switch (nivel)
            {
                case 1:
                    return "#4CAF50"; // Verde
                case 2:
                    return "#2196F3"; // Azul
                case 3:
                    return "#FF9800"; // Laranja
                case 4:
                    return "#F44336"; // Vermelho
                default:
                    return "#000000";  // Preto
            }
        }

        // ✅ OVERRIDES ÚTEIS
        public override string ToString()
        {
            return NomeComNivel;
        }

        public override bool Equals(object obj)
        {
            if (obj is CategoriaTier other)
            {
                return CategoriaTierId == other.CategoriaTierId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CategoriaTierId.GetHashCode();
        }

        // ✅ OPERADORES
        public static bool operator ==(CategoriaTier left, CategoriaTier right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(CategoriaTier left, CategoriaTier right)
        {
            return !(left == right);
        }
    }
}