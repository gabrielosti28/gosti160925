using System;
using System.ComponentModel.DataAnnotations;

namespace gosti2
{
    public class Mensagem
    {
        [Key]
        public int MensagemId { get; set; }

        [Required]
        public string Conteudo { get; set; }

        public DateTime DataEnvio { get; set; } = DateTime.Now;
        public bool Lida { get; set; } = false;

        // Chaves estrangeiras ATUALIZADAS
        public int RemetenteId { get; set; }
        public virtual Usuario Remetente { get; set; }

        public int DestinatarioId { get; set; }
        public virtual Usuario Destinatario { get; set; }
    }
}