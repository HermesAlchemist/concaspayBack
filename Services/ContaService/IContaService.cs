using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;

namespace ConcasPay.Services.ContaService;

public interface IContaService
{
    IEnumerable<ContaDto> GetAllContas();
    ContaDto GetContaById(int id);
    ContaDto GetContaByUserId(int id);
    ContaDto CreateConta(ContaDto contaDto);
    ContaDto UpdateConta(ContaDto contaDto);
    void DeleteConta(int id);
}
