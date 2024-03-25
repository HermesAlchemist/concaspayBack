using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;

namespace ConcasPay.Domain.Dtos
{
    public class SaqueDto
    {
        public Guid Uuid { get;set; }
        public int IdConta { get;set; }
        public double Valor { get;set; }
        public DateTime DataSolicitacao { get;set; }
        public DateTime DataExpiracao { get;set; }
    }
}