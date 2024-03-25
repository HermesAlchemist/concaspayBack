namespace ConcasPay.Domain.Dtos;

public class UsuarioDto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public bool Ativo { get; set; }
    public int? EnderecoId { get; set; }
}