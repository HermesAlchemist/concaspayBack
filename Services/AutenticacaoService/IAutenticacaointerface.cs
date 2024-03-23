using ConcasPay.Domain.Dtos;
using jwtRegisterLogin.Models;

namespace ConcasPay.Services.AutenticacaoService;

public interface IAutenticacaoInterface
{
    Task<Response<UsuarioRegistroDto>> Registrar(UsuarioRegistroDto usuarioRegistro);
    Task<Response<string>> Login(UsuarioLoginDto usuarioLogin);
}