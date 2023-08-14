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
        private readonly DataContext _dataContext;
        public readonly IFotosRepository _fotosRepository;
        public FotosController(DataContext dataContext,IUsuariosRepository usuariosRepository, IFotosRepository fotosRepository)
        {
            _dataContext = dataContext;
            _fotosRepository = fotosRepository;
            _usuariosRepository = usuariosRepository;
        }

        [HttpPost("GravarFoto")]
        public async Task<ActionResult<FotosModels>> GravarFoto(IFormFile file,[FromQuery] int id)
        {
            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }
            var usuario = _usuariosRepository.VerUsuario(id);
            var foto = new FotosModels
            {
                FotoFile = bytes,
                FotoName = usuario.Nome,
                Id=0,
                UsuarioId = id
            };
            _dataContext.Fotos.Add(foto);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
