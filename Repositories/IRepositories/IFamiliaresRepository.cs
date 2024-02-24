using ApiLoja.Models;
using ApiLoja.Responses;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IFamiliaresRepository
    {
        IEnumerable<FamiliaresModels> VerFamiliaresCandidato(int id);
        List<NiverFamiliaresResponse> VerAniversarioFamilia();
    }
}
