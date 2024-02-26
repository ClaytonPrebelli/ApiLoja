using ApiLoja.Models;
using ApiLoja.Params;
using ApiLoja.Responses;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IUsuariosRepository
    {
        UsuarioModels CadastrarUsuario(UsuarioModels usuario);
        int VerUltimoCim();
        UsuarioModels VerUsuario(int id);
        IEnumerable<UsuarioModels> ListarUsuarios();
        UsuarioModels Login(LoginParams param);
        UsuarioModels VerficaAtivo(int id);
        UsuarioModels AtualizaUser(UsuarioModels usuario);
        IEnumerable<StatusModels> VerStatus();
        FamiliaresModels CadastrarFamiliar(FamiliaresModels familiar);
        List<UsuarioModels> VerAniversarios();
        byte[] GerarCarteirinha(UsuarioModels macom, LojaModels loja);


    }
}
