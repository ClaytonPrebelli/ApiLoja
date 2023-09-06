using ApiLoja.Models;
using ApiLoja.Responses;

namespace ApiLoja.Repositories.IRepositories
{
    public interface INoticiasRepository
    {
        NoticiasModels CadastrarNoticia(NoticiasModels noticia);
        NoticiasResponse ListarNoticias(int page, int pageSize);
        NoticiasModels VerNoticia(int id);
    }
}
