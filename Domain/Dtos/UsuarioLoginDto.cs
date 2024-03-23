using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Dtos;

public class UsuarioLoginDto
{
    [Required(ErrorMessage = "O campo email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatória")]
    [StringLength(100, MinimumLength = 6)]
    public string? Senha { get; set; }
}