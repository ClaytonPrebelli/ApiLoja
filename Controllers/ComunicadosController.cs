using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class ComunicadosController : ControllerBase
    {
        private readonly IComunicadosRepository _comunicadosRepository;

        public ComunicadosController(IComunicadosRepository comunicadosRepository)
        {
            _comunicadosRepository = comunicadosRepository;
        }

        [HttpPost("CadastrarComunicado")]
        public ActionResult<ComunicadosModels> CadastrarComunicado([FromBody] ComunicadosModels comunicado)
        {
            var result = _comunicadosRepository.CadastrarComunicado(comunicado);
            return Created("criado", result);
        }

        [HttpGet("VerComunicado")]
        public ActionResult<ComunicadosModels> VerComunicado([FromQuery] int id)
        {
            var comunicado = _comunicadosRepository.VerComunicado(id);
            if (comunicado == null) return NotFound();
            return Ok(comunicado);
        }

        [HttpPut("AtualizarComunicado")]
        public ActionResult<ComunicadosModels> AtualizarComunicado([FromBody] ComunicadosModels comunicado)
        {
            var result = _comunicadosRepository.AtualizarComunicado(comunicado);
            return Ok(result);
        }

        [HttpDelete("DeletarComunicado")]
        public ActionResult DeletarComunicado([FromQuery] int id)
        {
            var deletado = _comunicadosRepository.DeletarComunicado(id);
            if (!deletado) return NotFound();
            return Ok(new { message = "Comunicado deletado com sucesso." });
        }

        [HttpGet("ListarTodos")]
        public ActionResult<List<ComunicadosModels>> ListarTodos()
        {
            var lista = _comunicadosRepository.ListarTodos();
            return Ok(lista);
        }

        [HttpGet("ListarPorGrau")]
        public ActionResult<List<ComunicadosModels>> ListarPorGrau([FromQuery] string grau)
        {
            var lista = _comunicadosRepository.ListarPorGrau(grau);
            return Ok(lista);
        }

        [HttpGet("ListarRecentes")]
        public ActionResult<List<ComunicadosModels>> ListarRecentes([FromQuery] int quantidade = 5)
        {
            var lista = _comunicadosRepository.ListarRecentes(quantidade);
            return Ok(lista);
        }
    }
}
