using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public string CNPJ { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public string Endereco { get; set; }
        
        public string Telefone { get; set; }
    }
}
