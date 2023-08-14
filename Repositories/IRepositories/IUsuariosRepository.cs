using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IUsuariosRepository
    {
        UsuarioModels CadastrarUsuario(UsuarioModels usuario);
        UsuarioModels VerUsuario(int id);
        IEnumerable<UsuarioModels> ListarUsuarios();
    }
}
