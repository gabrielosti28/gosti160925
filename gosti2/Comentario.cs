using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComentarioId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Texto { get; set; }

        [Required]
        public DateTime DataComentario { get; set; } = DateTime.Now;

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;

        public bool Editado { get; set; } = false;
        public DateTime? DataEdicao { get; set; }

        // Chaves estrangeiras
        [Required]
        [ForeignKey("Livro")]
        public int LivroId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // Navegações
        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }

        // Construtor
        public Comentario() { }

        public Comentario(string texto, int livroId, int usuarioId)
        {
            Texto = texto;
            LivroId = livroId;
            UsuarioId = usuarioId;
            DataComentario = DateTime.Now;
        }

        // === MÉTODOS ESSENCIAIS ===

        // Propriedades calculadas simples
        [NotMapped]
        public int Pontuacao => Likes - Dislikes;

        [NotMapped]
        public string TempoRelativo
        {
            get
            {
                var tempo = DateTime.Now - DataComentario;

                if (tempo.TotalMinutes < 1) return "Agora";
                if (tempo.TotalHours < 1) return $"{(int)tempo.TotalMinutes}m";
                if (tempo.TotalDays < 1) return $"{(int)tempo.TotalHours}h";
                if (tempo.TotalDays < 30) return $"{(int)tempo.TotalDays}d";

                return DataComentario.ToString("dd/MM/yy");
            }
        }

        // Métodos de reação SIMPLES
        public void AdicionarLike() => Likes++;
        public void AdicionarDislike() => Dislikes++;

        // Método de edição SIMPLES
        public bool Editar(string novoTexto)
        {
            if (string.IsNullOrWhiteSpace(novoTexto) || novoTexto.Length > 2000)
                return false;

            Texto = novoTexto.Trim();
            Editado = true;
            DataEdicao = DateTime.Now;
            return true;
        }

        // Validações de permissão SIMPLES
        public bool PodeEditar(int usuarioId)
        {
            return UsuarioId == usuarioId &&
                   (DateTime.Now - DataComentario).TotalMinutes <= 30;
        }

        public bool PodeExcluir(int usuarioId)
        {
            return UsuarioId == usuarioId &&
                   (DateTime.Now - DataComentario).TotalHours <= 1;
        }

        // Validação básica
        public bool Validar()
        {
            return !string.IsNullOrWhiteSpace(Texto) &&
                   Texto.Length >= 5 &&
                   Texto.Length <= 2000 &&
                   LivroId > 0 &&
                   UsuarioId > 0;
        }

        // ToString útil
        public override string ToString()
        {
            var texto = Texto.Length > 50 ? Texto.Substring(0, 47) + "..." : Texto;
            return $"{Usuario?.NomeUsuario ?? "Usuário"}: {texto}";
        }
    }
}