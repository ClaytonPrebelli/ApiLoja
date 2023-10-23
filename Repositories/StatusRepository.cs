using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class StatusRepository:IStatusRepository
    {
        private readonly DataContext _dataContext;

        public StatusRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }

        public StatusModels CadastrarStatus(StatusModels status)
        {
            try
            {
                _dataContext.Status.Add(status);
                _dataContext.SaveChanges();
                return status;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public StatusModels VerStatus(int id)
        {
            var status = _dataContext.Status.FirstOrDefault(x => x.Id == id);
            return status;
        }
    }
}
