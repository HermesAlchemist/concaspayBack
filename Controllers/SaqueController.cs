using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using concaspayBack.Services.SaqueService;
using concaspayBack.Services.AutenticacaoService;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace concaspayBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaqueController : ControllerBase
    {
        private readonly ISaqueService _saqueService;
        private readonly IAutenticacaointerface _autenticacaoInterface;
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



            var saques = _saqueService.GetAllSaquesOfAccount(userName);

            return Ok(saques);
        }       
    }
}
