using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using concaspayBack.Services.SaqueService;
using ConcasPay.Services.AutenticacaoService;
using concaspayBack.Domain.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace concaspayBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaqueController : ControllerBase
    {
        private readonly ISaqueService _saqueService;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaqueController(ISaqueService saqueService, IAutenticacaoInterface autenticacaoInterface,IHttpContextAccessor httpContextAccessor)
        {
            _saqueService = saqueService;
            _autenticacaoInterface = autenticacaoInterface;
            _httpContextAccessor = httpContextAccessor;
        }
    
        [HttpGet]
        public IActionResult Get()
        {
            // Accessing the logged-in user's identity
            var jwt = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

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
            // Accessing the logged-in user's identity
            var jwt = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var userId = _autenticacaoInterface.ObterUsuarioIdPorToken(jwt);

            SaqueDto saqueDto = new SaqueDto(){
                Uuid = "",
                IdConta = userId,
                Valor = valorSaque.Valor,
                DataSolicitacao = DateTime.Now,
                DataExpiracao = DateTime.Now.AddMinutes(30)
            };
            var novoSaque = _saqueService.CreateSaque(saqueDto);
            return CreatedAtAction(nameof(Get), new { id = novoSaque.Uuid }, novoSaque);
        }
    }
}
