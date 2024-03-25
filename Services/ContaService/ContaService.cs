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
    private readonly Random _random;

    public ContaService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _random = new Random();
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
        string numeroConta = GenerateRandomContaNumber();
        string agencia = "1199";

        var conta = new Conta
        {
            IdUsuario = contaDto.IdUsuario,
            TipoConta = contaDto.TipoConta,
            Saldo = 0,
            Agencia = agencia,
            Numero = numeroConta,
            Banco = contaDto.Banco
        };

        _dbContext.Contas.Add(conta);
        _dbContext.SaveChanges();

        return _mapper.Map<ContaDto>(conta);
    }

    private string GenerateRandomContaNumber()
    {
        // Gera 5 dígitos aleatórios para a parte inicial do número de conta
        int randomDigits = _random.Next(10000, 99999);
        // Gera 1 dígito aleatório para a parte final do número de conta
        int randomDigit = _random.Next(0, 9);
        return $"{randomDigits}-{randomDigit}";
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
