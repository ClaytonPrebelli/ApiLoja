using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ApiLoja.Repositories
{
    public class UsuariosRepository:IUsuariosRepository
    {
        private readonly DataContext _dataContext;
        public UsuariosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public UsuarioModels CadastrarUsuario(UsuarioModels usuario)
        {
            string hash = GerarHashMd5(usuario.Pass);

            usuario.Pass = hash;
            try
            {
                _dataContext.Usuario.Add(usuario);
                _dataContext.SaveChanges();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UsuarioModels VerUsuario(int id)
        {
            var usuario = _dataContext.Usuario
                .Include(x=>x.Loja)
                .Include(x=>x.Status)
                .FirstOrDefault(x => x.Id == id);
            if(usuario == null)
            {
                return null;
            }
            return usuario; 
        }
        public IEnumerable<UsuarioModels> ListarUsuarios()
        {
            var usuario = _dataContext.Usuario
                .Include(x=>x.Loja)
                .ToList();
            if(usuario == null)
            {
                return Enumerable.Empty<UsuarioModels>();

            }
            else
            {
                return usuario;
            }
        }
        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
