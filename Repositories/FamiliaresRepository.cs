using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
using Microsoft.EntityFrameworkCore;

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
            var familiares = _dataContext.Familiares.AsNoTracking().Where(x=>x.CandidatosModelsId == id).ToList();
            return familiares;
        }
        public List<NiverFamiliaresResponse> VerAniversarioFamilia()
        {
            var mesAtual = DateTime.Now.Month;

            var lista = (from f in _dataContext.Familiares.AsNoTracking()
                         join i in _dataContext.Usuario.AsNoTracking()
                         on f.UsuarioId equals i.Id
                         where f.NascimentoFamiliar.Month == mesAtual
                         select new NiverFamiliaresResponse
                         {
                             Nome = f.FamiliarNome,
                             Parentesco = f.Relacao,
                             Data = f.NascimentoFamiliar,
                             Irmao = i.Nome,
                             Loja = "Cavaleiros de Salomão 7106"
                         }).ToList();
           



            lista.OrderBy(x => x.Data);
            return lista;
        }
      
      
    }
}
