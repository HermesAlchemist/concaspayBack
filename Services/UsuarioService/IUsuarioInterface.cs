using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using jwtRegisterLogin.Models;

namespace ConcasPay.Services.UsuarioService;

public interface IUsuarioInterface
{
    Task<Response<UsuarioDto>> AtivaUsuario(int id);
    Task<Response<UsuarioDto>> DesativaUsuario(int id);
    Task<Response<UsuarioDto>> AtualizaUsuario(UsuarioDto usuarioDto);
    UsuarioDto ObterUsuarioDto(Usuario usuario);

}