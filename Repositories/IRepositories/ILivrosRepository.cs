using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface ILivrosRepository
    {
        LivrosModels CadastrarLivro(LivrosModels livro);
        IEnumerable<LivrosModels> VerLivrosAprendiz();
        IEnumerable<LivrosModels> VerLivrosCompanheiro();
        IEnumerable<LivrosModels> VerLivrosMestre();
    }
}
