using ApiLoja.Models;
using ApiLoja.Params;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IUsuariosRepository
    {
        UsuarioModels CadastrarUsuario(UsuarioModels usuario);
        UsuarioModels VerUsuario(int id);
        IEnumerable<UsuarioModels> ListarUsuarios();
        UsuarioModels Login(LoginParams param);
        UsuarioModels VerficaAtivo(int id);
        UsuarioModels AtualizaUser(UsuarioModels usuario);
    }
}
