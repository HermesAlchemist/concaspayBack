using ConcasPay.Domain.Dtos;
using ConcasPay.Services.ContaService;
using Microsoft.AspNetCore.Mvc;

namespace concaspayBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }

        // GET: api/Conta
        [HttpGet]
        public IActionResult Get()
        {
            var contas = _contaService.GetAllContas();
            return Ok(contas);
        }

        // GET: api/Conta/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var conta = _contaService.GetContaById(id);
            if (conta == null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        // POST: api/Conta
        [HttpPost]
        public IActionResult Post([FromBody] ContaDto contaDto)
        {
            var novaConta = _contaService.CreateConta(contaDto);
            return CreatedAtAction(nameof(Get), new { id = novaConta.Id }, novaConta);
        }

        // PUT: api/Conta/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContaDto contaDto)
        {
            if (id != contaDto.Id)
            {
                return BadRequest();
            }

            var conta = _contaService.UpdateConta(contaDto);
            if (conta == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Conta/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contaService.DeleteConta(id);
            return NoContent();
        }
    }
}
