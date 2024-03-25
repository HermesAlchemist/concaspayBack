using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConcasPay.Services.SaqueService;
using ConcasPay.Services.AutenticacaoService;
using ConcasPay.Domain.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ConcasPay.Domain.Models;
using ConcasPay.Services.ContaService;

namespace ConcasPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaqueController : ControllerBase
    {
        private readonly ISaqueService _saqueService;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContaService _contaService;

        public SaqueController(ISaqueService saqueService, IAutenticacaoInterface autenticacaoInterface, IHttpContextAccessor httpContextAccessor, IContaService contaService)
        {
            _saqueService = saqueService;
            _autenticacaoInterface = autenticacaoInterface;
            _httpContextAccessor = httpContextAccessor;
            _contaService = contaService;
        }
    
        [HttpGet]
        public IActionResult Get()
        {
            // Accessing the logged-in user's identity
            var jwt = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

            var userId = _autenticacaoInterface.ObterUsuarioIdPorToken(jwt);

            var saques = _saqueService.GetAllSaquesOfAccount(userId);

            return Ok(saques);
        }
        [HttpGet("{uuid}")]
        public IActionResult Get(Guid uuid)
        {
            var saque = _saqueService.GetSaqueByUuid(uuid);

            return Ok(saque);
        }  
        [HttpPost]
        public IActionResult Post([FromBody] CriarSaqueDto valorSaque)
        {
            try
            {
                // Accessing the logged-in user's identity
                var jwt = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

                var idUser = _autenticacaoInterface.ObterUsuarioIdPorToken(jwt);

                ContaDto conta = _contaService.GetContaByUserId(idUser);
                SaqueDto saqueDto = new()
                {
                    Uuid = Guid.NewGuid(),
                    IdConta = conta.Id,
                    Valor = valorSaque.Valor,
                    DataSolicitacao = DateTime.Now,
                    DataExpiracao = DateTime.Now.AddMinutes(30)
                };
                var novoSaque = _saqueService.CreateSaque(saqueDto);
                return CreatedAtAction(nameof(Get), new { Uuid = novoSaque.Uuid }, novoSaque);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
