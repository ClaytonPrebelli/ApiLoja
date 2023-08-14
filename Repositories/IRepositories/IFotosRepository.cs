using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IFotosRepository
    {
        IEnumerable<FotosModels> ListarFotosPorId(int id);
        string CadastrarFotos(FotosModels foto);
    }
}
