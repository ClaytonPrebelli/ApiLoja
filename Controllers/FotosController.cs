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
    public class FotosController:ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ILojasRepository _lojasRepository;
        private readonly DataContext _dataContext;
        public readonly IFotosRepository _fotosRepository;
        public readonly INoticiasRepository _noticiasRepository;
        public FotosController(
            DataContext dataContext,
            IUsuariosRepository usuariosRepository, 
            IFotosRepository fotosRepository, 
            ILojasRepository lojasRepository,
            INoticiasRepository noticiasRepository)
        {
            _dataContext = dataContext;
            _fotosRepository = fotosRepository;
            _usuariosRepository = usuariosRepository;
            _lojasRepository = lojasRepository;
            _noticiasRepository = noticiasRepository;
        }

        [HttpPost("GravarFotoUser")]
        public async Task<ActionResult<FotosModels>> GravarFotoUser(IFormFile file,[FromQuery] int id)
        {
            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }
            var user = _usuariosRepository.VerUsuario(id);
            var foto = new FotosModels
            {
                FotoFile = bytes,
                FotoName = user.Nome,
                Id=0,
                UsuarioId= user.Id
            };
            _dataContext.Fotos.Add(foto);
            await _dataContext.SaveChangesAsync();
            
            return Ok();
        }
        [HttpPost("GravarFotoLojas")]
        public async Task<ActionResult<FotosLojasModels>> GravarFotoLojas(IFormFile file, [FromQuery] int id)
        {
            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }
            var loja = _lojasRepository.VerLoja(id);
            var foto = new FotosLojasModels
            {
                FotoFile = bytes,
                FotoName = loja.NomeLoja,
                Id=0,
                LojasId = loja.Id
            };
            _dataContext.FotosLojas.Add(foto);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("GravarFotoNoticias")]
        public async Task<ActionResult<FotosNoticiaModels>> GravarFotoNoticias(IFormFile file, [FromQuery] int id)
        {
            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }
            var noticia = _noticiasRepository.VerNoticia(id);
            var foto = new FotosNoticiaModels
            {
                FotoFile = bytes,
                FotoName = noticia.Titulo,
                Id=0,
                NoticiasId = noticia.Id
            };
            _dataContext.FotosNoticias.Add(foto);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
