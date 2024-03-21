using AutoMapper;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;

namespace ConcasPay.Domain.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioRegistroDto, Usuario>();
        CreateMap<UsuarioLoginDto, Usuario>();
    }
}