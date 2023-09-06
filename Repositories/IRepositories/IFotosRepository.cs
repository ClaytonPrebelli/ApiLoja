using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IFotosRepository
    {
      
        string CadastrarFotosUser(FotosModels foto);
        string CadastrarFotosLojas(FotosLojasModels foto);
        ICollection<FotosNoticiaModels> VerFotoNoticia(int id);
        string CadastrarFotosNoticias(FotosNoticiaModels foto);
    }
}
