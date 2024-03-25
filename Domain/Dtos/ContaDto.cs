using System.ComponentModel.DataAnnotations;
using ConcasPay.Domain.Enum;

namespace ConcasPay.Domain.Dtos
{
    public class ContaDto
    {
        [Required(ErrorMessage = "O campo IdConta é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo IdUsuario é obrigatório")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo TipoConta é obrigatório")]
        public TipoConta TipoConta { get; set; }

        [Required(ErrorMessage = "O campo Saldo é obrigatório")]
        public decimal Saldo { get; set; }

        [Required(ErrorMessage = "O campo Agencia é obrigatório")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "O campo Numero é obrigatório")]
        public string Numero { get; set; } 

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; }
    }
}
