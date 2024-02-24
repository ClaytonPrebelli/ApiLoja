using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;

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
        public List<NiverFamiliaresResponse> VerAniversarioFamilia()
        {
            var mesAtual = DateTime.Now.Month;

            var lista = (from f in _dataContext.Familiares
                         join i in _dataContext.Usuario
                         on f.UsuarioId equals i.Id
                         where f.NascimentoFamiliar.Month == mesAtual
                         join l in _dataContext.Lojas
                         on i.LojaId equals l.Id
                         select new NiverFamiliaresResponse
                         {
                             Nome = f.FamiliarNome,
                             Parentesco = f.Relacao,
                             Data = f.NascimentoFamiliar,
                             Irmao = i.Nome,
                             Loja = l.NomeLoja +" "+ l.NumeroLoja
                         }).ToList();
           



            lista.OrderBy(x => x.Data);
            return lista;
        }
      
      
    }
}
