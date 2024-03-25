using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using jwtRegisterLogin.Models;

namespace ConcasPay.Services.AutenticacaoService;

public interface IAutenticacaoInterface
{
    Task<Response<UsuarioRegistroDto>> Registrar(UsuarioRegistroDto usuarioRegistro);
    Task<Response<string>> Login(UsuarioLoginDto usuarioLogin);
    Usuario? ObterUsuarioPorToken(string token);
    int ObterUsuarioIdPorToken(string token);
}