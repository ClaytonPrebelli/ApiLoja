using ApiLoja.Models;
using ApiLoja.Responses;

namespace ApiLoja.Repositories.IRepositories
{
    public interface ILojasRepository
    {
        LojaModels CadastrarLoja(LojaModels loja);
        LojaModels VerLoja(int id);
        IEnumerable<LojasResponse> VerLojasAtivas();
        IEnumerable<LojaModels> VerLojas();
    }
}
