using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class CobrancasController : ControllerBase
    {
        private readonly ICobrancasRepository _cobrancasRepository;

        public CobrancasController(ICobrancasRepository cobrancasRepository)
        {
            _cobrancasRepository = cobrancasRepository;
        }

        [HttpPost("CadastrarCobranca")]
        public ActionResult<CobrancasModels> CadastrarCobranca([FromBody] CobrancasModels cobranca)
        {
            var result = _cobrancasRepository.CadastrarCobranca(cobranca);
            return Created("criado", result);
        }

        [HttpGet("VerCobranca")]
        public ActionResult<CobrancasModels> VerCobranca([FromQuery] int id)
        {
            var cobranca = _cobrancasRepository.VerCobranca(id);
            if (cobranca == null) return NotFound();
            return Ok(cobranca);
        }

        [HttpPut("AtualizarCobranca")]
        public ActionResult<CobrancasModels> AtualizarCobranca([FromBody] CobrancasModels cobranca)
        {
            var result = _cobrancasRepository.AtualizarCobranca(cobranca);
            return Ok(result);
        }

        [HttpDelete("DeletarCobranca")]
        public ActionResult DeletarCobranca([FromQuery] int id)
        {
            var deletado = _cobrancasRepository.DeletarCobranca(id);
            if (!deletado) return NotFound();
            return Ok(new { message = "Cobrança deletada com sucesso." });
        }

        [HttpGet("ListarPorMembro")]
        public ActionResult<List<CobrancasModels>> ListarPorMembro([FromQuery] int usuarioId)
        {
            var lista = _cobrancasRepository.ListarPorMembro(usuarioId);
            return Ok(lista);
        }

        [HttpGet("ListarTodas")]
        public ActionResult<List<CobrancasModels>> ListarTodas([FromQuery] int? categoriaId, [FromQuery] bool? paga)
        {
            var lista = _cobrancasRepository.ListarTodas(categoriaId, paga);
            return Ok(lista);
        }

        [HttpPut("MarcarComoPaga")]
        public ActionResult MarcarComoPaga([FromQuery] int id, [FromQuery] DateTime dataPagamento)
        {
            var resultado = _cobrancasRepository.MarcarComoPaga(id, dataPagamento);
            if (!resultado) return NotFound();
            return Ok(new { message = "Cobrança marcada como paga." });
        }

        [HttpGet("ListarCategorias")]
        public ActionResult<List<CategoriasCobrancasModels>> ListarCategorias()
        {
            var categorias = _cobrancasRepository.ListarCategorias();
            return Ok(categorias);
        }
    }
}
