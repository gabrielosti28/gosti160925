using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeUsuario { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Senha { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime DataNascimento { get; set; }

        [MaxLength(500)]
        public string Bio { get; set; }

        public byte[] FotoPerfil { get; set; }

        [MaxLength(255)]
        public string Website { get; set; }

        [MaxLength(100)]
        public string Localizacao { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? UltimoLogin { get; set; }

        public bool Ativo { get; set; } = true;

        // NOVO: Cor do perfil (armazena o valor RGB como string, ex: "70,130,180")
        [MaxLength(50)]
        public string CorPerfil { get; set; } = "70,130,180"; // Azul padrão

        // Navegações
        public virtual ICollection<Livro> Livros { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

        [InverseProperty("Remetente")]
        public virtual ICollection<Mensagem> MensagensEnviadas { get; set; }

        [InverseProperty("Destinatario")]
        public virtual ICollection<Mensagem> MensagensRecebidas { get; set; }

        public Usuario()
        {
            Livros = new HashSet<Livro>();
            Comentarios = new HashSet<Comentario>();
            MensagensEnviadas = new HashSet<Mensagem>();
            MensagensRecebidas = new HashSet<Mensagem>();
        }

        // Propriedade calculada
        [NotMapped]
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Year;
                if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }
    }
}