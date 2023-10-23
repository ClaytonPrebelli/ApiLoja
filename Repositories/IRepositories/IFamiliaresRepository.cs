using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IFamiliaresRepository
    {
        IEnumerable<FamiliaresModels> VerFamiliaresCandidato(int id);
    }
}
