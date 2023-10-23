using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class FamiliaresRepository:IFamiliaresRepository
    {
        private readonly DataContext _dataContext;
        public  FamiliaresRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<FamiliaresModels> VerFamiliaresCandidato(int id)
        {
            var familiares = _dataContext.Familiares.Where(x=>x.CandidatosModelsId == id).ToList();
            return familiares;
        }

      
    }
}
