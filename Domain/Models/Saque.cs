using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ConcasPay.Domain.Models;

namespace ConcasPay.Domain.Models
{
    public class Saque
    {
        [Key]
        public Guid Uuid { get; private set; }

        [Required]
        public int IdConta { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime DataSolicitacao { get; private set; }
        
        [Required]
        public DateTime DataExpiracao { get; private set; }
    
        public bool EstaExpirado => DateTime.Now > DataExpiracao;

        public Saque(int idConta, double valor)
        {
            Uuid = Guid.NewGuid();
            IdConta = idConta;
            Valor = valor;
            DataSolicitacao = DateTime.Now;
            DataExpiracao = DateTime.Now.AddMinutes(30);
        }
    }
}