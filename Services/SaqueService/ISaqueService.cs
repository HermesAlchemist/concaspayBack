using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcasPay.Domain.Dtos;

namespace ConcasPay.Services.SaqueService
{
    public interface ISaqueService
    {
        IEnumerable<SaqueDto> GetAllSaquesOfAccount(int idConta);
        SaqueDto GetSaqueByUuid(Guid uuid);
        SaqueDto CreateSaque(SaqueDto contaDto);
        void DeleteSaque(Guid uuid);
    }
}