using ConcasPay.Domain.Dtos;
using ConcasPay.Services.AutenticacaoService;
using ConcasPay.Services.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcasPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IAutenticacaoInterface _autenticacaoService;

        public UsuarioController(IUsuarioInterface usuarioService, IAutenticacaoInterface autenticacaoService)
        {
            _usuarioInterface = usuarioService;
            _autenticacaoService = autenticacaoService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult UsuarioLogado([FromHeader] string token)
        {
            var usuario = _autenticacaoService.ObterUsuarioPorToken(token);
            var usuarioDto = _usuarioInterface.ObterUsuarioDto(usuario);

            return Ok(usuarioDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AtualizaUsuario([FromHeader] string token, [FromBody] UsuarioDto usuarioDto)
        {
            var usuarioId = _autenticacaoService.ObterUsuarioIdPorToken(token);

            if (usuarioId == 0)
            {
                return NotFound();
            }

            if (usuarioId != usuarioDto.Id)
            {
                return BadRequest();
            }

            var response = await _usuarioInterface.AtualizaUsuario(usuarioDto);

            if (!response.Status)
            {
                return BadRequest(response.Mensagem);
            }

            return Ok(response.Dados);
        }

        [Authorize]
        [HttpPut("ativa")]
        public async Task<IActionResult> AtivaUsuario([FromHeader] string token)
        {
            var usuarioId = _autenticacaoService.ObterUsuarioIdPorToken(token);

            if (usuarioId == 0)
            {
                return NotFound();
            }

            var response = await _usuarioInterface.AtivaUsuario(usuarioId);


            if (!response.Status)
            {
                return BadRequest(response.Mensagem);
            }

            return Ok(response.Dados);
        }

        [Authorize]
        [HttpPut("desativa")]
        public async Task<IActionResult> DesativaUsuario([FromHeader] string token)
        {
            var usuarioId = _autenticacaoService.ObterUsuarioIdPorToken(token);

            if (usuarioId == 0)
            {
                return NotFound();
            }

            var response = await _usuarioInterface.DesativaUsuario(usuarioId);


            if (!response.Status)
            {
                return BadRequest(response.Mensagem);
            }

            return Ok(response.Dados);
        }
    }
}
