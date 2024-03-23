
namespace ConcasPay.Services.SenhaService;

public interface ISenhaInterface
{
    void GerarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
}
