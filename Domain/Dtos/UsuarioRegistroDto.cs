using System.ComponentModel.DataAnnotations;

namespace ConcasPay.Domain.Dtos;

public class UsuarioRegistroDto
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo cpf é obrigatório")]
    [StringLength(11, MinimumLength = 11)]
    public string CPF { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo senha é obrigatório")]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;

    [Required]
    [Compare("Senha", ErrorMessage = "Senhas não coincidem!")]
    public string? ConfirmaSenha { get; set; }

    [Required(ErrorMessage = "O campo telefone é obrigatório")]
    public string Telefone { get; set; } = string.Empty;
}