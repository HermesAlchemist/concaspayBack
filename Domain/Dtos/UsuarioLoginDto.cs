using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Dtos;

public class UsuarioLoginDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string? Senha { get; set; }
}