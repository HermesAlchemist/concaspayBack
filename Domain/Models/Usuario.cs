using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Models;

public class Usuario
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string CPF { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public string Telefone { get; set; } = string.Empty;

    public int? EnderecoId { get; set; }
    public virtual Endereco Endereco { get; set; }

}