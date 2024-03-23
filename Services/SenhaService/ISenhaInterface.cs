
using ConcasPay.Domain.Dtos;
using ConcasPay.Domain.Models;

namespace ConcasPay.Services.SenhaService;

public interface ISenhaInterface
{
    void GerarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    bool VerificaSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);
    string GerarToken(Usuario usuario);
}
