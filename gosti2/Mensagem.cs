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

        [Required(ErrorMessage = "O conteúdo da mensagem é obrigatório")]
        [MaxLength(2000, ErrorMessage = "A mensagem não pode ter mais de 2000 caracteres")]
        [Column("Texto")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "Remetente é obrigatório")]
        [ForeignKey("Remetente")]
        public int RemetenteId { get; set; }

        [Required(ErrorMessage = "Destinatário é obrigatório")]
        [ForeignKey("Destinatario")]
        public int DestinatarioId { get; set; }

        [Required]
        public DateTime DataEnvio { get; set; } = DateTime.Now;

        [Required]
        public bool Lida { get; set; } = false;

        // ✅ NAVEGAÇÕES (LAZY LOADING)
        public virtual Usuario Remetente { get; set; }
        public virtual Usuario Destinatario { get; set; }

        // ✅ MÉTODOS DE NEGÓCIO (OPCIONAIS)
        public bool EhRecente() => (DateTime.Now - DataEnvio).TotalHours < 24;

        public bool PodeSerEditada() => (DateTime.Now - DataEnvio).TotalMinutes < 5;

        public void MarcarComoLida()
        {
            Lida = true;
        }

        public string ObterStatus() => Lida ? "📖 Lida" : "📨 Não lida";

        public bool EnvolveUsuario(int usuarioId)
            => RemetenteId == usuarioId || DestinatarioId == usuarioId;
    }
}