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
        
        public CandidatosController(DataContext dataContext, ICandidatosRepository candidatos,ITokenRepository tokenRepository)
        {
            _dataContext = dataContext;
            _candidatos = candidatos;
            _tokenRepository = tokenRepository;
        }
        [HttpGet("GerarTokenCandidato")]
        public ActionResult<string> GerarToken(int id)
        {
            var token = _tokenRepository.GerarToken(id);
            return Ok(token);
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
        public async Task<ActionResult<CandidatosModels>> CadastrarCandidatoAsync ([FromQuery]string token, [FromBody] CandidatosModels candidato)
        {
            candidato.RG = candidato.RG.Replace(" ", "").Replace(".", "").Replace("-", "").Replace("/", "");
            var quemIndica =  _tokenRepository.ValidarToken(token).QuemIndica;
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
    }
}
