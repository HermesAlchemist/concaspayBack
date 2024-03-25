using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Models;

public class Usuario
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string CPF { get; set; } = string.Empty;

    [Required]
    public byte[] SenhaHash { get; set; }

    [Required]
    public byte[] SenhaSalt { get; set; }

    [Required]
    public string Telefone { get; set; } = string.Empty;

    public int? EnderecoId { get; set; }
    public virtual Endereco? Endereco { get; set; }

    public bool Ativo { get; set; }

    public DateTime TokenDataCriacao { get; set; } = DateTime.UtcNow;
}