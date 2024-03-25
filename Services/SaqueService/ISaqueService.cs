using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using concaspayBack.Domain.Dtos;

namespace concaspayBack.Services.SaqueService
{
    public interface ISaqueService
    {
        IEnumerable<SaqueDto> GetAllSaquesOfAccount(int idConta);
        SaqueDto GetSaqueByUuid(Guid uuid);
        SaqueDto CreateSaque(SaqueDto contaDto);
        void DeleteSaque(Guid uuid);
    }
}