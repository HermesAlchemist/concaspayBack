// UsuarioService
using System;
using System.Threading.Tasks;
using ConcasPay.Domain;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;
using Microsoft.EntityFrameworkCore;
using jwtRegisterLogin.Models;
using AutoMapper;

namespace ConcasPay.Services.UsuarioService
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UsuarioDto ObterUsuarioDto(Usuario usuario)
        {
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public async Task<Response<UsuarioDto>> AtivaUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return new Response<UsuarioDto>
                {
                    Status = false,
                    Mensagem = "Usuário não encontrado."
                };
            }

            usuario.Ativo = true;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return new Response<UsuarioDto>
            {
                Dados = usuarioDto,
                Status = true,
                Mensagem = "Usuário atualizado com sucesso."
            };
        }


        public async Task<Response<UsuarioDto>> DesativaUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return new Response<UsuarioDto>
                {
                    Status = false,
                    Mensagem = "Usuário não encontrado."
                };
            }

            usuario.Ativo = false;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return new Response<UsuarioDto>
            {
                Dados = usuarioDto,
                Status = true,
                Mensagem = "Usuário atualizado com sucesso."
            };
        }

        public async Task<Response<UsuarioDto>> AtualizaUsuario(UsuarioDto usuarioDtoEditado)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioDtoEditado.Id);

            if (usuario == null)
            {
                return new Response<UsuarioDto>
                {
                    Status = false,
                    Mensagem = "Usuário não encontrado."
                };
            }

            usuario.Telefone = usuarioDtoEditado.Telefone;
            usuario.EnderecoId = usuarioDtoEditado.EnderecoId;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return new Response<UsuarioDto>
            {
                Dados = usuarioDto,
                Status = true,
                Mensagem = "Usuário atualizado com sucesso."
            };
        }
    }
}
