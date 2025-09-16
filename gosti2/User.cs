using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace gosti2
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string Senha { get; set; }

        [Required, MaxLength(10)]
        public string DataNascimento { get; set; }

        public byte[] FotoPerfil { get; set; }
        public string Bio { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Relações
        public virtual ICollection<Livro> Livros { get; set; }
        
        public virtual ICollection<Mensagem> Mensagens { get; set; }
    }
}