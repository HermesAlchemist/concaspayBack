using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConcasPay.Domain;
using concaspayBack.Domain.Dtos;
using concaspayBack.Domain.Models;

namespace concaspayBack.Services.SaqueService
{
    public class SaqueService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly Random _random;

        public SaqueService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _random = new Random();
        }

        public IEnumerable<SaqueDto> GetAllSaquesOfAccount(int idConta)
        {
            // get all 'saques' where idConta is equal to 'idConta'
            var saques = _dbContext.Saques.Where(s => s.IdConta == idConta);
            return _mapper.Map<List<SaqueDto>>(saques);
        }

        public SaqueDto GetSaqueByUuid(Guid uuid)
        {
            var saque = _dbContext.Saques.Find(uuid);
            return _mapper.Map<SaqueDto>(saque);
        }

        public SaqueDto CreateSaque(SaqueDto saqueDto)
        {
            var saque = new Saque(saqueDto.IdConta, saqueDto.Valor);

            _dbContext.Saques.Add(saque);
            _dbContext.SaveChanges();

            return _mapper.Map<SaqueDto>(saque);
        }

        public void DeleteSaque(Guid uuid)
        {
            var saque = _dbContext.Saques.Find(uuid);
            if (saque != null)
            {
                _dbContext.Saques.Remove(saque);
                _dbContext.SaveChanges();
            }
        }
    }
}