using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Params;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;
using ApiLoja.Helpers;

namespace ApiLoja.Controllers
{
    [Controller]
    [Route("[controller]")]
    [EnableCors("Politica")]
    public class UsuariosController:ControllerBase
    {

        private readonly IUsuariosRepository _usuariosRepository;
        private readonly DataContext _dataContext;
        private readonly ExportHtml _export;
        public UsuariosController(DataContext dataContext, IUsuariosRepository usuariosRepository,ExportHtml exportHtml)
        {
            _dataContext = dataContext;
            _usuariosRepository = usuariosRepository;
            _export = exportHtml;
        }

        [HttpPost("CadastrarUsuario")]
        public ActionResult<UsuarioModels> CadastrarUsuarioAsync([FromBody] UsuarioModels usuario)
        {
           
            var result =  _usuariosRepository.CadastrarUsuario(usuario);

            if (result!=null)
            {
                return Created("criado",result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("ListarUsuarios")]
        public ActionResult<IEnumerable<UsuarioModels>> ListarUsuarios()
        {
            var usuarios = _usuariosRepository.ListarUsuarios();
            if (usuarios.Any())
            {
                return Ok(usuarios);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("Login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginParams param)
        {
            if (param==null)
            {
                return BadRequest();
            }
            else
            {
                param.CPF = param.CPF.Replace(" ", "").Replace(".", "").Replace("/", "").Replace("-", "");
                var usuario = _usuariosRepository.Login(param);
                if (usuario==null)
                {
                    return NotFound();
                }
                else
                {
                    LoginResponse response = new LoginResponse();
                    response.isAdmin = usuario.isAdmin;
                    response.isCandidato = usuario.isCandidato;
                    response.isCompanheiro = usuario.isCompanheiro;
                    response.isMestre = usuario.isMestre;
                    response.isAprendiz = usuario.isAprendiz;
                    response.Nome = usuario.Nome;
                    response.isActive = usuario.StatusId==1 ? true : false;
                    response.Id = usuario.Id;
                    response.LojaId = usuario.LojaId;
                    response.Titulo = usuario.Titulo;
                    response.Foto = usuario.Foto!=null ? usuario.Foto.FotoFile :null;
                    return Ok(response);
                }
            }
        }
        [HttpGet("VerStatus")]
        public async Task<ActionResult> VerStatus()
        {
            var status = _usuariosRepository.VerStatus();
            if (status == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(status);
            }
        }
        [HttpGet("VerUsuario")]
        public async Task<ActionResult> VerUsuario([FromQuery]int id)
        {
            var usuario = _usuariosRepository.VerUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
               
                return Ok(usuario);
            }
        }
        [HttpGet("VerificaAtivo")]
        public ActionResult<LoginResponse> ConfereAtivo([FromQuery] int id)
        {
            var usuario = _usuariosRepository.VerficaAtivo(id);
            if (usuario==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(usuario);
            }
        }
       
        [HttpPost("EnviarIntencao")]
        public async Task<ActionResult> EnviarIntencao([FromBody] IntencaoParams form)
        {
            //var message = $"<div class='geral' style='margin: 0;padding: 0;width:100%;display: flex;flex-direction: column;align-items: center;row-gap: 16px;		background: rgb(203, 242, 249);		background: radial-gradient(circle, rgba(203, 242, 249, 1) 0%, rgba(127, 159, 235, 1) 100%);		padding: 24px 0; '><img src='https://glumbsp.vercel.app/assets/images/glumbsp_logo.png' alt='' class='logo' style='width: 20%;'><h1 class='title' style='width:80%;text-align: center;'>Você recebeu uma intenção de contato</h1><div class='info' style='border: 2px solid #242fc9;border-radius: 8px;padding: 16px;width: 70%;'><h4 class='linha' style='font-size:18px;'><strong>Nome:</strong> {form.Nome}</h4>		<h4 class='linha' style='font-size:18px;'><strong>Email:</strong> {form.Email}</h4><h4 class='linha' style='font-size:18px;'><strong>Whatsapp:</strong> <a href='https://api.whatsapp.com/send?phone=55{form.Telefone}&text=Ol%C3%A1 {form.Nome}.%20Estou%20retornando%20atrav%C3%A9s%20do%20seu%20contato%20pelo%20site%20Est%C3%A1cio%20Tatui,%20tudo%20bem?' target='_blank'>{form.Telefone}</a></h4><h4 class='linha' style='font-size:18px;'><strong>Cidade Próxima:</strong> {form.Cidade}</h4></div></div>";
            var message = $"<div class='geral' style='margin: 0;width:100%;display: flex;flex-direction: column;align-items: center;row-gap: 16px;		background: rgb(203, 242, 249);		background: radial-gradient(circle, rgba(203, 242, 249, 1) 0%, rgba(127, 159, 235, 1) 100%);		padding: 8px 0; '><div class='info' style='border: 2px solid #242fc9;border-radius: 8px;padding: 16px;width: 100%; margin:0 auto;'><h4 class='linha' style='font-size:18px;'><strong>Nome:</strong> {form.Nome}</h4>		<h4 class='linha' style='font-size:18px;'><strong>Email:</strong> {form.Email}</h4><h4 class='linha' style='font-size:18px;'><strong>Whatsapp:</strong> <a href='https://api.whatsapp.com/send?phone=55{form.Telefone}&text=Ol%C3%A1 {form.Nome}.%20Estou%20retornando%20atrav%C3%A9s%20do%20seu%20contato%20pelo%20site%20Est%C3%A1cio%20Tatui,%20tudo%20bem?' target='_blank'>{form.Telefone}</a></h4><h4 class='linha' style='font-size:18px;'><strong>Cidade Próxima:</strong> {form.Cidade}</h4></div></div>";
            var corpo = new BodyBuilder
            {
                HtmlBody = message
            };
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(MailboxAddress.Parse("clayton@prebellisolucoes.com"));
            mimeMessage.To.Add(MailboxAddress.Parse("prebelli.lider@gmail.com"));
            mimeMessage.Bcc.Add(MailboxAddress.Parse("clayton@prebellisolucoes.com"));
            // mimeMessage.To.Add(MailboxAddress.Parse("clayton@prebellisolucoes.com"));
            mimeMessage.Subject = "CONTATO PELO SITE GLUMBSP.";
            mimeMessage.Body = corpo.ToMessageBody();

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.SslProtocols = System.Security.Authentication.SslProtocols.None;
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await smtpClient.ConnectAsync("smtp.kinghost.net", 587, MailKit.Security.SecureSocketOptions.None);
                    await smtpClient.AuthenticateAsync(new SaslMechanismLogin("clayton@prebellisolucoes.com", "Lilith@1708"));
                    await smtpClient.SendAsync(mimeMessage);
                    await smtpClient.DisconnectAsync(true);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
