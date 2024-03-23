using ConcasPay.Domain.Dtos;
using ConcasPay.Services.AutenticacaoService;
using Microsoft.AspNetCore.Mvc;

namespace jwtRegisterLogin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public AutenticacaoController(IAutenticacaoInterface autenticacaoInterface)
        {
            _autenticacaoInterface = autenticacaoInterface;
        }

        [HttpPost("/registrar")]
        public async Task<ActionResult> Registrar(UsuarioRegistroDto usuarioRegistro)
        {
            var resposta = await _autenticacaoInterface.Registrar(usuarioRegistro);
            return Ok(resposta);
        }

    }
}