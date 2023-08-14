using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class UsuariosController:ControllerBase
    {

        private readonly IUsuariosRepository _usuariosRepository;
        private readonly DataContext _dataContext;
        public UsuariosController(DataContext dataContext, IUsuariosRepository usuariosRepository)
        {
            _dataContext = dataContext;
            _usuariosRepository = usuariosRepository;
        }

        [HttpPost("CadastrarUsuario")]
        public ActionResult<UsuarioModels> CadastrarUsuarioAsync([FromBody] UsuarioModels usuario)
        {
            var result =  _usuariosRepository.CadastrarUsuario(usuario);

            if (result!=null)
            {
                return Created("criado",result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("ListarUsuarios")]
        public ActionResult<IEnumerable<UsuarioModels>> ListarUsuarios()
        {
            var usuarios = _usuariosRepository.ListarUsuarios();
            if (usuarios.Any())
            {
                return Ok(usuarios);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
