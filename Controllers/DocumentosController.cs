using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Net.Http.Headers;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class DocumentosController: ControllerBase
    {
        private readonly DataContext _dataContext;

        public DocumentosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

    [HttpPost("CadastrarDocumentoUsuario")]
    public async Task<ActionResult> CadastrarDocumentoUsuario(IFormFile arquivo, [FromQuery] int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Documentos", id.ToString());
                var pathAtual =  Directory.GetParent(Directory.GetCurrentDirectory());
                var pathToSave = Path.Combine(pathAtual.ToString()+"/httpdocs",folderName);
                if (Directory.Exists(pathToSave))
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                   Directory.CreateDirectory(pathToSave);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    [HttpGet("VerDocumentosUsuario")]
    public IEnumerable<ArquivosResponse> VerDocumentosUsuario([FromQuery] int id)
        {
            var folderName = Path.Combine("Documentos", id.ToString());
            var pathAtual = Directory.GetParent(Directory.GetCurrentDirectory());
            var pathFiles = Path.Combine(pathAtual.ToString()+"/httpdocs", folderName);
            var arquivos = new List<ArquivosResponse>();
            if (Directory.Exists(pathFiles))
            {
                DirectoryInfo diretorio = new DirectoryInfo(pathFiles);
                //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
                FileInfo[] Arquivos = diretorio.GetFiles("*.*");
              
                var arquivo = new ArquivosResponse();
                //Começamos a listar os arquivos
                for(int i = 0; i<Arquivos.Length; i++) {


                    arquivos.Add(new ArquivosResponse()
                    {
                        Link="https://prebellisolucoes.com/Documentos/"+id+"/"+Arquivos[i].Name,
                        Nome = Arquivos[i].Name
                    });
                }
                return arquivos;
            }
            else
            {
                return arquivos;
            }
        }
       

        
       
    }
}
