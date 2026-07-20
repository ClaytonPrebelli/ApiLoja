using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class FrequenciaController : ControllerBase
    {
        private readonly IFrequenciaRepository _frequenciaRepository;

        public FrequenciaController(IFrequenciaRepository frequenciaRepository)
        {
            _frequenciaRepository = frequenciaRepository;
        }

        [HttpPost("RegistrarPresenca")]
        public ActionResult<FrequenciaModels> RegistrarPresenca([FromBody] FrequenciaModels frequencia)
        {
            var existente = _frequenciaRepository.VerificarPresenca(frequencia.UsuarioModelsId, frequencia.DataReuniao);
            if (existente != null)
            {
                return BadRequest("Presença já registrada para este membro nesta data.");
            }

            var result = _frequenciaRepository.RegistrarPresenca(frequencia);
            return Created("criado", result);
        }

        [HttpGet("ListarPorData")]
        public ActionResult<List<FrequenciaModels>> ListarPorData([FromQuery] DateTime dataReuniao)
        {
            var lista = _frequenciaRepository.ListarPorData(dataReuniao);
            return Ok(lista);
        }

        [HttpGet("ListarPorMembro")]
        public ActionResult<List<FrequenciaModels>> ListarPorMembro([FromQuery] int usuarioId)
        {
            var lista = _frequenciaRepository.ListarPorMembro(usuarioId);
            return Ok(lista);
        }

        [HttpGet("VerificarPresenca")]
        public ActionResult VerificarPresenca([FromQuery] int usuarioId, [FromQuery] DateTime dataReuniao)
        {
            var registro = _frequenciaRepository.VerificarPresenca(usuarioId, dataReuniao);
            if (registro == null)
            {
                return Ok(new { presente = false });
            }
            return Ok(new { presente = registro.Presente, registro });
        }

        [HttpPut("TogglePresenca")]
        public ActionResult TogglePresenca([FromQuery] int usuarioId, [FromQuery] DateTime dataReuniao)
        {
            var presente = _frequenciaRepository.TogglePresenca(usuarioId, dataReuniao);
            return Ok(new { presente });
        }

        [HttpGet("ListarTodas")]
        public ActionResult<List<FrequenciaModels>> ListarTodas([FromQuery] int? mes, [FromQuery] int? ano)
        {
            var lista = _frequenciaRepository.ListarTodas(mes, ano);
            return Ok(lista);
        }

        [HttpDelete("DeletarPresenca")]
        public ActionResult DeletarPresenca([FromQuery] int id)
        {
            var deletado = _frequenciaRepository.DeletarPresenca(id);
            if (!deletado) return NotFound();
            return Ok(new { message = "Registro removido com sucesso." });
        }

        [HttpPost("SalvarLista")]
        public ActionResult<List<FrequenciaModels>> SalvarLista([FromBody] List<FrequenciaModels> lista)
        {
            var result = _frequenciaRepository.SalvarLista(lista);
            return Ok(result);
        }

        [HttpGet("ListarDatasReuniao")]
        public ActionResult<List<DateTime>> ListarDatasReuniao([FromQuery] int mes, [FromQuery] int ano)
        {
            var datas = _frequenciaRepository.ListarDatasReuniao(mes, ano);
            return Ok(datas);
        }
    }
}
