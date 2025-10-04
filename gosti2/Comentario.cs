using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosti2.Tools;

namespace gosti2.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComentarioId { get; set; }

        [Required(ErrorMessage = "O texto do comentário é obrigatório")]
        [MaxLength(2000, ErrorMessage = "O comentário não pode ter mais de 2000 caracteres")]
        [MinLength(5, ErrorMessage = "O comentário deve ter pelo menos 5 caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentário")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "A data do comentário é obrigatória")]
        [Display(Name = "Data do Comentário")]
        public DateTime DataComentario { get; set; } = DateTime.Now;

        [Range(0, int.MaxValue, ErrorMessage = "Likes não pode ser negativo")]
        [Display(Name = "Likes")]
        public int Likes { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Dislikes não pode ser negativo")]
        [Display(Name = "Dislikes")]
        public int Dislikes { get; set; } = 0;

        [Display(Name = "Editado")]
        public bool Editado { get; set; } = false;

        [Display(Name = "Data de Edição")]
        public DateTime? DataEdicao { get; set; }

        // ✅ Chaves estrangeiras com validação
        [Required(ErrorMessage = "O livro é obrigatório")]
        [ForeignKey("Livro")]
        [Display(Name = "Livro")]
        public int LivroId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        [ForeignKey("Usuario")]
        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

        // ✅ Navegações
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }

        // ✅ CONSTRUTORES
        public Comentario() { }

        public Comentario(string texto, int livroId, int usuarioId)
        {
            Texto = texto ?? throw new ArgumentNullException(nameof(texto));
            LivroId = livroId;
            UsuarioId = usuarioId;
            DataComentario = DateTime.Now;
        }

        // ✅ PROPRIEDADES CALCULADAS
        [NotMapped]
        [Display(Name = "Pontuação")]
        public int Pontuacao => Likes - Dislikes;

        [NotMapped]
        [Display(Name = "Total de Reações")]
        public int TotalReacoes => Likes + Dislikes;

        [NotMapped]
        [Display(Name = "Taxa de Aprovação")]
        public double TaxaAprovacao => TotalReacoes > 0 ? (double)Likes / TotalReacoes * 100 : 0;

        [NotMapped]
        [Display(Name = "É Popular")]
        public bool EhPopular => Likes >= 10 && TaxaAprovacao >= 70;

        [NotMapped]
        [Display(Name = "É Polêmico")]
        public bool EhPolemico => Likes >= 5 && Dislikes >= 5 && Math.Abs(Likes - Dislikes) <= 3;

        [NotMapped]
        [Display(Name = "É Recente")]
        public bool EhRecente => (DateTime.Now - DataComentario).TotalHours < 24;

        [NotMapped]
        [Display(Name = "Status")]
        public string Status
        {
            get
            {
                if (EhPopular) return "🔥 Popular";
                if (EhPolemico) return "⚡ Polêmico";
                if (EhRecente) return "🆕 Recente";
                return "💬 Normal";
            }
        }

        // ✅ MÉTODOS DE NEGÓCIO - REAÇÕES
        public void AdicionarLike()
        {
            Likes++;
            DiagnosticContext.LogarInfo($"Like adicionado ao comentário {ComentarioId}");
        }

        public void AdicionarDislike()
        {
            Dislikes++;
            DiagnosticContext.LogarInfo($"Dislike adicionado ao comentário {ComentarioId}");
        }

        public void RemoverLike()
        {
            if (Likes > 0)
            {
                Likes--;
                DiagnosticContext.LogarInfo($"Like removido do comentário {ComentarioId}");
            }
        }

        public void RemoverDislike()
        {
            if (Dislikes > 0)
            {
                Dislikes--;
                DiagnosticContext.LogarInfo($"Dislike removido do comentário {ComentarioId}");
            }
        }

        public void AlterarReacao(bool isLike, bool adicionar)
        {
            if (adicionar)
            {
                if (isLike) AdicionarLike();
                else AdicionarDislike();
            }
            else
            {
                if (isLike) RemoverLike();
                else RemoverDislike();
            }
        }

        // ✅ MÉTODOS DE NEGÓCIO - EDIÇÃO
        public bool Editar(string novoTexto)
        {
            if (string.IsNullOrWhiteSpace(novoTexto))
            {
                DiagnosticContext.LogarAviso($"Tentativa de editar comentário {ComentarioId} com texto vazio");
                return false;
            }

            if (novoTexto.Length > 2000)
            {
                DiagnosticContext.LogarAviso($"Tentativa de editar comentário {ComentarioId} com texto muito longo: {novoTexto.Length} caracteres");
                return false;
            }

            Texto = novoTexto.Trim();
            Editado = true;
            DataEdicao = DateTime.Now;

            DiagnosticContext.LogarInfo($"Comentário {ComentarioId} editado com sucesso");
            return true;
        }

        public bool PodeSerEditado(int usuarioId)
        {
            bool mesmoUsuario = UsuarioId == usuarioId;
            bool tempoValido = (DateTime.Now - DataComentario).TotalMinutes <= 30; // 30 minutos para editar

            return mesmoUsuario && tempoValido;
        }

        public bool PodeSerExcluido(int usuarioId, bool ehAdministrador = false)
        {
            bool mesmoUsuario = UsuarioId == usuarioId;
            bool tempoValido = (DateTime.Now - DataComentario).TotalHours <= 1; // 1 hora para excluir

            return mesmoUsuario && tempoValido || ehAdministrador;
        }

        // ✅ MÉTODOS DE VALIDAÇÃO
        public bool Validar()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(this, context, results, true);
        }

        public IEnumerable<ValidationResult> ValidarDetalhado()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            Validator.TryValidateObject(this, context, results, true);

            // Validações customizadas
            if (!string.IsNullOrWhiteSpace(Texto) && Texto.Length < 5)
            {
                results.Add(new ValidationResult("O comentário deve ter pelo menos 5 caracteres",
                    new[] { nameof(Texto) }));
            }

            if (DataEdicao.HasValue && DataEdicao.Value < DataComentario)
            {
                results.Add(new ValidationResult("A data de edição não pode ser anterior à data do comentário",
                    new[] { nameof(DataEdicao) }));
            }

            return results;
        }

        // ✅ MÉTODOS DE FORMATAÇÃO
        public string ObterTextoResumido(int maxLength = 100)
        {
            if (string.IsNullOrEmpty(Texto)) return string.Empty;

            return Texto.Length <= maxLength
                ? Texto
                : Texto.Substring(0, maxLength) + "...";
        }

        public string ObterTempoRelativo()
        {
            var tempoDecorrido = DateTime.Now - DataComentario;

            if (tempoDecorrido.TotalMinutes < 1)
                return "Agora mesmo";
            if (tempoDecorrido.TotalHours < 1)
                return $"{(int)tempoDecorrido.TotalMinutes} min atrás";
            if (tempoDecorrido.TotalDays < 1)
                return $"{(int)tempoDecorrido.TotalHours} h atrás";
            if (tempoDecorrido.TotalDays < 30)
                return $"{(int)tempoDecorrido.TotalDays} dias atrás";

            return DataComentario.ToString("dd/MM/yyyy");
        }

        public string ObterInfoEdicao()
        {
            return Editado && DataEdicao.HasValue
                ? $"Editado em {DataEdicao.Value:dd/MM/yyyy HH:mm}"
                : string.Empty;
        }

        // ✅ MÉTODOS DE RELATÓRIO
        public string ObterRelatorioDesempenho()
        {
            return $"📊 Desempenho: {Likes} 👍 {Dislikes} 👎 ({TaxaAprovacao:0}% aprovação)";
        }

        public string ObterClassificacaoEngajamento()
        {
            if (TotalReacoes == 0) return "Sem engajamento";
            if (EhPopular) return "Alto engajamento positivo";
            if (EhPolemico) return "Engajamento polarizado";
            if (TaxaAprovacao >= 80) return "Bom engajamento";
            if (TaxaAprovacao <= 20) return "Baixo engajamento";
            return "Engajamento moderado";
        }

        // ✅ MÉTODOS DE SEGURANÇA
        public bool ContemTermoInapropriado()
        {
            // Lista básica de termos inapropriados (pode ser expandida)
            var termosInapropriados = new[]
            {
                "palavrão", "xingamento", "ofensa", "ódio", "violência"
            };

            if (string.IsNullOrWhiteSpace(Texto)) return false;

            var textoLower = Texto.ToLowerInvariant();
            foreach (var termo in termosInapropriados)
            {
                if (textoLower.Contains(termo))
                    return true;
            }

            return false;
        }

        public string ObterTextoSanitizado()
        {
            if (string.IsNullOrWhiteSpace(Texto)) return string.Empty;

            // Sanitização básica (pode ser expandida)
            return Texto
                .Replace("<script>", "&lt;script&gt;")
                .Replace("</script>", "&lt;/script&gt;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }

        // ✅ OVERRIDES E OPERADORES
        public override string ToString()
        {
            return $"{Usuario?.NomeUsuario ?? "Usuário"}: {ObterTextoResumido(50)}";
        }

        public override bool Equals(object obj)
        {
            return obj is Comentario comentario && ComentarioId == comentario.ComentarioId;
        }

        public override int GetHashCode()
        {
            return ComentarioId.GetHashCode();
        }

        public static bool operator ==(Comentario left, Comentario right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Comentario left, Comentario right)
        {
            return !(left == right);
        }
    }
}