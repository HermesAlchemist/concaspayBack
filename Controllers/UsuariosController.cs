using AutoMapper;
using ConcasPay.Domain;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcasPay.Controllers;

[ApiController]
[Route("usuarios)")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UsuariosController(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    private async Task<ActionResult<UsuarioDto>> GetById(int id)
    {
        var usuario = await _appDbContext.Usuarios.FindAsync(id);

        if (usuario == null) return NotFound();

        return Ok(_mapper.Map<UsuarioDto>(usuario));
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Registro(UsuarioRegistroDto usuarioRegistroDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var emailEmUso = await _appDbContext.Usuarios.AnyAsync(usuario => usuario.Email == usuarioRegistroDto.Email);
        if (emailEmUso) return BadRequest("O email fornecido já está cadastrado.");

        var usuario = _mapper.Map<Usuario>(usuarioRegistroDto);
        _appDbContext.Usuarios.Add(usuario);

        try
        {
            await _appDbContext.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar salvar os dados do usuário no banco de dados");
        }

        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, _mapper.Map<UsuarioDto>(usuario));
    }
}
