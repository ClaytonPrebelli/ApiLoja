using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface ITokenRepository
    {
        string GerarToken(int id);
        TokenModels ValidarToken(string token);
        TokenModels DesabilitarToken(TokenModels token);
    }
}
