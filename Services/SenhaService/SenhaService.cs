using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ConcasPay.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace ConcasPay.Services.SenhaService;

public class SenhaService : ISenhaInterface
{

    private readonly IConfiguration _config;

    public SenhaService(IConfiguration config)
    {
        _config = config;
    }

    public void GerarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            senhaSalt = hmac.Key;
            senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
        }
    }

    public bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
    {
        using (var hmac = new HMACSHA512(senhaSalt))
        {
            var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

            return ComputeHash.SequenceEqual(senhaHash);
        }
    }

    public string GerarToken(Usuario usuario)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", usuario.Id.ToString()),
            new Claim("Email", usuario.Email),
            new Claim("CPF", usuario.CPF)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: cred
               );


        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
