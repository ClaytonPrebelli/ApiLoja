using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IStatusRepository
    {
        StatusModels CadastrarStatus(StatusModels status);
        StatusModels VerStatus(int id);
    }
}
