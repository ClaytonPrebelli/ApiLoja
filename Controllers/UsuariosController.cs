using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Params;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
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
        [HttpPost("Login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginParams param)
        {
            if (param==null)
            {
                return BadRequest();
            }
            else
            {
                var usuario = _usuariosRepository.Login(param);
                if (usuario==null)
                {
                    return NotFound();
                }
                else
                {
                    LoginResponse response = new LoginResponse();
                    response.isAdmin = usuario.isAdmin;
                    response.isCandidato = usuario.isCandidato;
                    response.isCompanheiro = usuario.isCompanheiro;
                    response.isMestre = usuario.isMestre;
                    response.isAprendiz = usuario.isAprendiz;
                    response.Nome = usuario.Nome;
                    response.isActive = usuario.StatusId==1 ? true : false;

                    return Ok(response);
                }
            }
        }


    }
}
