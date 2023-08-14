using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class FotosRepository : IFotosRepository
    {
        private readonly DataContext _dataContext;

        public FotosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<FotosModels> ListarFotosPorId(int id)
        {
            var fotos = _dataContext.Fotos.Where(x => x.UsuarioId == id).ToList();
            return fotos;
        }
        public string CadastrarFotos(FotosModels foto)
        {
            try
            {
                _dataContext.Fotos.Add(foto);
                _dataContext.SaveChanges();
                return "Ok";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
