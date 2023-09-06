using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class NoticiasController:ControllerBase
    {
        private readonly INoticiasRepository _noticiasRepository;

        public NoticiasController(INoticiasRepository noticiasRepository)
        {
            _noticiasRepository = noticiasRepository;
        }
        [HttpGet("ListarNoticias")]
        public ActionResult<NoticiasResponse> ListarNoticias([FromQuery] int page, int pageSize)
        {
            var noticias = _noticiasRepository.ListarNoticias(page,pageSize);

            if (noticias == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(noticias);
            }
        }

        [HttpPost("CadastrarNoticia")]
        public ActionResult<NoticiasModels> CadastrarNoticia ([FromBody] NoticiasModels noticia)
        {
            if (noticia == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var result = _noticiasRepository.CadastrarNoticia(noticia);
                    return Ok(result);
                }catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
