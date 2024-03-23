using ConcasPay.Domain;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using ConcasPay.Services.SenhaService;
using jwtRegisterLogin.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

            response.Mensagem = "Usuário criado com sucesso!";

        }
        catch (Exception ex)
        {
            response.Dados = null;
            response.Mensagem = ex.Message;
            response.Status = false;
        }

        return response;
    }

    public async Task<Response<string>> Login(UsuarioLoginDto usuarioLogin)
    {
        Response<string> respostaServico = new Response<string>();

        try
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLogin.Email);

            if (usuario == null)
            {
                respostaServico.Mensagem = "Credenciais inválidas!";
                respostaServico.Status = false;
                return respostaServico;
            }

            if (!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                respostaServico.Mensagem = "Credenciais inválidas!";
                respostaServico.Status = false;
                return respostaServico;
            }

            var token = _senhaInterface.GerarToken(usuario);

            respostaServico.Dados = token;
            respostaServico.Mensagem = "Usuário logado com sucesso!";


        }
        catch (Exception ex)
        {
            respostaServico.Dados = null;
            respostaServico.Mensagem = ex.Message;
            respostaServico.Status = false;
        }


        return respostaServico;
    }

    public bool VerificaEmailUsuarioExistente(UsuarioRegistroDto usuarioRegistro)
    {
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == usuarioRegistro.Email);

        if (usuario != null) return true;

        return false;
    }
}