using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gosti2.Models
{
    [Table("Mensagens")]
    public class Mensagem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MensagemId { get; set; }

        [Required]
        [ForeignKey("Remetente")]
        public int RemetenteId { get; set; }

        [Required]
        [ForeignKey("Destinatario")]
        public int DestinatarioId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Texto { get; set; }

        [Required]
        public DateTime DataEnvio { get; set; } = DateTime.Now;

        [Required]
        public bool Lida { get; set; } = false;

        // Navegações
        public virtual Usuario Remetente { get; set; }
        public virtual Usuario Destinatario { get; set; }
    }
}