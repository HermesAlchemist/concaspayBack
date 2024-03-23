using ConcasPay.Domain;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using ConcasPay.Services.SenhaService;
using jwtRegisterLogin.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConcasPay.Services.AutenticacaoService;

public class AutenticacaoService : IAutenticacaoInterface
{
    public readonly AppDbContext _context;
    private readonly ISenhaInterface _senhaInterface;

    public AutenticacaoService(AppDbContext context, ISenhaInterface senhaInterface)
    {
        _context = context;
        _senhaInterface = senhaInterface;
    }

    public async Task<Response<UsuarioRegistroDto>> Registrar(UsuarioRegistroDto usuarioRegistro)
    {
        Response<UsuarioRegistroDto> response = new Response<UsuarioRegistroDto>();

        try
        {
            if (VerificaEmailUsuarioExistente(usuarioRegistro))
            {
                response.Dados = null;
                response.Mensagem = "Email já cadastrado.";
                response.Status = false;

                return response;
            }

            _senhaInterface.GerarSenhaHash(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            Usuario usuario = new()
            {
                Nome = usuarioRegistro.Nome,
                Email = usuarioRegistro.Email,
                CPF = usuarioRegistro.CPF,
                Telefone = usuarioRegistro.Telefone,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            response.Mensagem = "Usuário criado com sucesso.";

        }
        catch (Exception ex)
        {
            response.Dados = null;
            response.Mensagem = ex.Message;
            response.Status = false;
        }

        return response;
    }

    public bool VerificaEmailUsuarioExistente(UsuarioRegistroDto usuarioRegistro)
    {
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == usuarioRegistro.Email);

        if (usuario != null) return true;

        return false;
    }
}