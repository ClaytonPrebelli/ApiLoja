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
    public class StatusController : ControllerBase
    {
        public readonly DataContext _dataContext;
        public readonly IStatusRepository _statusRepository;
        public StatusController(DataContext dataContext, IStatusRepository statusRepository)
        {
            _dataContext = dataContext;
            _statusRepository = statusRepository;
        }
        [HttpPost("CadastrarStatus")]
        public ActionResult<StatusModels> CadastrarStatus([FromBody] StatusModels status)
        {
            var result = _statusRepository.CadastrarStatus(status);
            if (result != null)
            {
                return Ok(status);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
