using System.ComponentModel.DataAnnotations;
using ConcasPay.Domain.Enum;

namespace ConcasPay.Domain.Models;

public class Conta
{
    [Key]
    public int IdConta { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public TipoConta TipoConta { get; set; }

    [Required]
    public decimal Saldo { get; set; } = 0;

    [Required]
    [StringLength(4)]
    public string Agencia { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"\d{5}-\d{1}")]
    public string Numero { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Banco { get; set; } = string.Empty;
}
