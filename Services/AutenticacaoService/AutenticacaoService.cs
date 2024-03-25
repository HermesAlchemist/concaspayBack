using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ConcasPay.Domain;
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Enum;
using ConcasPay.Domain.Models;
using ConcasPay.Services.ContaService;
using ConcasPay.Services.SenhaService;
using jwtRegisterLogin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConcasPay.Services.AutenticacaoService;

public class AutenticacaoService : IAutenticacaoInterface
{
    public readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISenhaInterface _senhaInterface;
    public readonly IContaService _contaInterface;
    private readonly IConfiguration _config;

    public AutenticacaoService(AppDbContext context, ISenhaInterface senhaInterface, IContaService contaInterface, IConfiguration config, IMapper mapper)
    {
        _context = context;
        _senhaInterface = senhaInterface;
        _config = config;
        _contaInterface = contaInterface;
        _mapper = mapper;
    }

    public async Task<Response<UsuarioDto>> Registrar(UsuarioRegistroDto usuarioRegistro)
    {
        Response<UsuarioDto> response = new Response<UsuarioDto>();

        try
        {
            if (VerificaEmailUsuarioExistente(usuarioRegistro))
            {
                response.Dados = null;
                response.Mensagem = "Email já cadastrado.";
                response.Status = false;

                return response;
            }

            _senhaInterface.GerarSenhaHash(usuarioRegistro.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            Usuario usuario = new()
            {
                Nome = usuarioRegistro.Nome,
                Email = usuarioRegistro.Email,
                CPF = usuarioRegistro.CPF,
                Telefone = usuarioRegistro.Telefone,
                Ativo = true,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            ContaDto contaDto = new()
            {
                IdUsuario = usuario.Id,
                TipoConta = TipoConta.ContaPoupanca,
            };

            _contaInterface.CreateConta(contaDto);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            response.Dados = usuarioDto;
            response.Mensagem = "Usuário criado com sucesso!";
        }
        catch (Exception ex)
        {
            response.Dados = null;
            response.Mensagem = ex.Message;
            response.Status = false;
        }

        return response;
    }

    public async Task<Response<string>> Login(UsuarioLoginDto usuarioLogin)
    {
        Response<string> respostaServico = new Response<string>();

        try
        {

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBanco => userBanco.Email == usuarioLogin.Email);

            if (usuario == null)
            {
                respostaServico.Mensagem = "Credenciais inválidas!";
                respostaServico.Status = false;
                return respostaServico;
            }

            if (!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                respostaServico.Mensagem = "Credenciais inválidas!";
                respostaServico.Status = false;
                return respostaServico;
            }

            var token = _senhaInterface.GerarToken(usuario);

            respostaServico.Dados = token;
            respostaServico.Mensagem = "Usuário logado com sucesso!";


        }
        catch (Exception ex)
        {
            respostaServico.Dados = null;
            respostaServico.Mensagem = ex.Message;
            respostaServico.Status = false;
        }


        return respostaServico;
    }

    public bool VerificaEmailUsuarioExistente(UsuarioRegistroDto usuarioRegistro)
    {
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == usuarioRegistro.Email);

        if (usuario != null) return true;

        return false;
    }

    public Usuario? ObterUsuarioPorToken(string token)
    {
        var jwtToken = ObterJwtToken(token);

        var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        return _context.Usuarios.FirstOrDefault(u => u.Id == userId);
    }

    public int ObterUsuarioIdPorToken(string token)
    {
        var jwtToken = ObterJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        return 0;
    }

    private JwtSecurityToken ObterJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        return jwtToken;
    }
}