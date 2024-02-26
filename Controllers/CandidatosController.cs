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
    public class CandidatosController : ControllerBase
    {

        private readonly ICandidatosRepository _candidatos;
        private readonly DataContext _dataContext;
        private readonly ITokenRepository _tokenRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IFamiliaresRepository _familias;

        public CandidatosController(DataContext dataContext, ICandidatosRepository candidatos, ITokenRepository tokenRepository, IStatusRepository statusRepository, IUsuariosRepository usuariosRepository, IFamiliaresRepository familiaresRepository)
        {
            _dataContext = dataContext;
            _candidatos = candidatos;
            _tokenRepository = tokenRepository;
            _statusRepository = statusRepository;
            _usuariosRepository = usuariosRepository;
            _familias = familiaresRepository;
        }
        [HttpGet("GerarTokenCandidato")]
        public ActionResult<string> GerarToken(int id)
        {
            var token = _tokenRepository.GerarToken(id);
            return Created(token, token);
        }
        [HttpGet("ValidarToken")]
        public ActionResult<TokenModels> ValidarToken([FromQuery] string token)
        {
            var result = _tokenRepository.ValidarToken(token);
            if (result!=null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("CadastrarCandidato")]
        public async Task<ActionResult<CandidatosModels>> CadastrarCandidatoAsync([FromQuery] string token, [FromBody] CandidatosModels candidato)
        {
            candidato.RG = candidato.RG.Replace(" ", "").Replace(".", "").Replace("-", "").Replace("/", "");
            var quemIndica = _tokenRepository.ValidarToken(token).QuemIndica;
            candidato.QuemIndica = quemIndica;
            var result = _candidatos.CadastrarCandidato(candidato);
            var resultToken = _tokenRepository.ValidarToken(token);
            if (resultToken!=null)
            {
                _tokenRepository.DesabilitarToken(resultToken);
            }
            if (result!=null)
            {
                return Created("Criado", result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("VerCandidatos")]
        public ActionResult<IEnumerable<CandidatosModels>> VerCandidatos()
        {
            var result = _candidatos.VerCandidatos();
            if (result!=null)
            {
                foreach (var candidato in result)
                {
                    candidato.Status = _statusRepository.VerStatus(candidato.StatusId);
                    candidato.Familiares = _familias.VerFamiliaresCandidato(candidato.Id).ToList();
                }
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("VerCandidato")]
        public ActionResult<CandidatosModels> VerCandidato(int id)
        {
            var result = _candidatos.VerCandidato(id);
            if (result!=null)
            {
                result.Status = _statusRepository.VerStatus(result.StatusId);
                result.Familiares = _familias.VerFamiliaresCandidato(result.Id).ToList();
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("FichaCandidato")]
        public ActionResult<byte[]> FichaCandidato([FromQuery] int id, [FromQuery] int idade)
        {
            var result = _candidatos.VerCandidato(id);

            result.Status = _statusRepository.VerStatus(result.StatusId);
            result.Familiares = _familias.VerFamiliaresCandidato(result.Id).ToList();
            var file = _candidatos.MontarFicha(result, idade);

            return Ok(file);

        }
    }
}
