using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface ILojasRepository
    {
        LojaModels CadastrarLoja(LojaModels loja);
        LojaModels VerLoja(int id);
    }
}
