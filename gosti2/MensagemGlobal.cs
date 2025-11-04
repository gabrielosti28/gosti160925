using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("MensagensGlobais")]
    public class MensagemGlobal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MensagemGlobalId { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [MaxLength(2000)]
        public string TextoMensagem { get; set; }

        public byte[] ImagemMensagem { get; set; }

        [Required]
        public DateTime DataPostagem { get; set; } = DateTime.Now;

        public bool Editada { get; set; } = false;

        public DateTime? DataEdicao { get; set; }

        // Navegação
        public virtual Usuario Usuario { get; set; }

        // Propriedade calculada - Tempo relativo
        [NotMapped]
        public string TempoRelativo
        {
            get
            {
                var tempo = DateTime.Now - DataPostagem;

                if (tempo.TotalMinutes < 1) return "Agora";
                if (tempo.TotalMinutes < 60) return $"{(int)tempo.TotalMinutes}m";
                if (tempo.TotalHours < 24) return $"{(int)tempo.TotalHours}h";
                if (tempo.TotalDays < 30) return $"{(int)tempo.TotalDays}d";

                return DataPostagem.ToString("dd/MM/yy");
            }
        }
    }
}