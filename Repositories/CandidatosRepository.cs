using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class CandidatosRepository: ICandidatosRepository
    {
        private readonly DataContext _dataContext;
        public CandidatosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CandidatosModels CadastrarCandidato(CandidatosModels candidato)
        {
            try
            {
               
                _dataContext.Candidatos.Add(candidato);
                _dataContext.SaveChanges();
                return candidato;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public IEnumerable<CandidatosModels> VerCandidatos()
        {
            var candidatos = _dataContext.Candidatos
                .ToList();
            return candidatos;
        }
        public CandidatosModels VerCandidato(int id)
        {
            var candidato = _dataContext.Candidatos.Where(x=>x.Id == id).FirstOrDefault();
            return candidato;
        }
    }
}
