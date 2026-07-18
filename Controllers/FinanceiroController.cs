using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class FinanceiroController : ControllerBase
    {
        private readonly IFinanceiroRepository _financeiroRepository;

        public FinanceiroController(IFinanceiroRepository financeiroRepository)
        {
            _financeiroRepository = financeiroRepository;
        }

        [HttpPost("CadastrarEntrada")]
        public ActionResult<FinanceiroModels> CadastrarEntrada([FromBody] FinanceiroModels financeiro)
        {
            var result = _financeiroRepository.CadastrarEntrada(financeiro);
            return Created("criado", result);
        }

        [HttpPost("CadastrarSaida")]
        public ActionResult<FinanceiroModels> CadastrarSaida([FromBody] FinanceiroModels financeiro)
        {
            var result = _financeiroRepository.CadastrarSaida(financeiro);
            return Created("criado", result);
        }

        [HttpGet("VerLancamento")]
        public ActionResult<FinanceiroModels> VerLancamento([FromQuery] int id)
        {
            var lancamento = _financeiroRepository.VerLancamento(id);
            if (lancamento == null) return NotFound();
            return Ok(lancamento);
        }

        [HttpPut("AtualizarLancamento")]
        public ActionResult<FinanceiroModels> AtualizarLancamento([FromBody] FinanceiroModels financeiro)
        {
            var result = _financeiroRepository.AtualizarLancamento(financeiro);
            return Ok(result);
        }

        [HttpDelete("DeletarLancamento")]
        public ActionResult DeletarLancamento([FromQuery] int id)
        {
            var deletado = _financeiroRepository.DeletarLancamento(id);
            if (!deletado) return NotFound();
            return Ok(new { message = "Lancamento deletado com sucesso." });
        }

        [HttpGet("ListarEntradas")]
        public ActionResult<List<FinanceiroModels>> ListarEntradas([FromQuery] int? mes, [FromQuery] int? ano, [FromQuery] int? categoriaId)
        {
            var lista = _financeiroRepository.ListarEntradas(mes, ano, categoriaId);
            return Ok(lista);
        }

        [HttpGet("ListarSaidas")]
        public ActionResult<List<FinanceiroModels>> ListarSaidas([FromQuery] int? mes, [FromQuery] int? ano, [FromQuery] int? categoriaId)
        {
            var lista = _financeiroRepository.ListarSaidas(mes, ano, categoriaId);
            return Ok(lista);
        }

        [HttpGet("ListarTodos")]
        public ActionResult<List<FinanceiroModels>> ListarTodos([FromQuery] int? mes, [FromQuery] int? ano, [FromQuery] string? tipo, [FromQuery] bool? pago)
        {
            var lista = _financeiroRepository.ListarTodos(mes, ano, tipo, pago);
            return Ok(lista);
        }

        [HttpGet("ObterSaldo")]
        public ActionResult ObterSaldo([FromQuery] int? mes, [FromQuery] int? ano)
        {
            var saldo = _financeiroRepository.ObterSaldo(mes, ano);
            return Ok(new { saldo });
        }

        [HttpGet("ListarCategorias")]
        public ActionResult<List<CategoriaFinanceiroModels>> ListarCategorias([FromQuery] string? tipo)
        {
            var categorias = _financeiroRepository.ListarCategorias(tipo);
            return Ok(categorias);
        }

        [HttpGet("ListarPorMembro")]
        public ActionResult<List<FinanceiroModels>> ListarPorMembro([FromQuery] int usuarioId)
        {
            var lista = _financeiroRepository.ListarPorMembro(usuarioId);
            return Ok(lista);
        }

        [HttpPut("MarcarComoPago")]
        public ActionResult MarcarComoPago([FromQuery] int id, [FromQuery] DateTime dataPagamento)
        {
            var resultado = _financeiroRepository.MarcarComoPago(id, dataPagamento);
            if (!resultado) return NotFound();
            return Ok(new { message = "Lançamento marcado como pago." });
        }
    }
}
