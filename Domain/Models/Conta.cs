using System.ComponentModel.DataAnnotations;

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
    public decimal Saldo { get; set; }

    [Required]
    public int Agencia { get; set; }

    [Required]
    public int Numero { get; set; }

    [Required]
    [StringLength(50)]
    public string Banco { get; set; }
}

public enum TipoConta
{
    ContaCorrente,
    ContaPoupanca
}
