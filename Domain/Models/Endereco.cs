using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Cep { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Estado { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Cidade { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Bairro { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Logradouro { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Numero { get; set; } = string.Empty;

    [StringLength(100)]
    public string Complemento { get; set; } = string.Empty;
}
