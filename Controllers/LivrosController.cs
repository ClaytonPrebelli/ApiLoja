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
    public class LivrosController:ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILivrosRepository _livrosRepository;

        public LivrosController(DataContext dataContext, ILivrosRepository livrosRepository)
        {
            _dataContext = dataContext;
            _livrosRepository = livrosRepository;
        }

        [HttpPost("CadastrarLivro")]
        public async Task<ActionResult> CadastrarLivro([FromBody] LivrosModels livro)
        {
            var livroCadastrar = _livrosRepository.CadastrarLivro(livro);
            return Ok(livroCadastrar);
        }
        [HttpGet("VerLivrosAprendiz")]
        public async Task<ActionResult> VerLivrosAprendiz()
        {
            var livros = _livrosRepository.VerLivrosAprendiz();
            return Ok(livros);
        }
        [HttpGet("VerLivrosCompanheiro")]
        public async Task<ActionResult> VerLivrosCompanheiro()
        {
            var livros = _livrosRepository.VerLivrosCompanheiro();
            return Ok(livros);
        }
        [HttpGet("VerLivrosMestre")]
        public async Task<ActionResult> VerLivrosMestre()
        {
            var livros = _livrosRepository.VerLivrosMestre();
            return Ok(livros);
        }
    }
}
