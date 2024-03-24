using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using ConcasPay.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConcasPay.Services.ContaService;

public class ContaService : IContaService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ContaService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<ContaDto> GetAllContas()
    {
        var contas = _dbContext.Contas.ToList();
        return _mapper.Map<List<ContaDto>>(contas);
    }

    public ContaDto GetContaById(int id)
    {
        var conta = _dbContext.Contas.Find(id);
        return _mapper.Map<ContaDto>(conta);
    }

    public ContaDto CreateConta(ContaDto contaDto)
    {
        var conta = _mapper.Map<Conta>(contaDto);
        _dbContext.Contas.Add(conta);
        _dbContext.SaveChanges();
        return _mapper.Map<ContaDto>(conta);
    }

    public ContaDto UpdateConta(ContaDto contaDto)
    {
        var conta = _dbContext.Contas.Find(contaDto.Id);
        if (conta == null)
        {
            return null;
        }

        _mapper.Map(contaDto, conta);
        _dbContext.SaveChanges();
        return _mapper.Map<ContaDto>(conta);
    }

    public void DeleteConta(int id)
    {
        var conta = _dbContext.Contas.Find(id);
        if (conta != null)
        {
            _dbContext.Contas.Remove(conta);
            _dbContext.SaveChanges();
        }
    }
}
