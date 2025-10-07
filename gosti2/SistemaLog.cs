using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gosti2.Models;

public class SistemaLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long LogId { get; set; }

    [Required]
    [StringLength(20)]
    public string Nivel { get; set; }

    [Required]
    public string Mensagem { get; set; }

    public string StackTrace { get; set; }

    public int? UsuarioId { get; set; }

    [StringLength(100)]
    public string Formulario { get; set; }

    [StringLength(200)]
    public string Metodo { get; set; }

    [Required]
    public DateTime DataHora { get; set; } = DateTime.Now;

    [StringLength(200)]
    public string ExceptionType { get; set; }

    public string InnerException { get; set; }

    // Navigation property
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; }
}