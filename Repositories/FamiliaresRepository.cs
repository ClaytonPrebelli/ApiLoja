using ApiLoja.Data;
using ApiLoja.Models;

namespace ApiLoja.Repositories
{
    public class FamiliaresRepository
    {
        private readonly DataContext _dataContext;
        public  FamiliaresRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

      
    }
}
