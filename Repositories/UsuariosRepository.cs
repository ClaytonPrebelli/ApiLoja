using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Params;
using ApiLoja.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
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
                .Include(x=>x.Cobrancas)
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
                .Include(x=>x.Status)
                .Include(x=>x.Foto)
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
        public UsuarioModels Login(LoginParams param)
        {
            if(param == null){
                return null;
            }
            else
            {
                param.CPF = param.CPF.Replace(".", "").Replace("-", "");
                var senha = GerarHashMd5(param.Pass);
                param.Pass = senha;
                var usuario = _dataContext.Usuario.Where(x=>x.StatusId == 1 && x.CPF == param.CPF && x.Pass == param.Pass)
                     .Include(x => x.Loja)
                .Include(x => x.Status)
                .Include(x => x.Foto)
                    .FirstOrDefault();
                return usuario;
            }
        }

        public UsuarioModels VerficaAtivo(int id)
        {
            var usuario = _dataContext.Usuario.Where(x=>x.Id==id && x.StatusId==1)
                .Include(x=>x.Loja)
                .Include(x=>x.Status)
                .Include(x=>x.Foto)
                .FirstOrDefault();
            return usuario;

        }
        public UsuarioModels AtualizaUser(UsuarioModels usuario)
        {
            if (usuario == null)
            {
                return null;
            }
            else
            {
                string hash = GerarHashMd5(usuario.Pass);
                usuario.Pass = hash;
                _dataContext.Usuario.Update(usuario);
                _dataContext.SaveChanges();
                return usuario;
            }
        }
        public IEnumerable<StatusModels> VerStatus()
        {
            var status = _dataContext.Status.ToList();
            return status;
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
