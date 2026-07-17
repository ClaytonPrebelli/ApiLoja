using ApiLoja.Models;
using ApiLoja.Params;
using ApiLoja.Responses;
using Canducci.Pagination;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IUsuariosRepository
    {
        UsuarioModels CadastrarUsuario(UsuarioModels usuario);
        int VerUltimoCim();
        UsuarioModels VerUsuario(int id);
        Task<PaginatedRest<UsuarioModels>> ListarUsuarios(int? page, int? status, string? termo);
        UsuarioModels Login(LoginParams param);
        UsuarioModels VerficaAtivo(int id);
        UsuarioModels AtualizaUser(UsuarioModels usuario);
        IEnumerable<StatusModels> VerStatus();
        FamiliaresModels CadastrarFamiliar(FamiliaresModels familiar);
        List<UsuarioModels> VerAniversarios();
        byte[] GerarCarteirinha(UsuarioModels macom);


    }
}
