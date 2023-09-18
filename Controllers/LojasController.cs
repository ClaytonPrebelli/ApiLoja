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
    public class LojasController: ControllerBase
    {
        public readonly DataContext _dataContext;
        public readonly ILojasRepository _lojas;
        public LojasController(DataContext dataContext, ILojasRepository lojasRepository)
        {
            _dataContext = dataContext;
            _lojas = lojasRepository;
        }
        [HttpGet("VerLojasAtivas")]
        public ActionResult<IEnumerable<LojaModels>> VerLojasAtivas()
        {
            var lojas = _lojas.VerLojasAtivas();
            if (lojas.Any())
            {
                return Ok(lojas);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("VerLojas")]
        public ActionResult<IEnumerable<LojaModels>> VerLojas()
        {
            var lojas = _lojas.VerLojas();
            if (lojas.Any())
            {
                return Ok(lojas);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("CadastrarLoja")]
        public ActionResult<LojaModels> CadastrarLoja ([FromBody] LojaModels loja)
        {
            var result = _lojas.CadastrarLoja(loja);
            if (result!=null)
            {
                return Created("Loja Criada",loja);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
