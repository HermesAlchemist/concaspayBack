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
    public string Estado { get; set; } = string.Empty;

    [Required]
    public string Cidade { get; set; } = string.Empty;

    [Required]
    public string Bairro { get; set; } = string.Empty;

    [Required]
    public string Logradouro { get; set; } = string.Empty;

    [Required]
    public string Numero { get; set; } = string.Empty;

    public string Complemento { get; set; } = string.Empty;

}
