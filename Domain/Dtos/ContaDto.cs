using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Dtos
{
    public class ContaDto
    {
        [Required(ErrorMessage = "O campo IdConta é obrigatório")]
        public int IdConta { get; set; }

        [Required(ErrorMessage = "O campo IdUsuario é obrigatório")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo TipoConta é obrigatório")]
        public TipoConta TipoConta { get; set; }

        [Required(ErrorMessage = "O campo Saldo é obrigatório")]
        public decimal Saldo { get; set; }
    }
}
