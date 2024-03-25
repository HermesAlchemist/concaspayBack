using AutoMapper;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;

namespace ConcasPay.Domain.Profiles;

public class ContaProfile : Profile
{
    public ContaProfile()
    {
        CreateMap<Conta, ContaDto>();
    }
}