using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class LojasRepository : ILojasRepository
    {
        private readonly DataContext _dataContext;

        public LojasRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public LojaModels CadastrarLoja(LojaModels loja)
        {
            try
            {
                _dataContext.Lojas.Add(loja);
                _dataContext.SaveChanges();
                return loja;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LojaModels VerLoja(int id)
        {
            var loja = _dataContext.Lojas.FirstOrDefault(x => x.Id == id && x.Ativa);
            if (loja == null)
            {
                return null;
            }
            else
            {
                return loja;
            }
        }
    }
}
