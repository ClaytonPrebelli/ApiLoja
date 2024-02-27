using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class LivrosRepository:ILivrosRepository
    {
        private readonly DataContext _dataContext;
        public LivrosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public LivrosModels CadastrarLivro(LivrosModels livro)
        {
            try
            {

                _dataContext.Livros.Add(livro);
                _dataContext.SaveChanges();
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public IEnumerable<LivrosModels> VerLivrosAprendiz()
        {
            var livros = _dataContext.Livros.Where(x => x.isAprendiz).ToList();
            return livros;
        }
        public IEnumerable<LivrosModels> VerLivrosCompanheiro()
        {
            var livros = _dataContext.Livros.Where(x => x.isCompanheiro).ToList();
            return livros;
        }
        public IEnumerable<LivrosModels> VerLivrosMestre()
        {
            var livros = _dataContext.Livros.Where(x => x.isMestre).ToList();
            return livros;
        }
    }
}
