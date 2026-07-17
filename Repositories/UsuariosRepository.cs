using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Params;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
using Canducci.Pagination;
using ceTe.DynamicPDF.HtmlConverter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;


namespace ApiLoja.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly DataContext _dataContext;
        private readonly IFotosRepository _fotosRepository;
        private readonly IFamiliaresRepository _familiaresRepository;
        public UsuariosRepository(DataContext dataContext, IFotosRepository fotosRepository, IFamiliaresRepository familiaresRepository)
        {
            _dataContext = dataContext;
            _fotosRepository = fotosRepository;
            _familiaresRepository = familiaresRepository;
        }
        public int VerUltimoCim()
        {
            var ultimoUser = _dataContext.Usuario.OrderByDescending(x => x.CIM).FirstOrDefault();
            var ultimoCim = 0;
            if (ultimoUser != null)
            {
                var cim = int.Parse(ultimoUser.CIM!=null ? ultimoUser.CIM.ToString() : "150");
                ultimoCim = cim;
            }
            return ultimoCim;
        }
        public UsuarioModels CadastrarUsuario(UsuarioModels usuario)
        {
            string hash = GerarHashMd5(usuario.Pass);

            usuario.Pass = hash;
            try
            {
                _dataContext.Usuario.Add(usuario);
                _dataContext.SaveChanges();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public UsuarioModels VerUsuario(int id)
        {
            var usuario = _dataContext.Usuario.AsNoTracking()
                .Include(x => x.Status)
                .Include(x => x.Cargo)
                .Include(x => x.Familiares)
                .FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }
        public async Task<PaginatedRest<UsuarioModels>> ListarUsuarios(int? page, int? status,string? termo)
        {
            page ??= 1;
            if (status==0)
            {
                status = null;
            }

            if (page <= 0) page = 1;
            Task<PaginatedRest<UsuarioModels>> usuario = null;
            if (status!=null && (string.IsNullOrEmpty(termo) || termo == "undefined"))
            {
                usuario = _dataContext.Usuario.AsNoTracking()
                     .Where(x => x.StatusId == status)
                   .Include(x => x.Status)
                   .Include(x => x.Cargo)
                    .OrderBy(x => x.Id)
                   .ToPaginatedRestAsync(page.Value, 60);
            }
            else if (status !=null && (!string.IsNullOrEmpty(termo) &&   termo != "undefined"))
            {
                usuario =  _dataContext.Usuario.AsNoTracking()
                   .Where(x => x.StatusId == status && x.Nome.Contains(termo))
                 .Include(x => x.Status)
                 .Include(x => x.Cargo)
                  .OrderBy(x => x.Id)
                 .ToPaginatedRestAsync(page.Value, 60);
            }else if(status ==null && (!string.IsNullOrEmpty(termo)  && termo != "undefined"))
            {
                usuario =  _dataContext.Usuario.AsNoTracking()
                   .Where(x =>  x.Nome.Contains(termo))
                 .Include(x => x.Status)
                 .Include(x => x.Cargo)
                  .OrderBy(x => x.Id)
                 .ToPaginatedRestAsync(page.Value, 60);
            }
            else
            {
                usuario =  _dataContext.Usuario.AsNoTracking()
                 .Include(x => x.Status)
                 .Include(x => x.Cargo)
                  .OrderBy(x => x.Id)
                 .ToPaginatedRestAsync(page.Value, 60);
            }


            if (usuario == null)
            {
                return null;

            }
            else
            {
                return await usuario;
            }
        }
        public UsuarioModels Login(LoginParams param)
        {
            if (param == null)
            {
                return null;
            }
            else
            {
                param.CPF = param.CPF.Replace(".", "").Replace("-", "");
                var senha = GerarHashMd5(param.Pass);
                param.Pass = senha;
                var usuario = _dataContext.Usuario.AsNoTracking().Where(x => x.StatusId == 1 && x.CPF == param.CPF && x.Pass == param.Pass)
                    .FirstOrDefault();
                return usuario;
            }
        }

        public UsuarioModels VerficaAtivo(int id)
        {
            var usuario = _dataContext.Usuario.AsNoTracking().Where(x => x.Id==id && x.StatusId==1)
                .Include(x => x.Status)
                .Include(x => x.Cargo)
                .FirstOrDefault();
            return usuario;

        }
        public UsuarioModels AtualizaUser(UsuarioModels usuario)
        {
            if (usuario == null)
            {
                return null;
            }
            else
            {
                string hash = usuario.Pass;
                usuario.Pass = hash;
                _dataContext.Usuario.Update(usuario);
                _dataContext.SaveChanges();
                return usuario;
            }
        }
        public IEnumerable<StatusModels> VerStatus()
        {
            var status = _dataContext.Status.ToList();
            return status;
        }

        public FamiliaresModels CadastrarFamiliar(FamiliaresModels familiar)
        {
            _dataContext.Familiares.Add(familiar);
            _dataContext.SaveChanges();
            return familiar;
        }
        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        public List<UsuarioModels> VerAniversarios()
        {
            var mesAtual = DateTime.Now.Month;

            var lista = _dataContext.Usuario.AsNoTracking().Where(x => x.StatusId == 1 && x.Nascimento.Month == mesAtual)
                                             .Include(x => x.Status)
                                             .Include(x => x.Cargo)
                                             .ToList();


            lista.OrderBy(x => x.Nascimento);
            return lista;
        }

        public byte[] GerarCarteirinha(UsuarioModels macom)
        {
            var html = new StringBuilder();
            html.Append(" <div class='box-carteirinha'>     <div class='carteirinha--frente'>");
            html.Append($"<img src='https://portalcavaleirosdesalomao.claytonprebelli.com.br/assets/images/logos/salomao.png' alt='' class='carteirinha--frente--glumb'>           <div lass='carteirinha--frente--head'>                                <div class='carteirinha--frente--head--cim'>                                    <h4 class='carteirinha--frente--head--cim--title'>CARTEIRA DE IDENTIDADE MAÇÔNICA</h4>                                    <h4 class='carteirinha--frente--head--cim--numero'>{macom.CIM.ToString().PadLeft(6,'0')}</h4></div><div class='carteirinha--frente--head--nome'><h4 class='carteirinha--frente--head--nome--title'>{ macom.Nome.ToString() }</h4><h4 class='carteirinha--frente--head--nome--grau'>{ConverteGrau(macom)}</h4></div></div>");
            html.Append($" <img src='https://portalcavaleirosdesalomao.claytonprebelli.com.br/assets/images/logos/salomao.png' alt='' class='carteirinha--frente--loja'>");
            html.Append($" <div class='carteirinha--frente--corpo'>                        <div class='carteirinha--frente--corpo--box'>                                   <div class='carteirinha--frente--corpo--box--infos'>                                   <h5 class='carteirinha--frente--corpo--box--infos--item'><strong>Oriente:</strong> São Paulo</h5><h5 class='carteirinha--frente--corpo--box--infos--item'><strong>Loja:</strong> Cavaleiros de Salomão nº 7106</h5><h5 class='carteirinha--frente--corpo--box--infos--item'><strong>Obediência:</strong> GLUMBSP</h5></div><img src='https://prebellisolucoes.com/FotosUsers/{macom.Id}.png' alt='' class='carteirinha--frente--corpo--box--foto'></div></div>");
            html.Append($" <div class='carteirinha--frente--footer'>                        <div class='carteirinha--frente--footer--inner'>                              <div class='carteirinha--frente--footer--inner--item'>                                <h5 class='carteirinha--frente--footer--inner--item--text'><strong>Emissão</strong></h5>                          <h5 class='carteirinha--frente--footer--inner--item--text'><strong>{MesEAno( DateTime.Now)}</strong></h5></div><div class='carteirinha--frente--footer--inner--item'><h5 class='carteirinha--frente--footer--inner--item--text'><strong>Validade</strong></h5><h5 class='carteirinha--frente--footer--inner--item--text'><strong>{ MesEAno(DateTime.Now.AddYears(1)) }</strong></h5></div></div></div></div>");
            html.Append("<div class='carteirinha--tras'>           <h3 class='carteirinha--tras--title'>GRANDE LOJA UNIDA MAÇÔNICA DO BRASIL-SP</h3>                            <h4 class='carteirinha--tras--text'>Esta carteira de identidade maçônica constitui documento oficial da Grande Loja Unida Maçônica do Brasil-SP, certificando regularidade de obreiro.</h4>");
            html.Append($"<div class='carteirinha--tras--graus'>                                <div class='carteirinha--tras--graus--item'>                                    <h4 class='carteirinha--tras--graus--item--text'>Iniciação</h4>                                    <h4 class='carteirinha--tras--graus--item--text'>{ FormataData(macom.Iniciacao.ToString())}</h4></div>");
            html.Append($"<div class='carteirinha--tras--graus--item'>                                    <h4 class='carteirinha--tras--graus--item--text'>Elevação</h4>                                    <h4 class='carteirinha--tras--graus--item--text'>{FormataData(macom.Elevacao.ToString()) }</h4></div>");
            html.Append($"<div class='carteirinha--tras--graus--item'>                                    <h4 class='carteirinha--tras--graus--item--text'>Exaltação</h4>                                    <h4 class='carteirinha--tras--graus--item--text'>{FormataData(macom.Exaltacao.ToString())}</h4></div>");
            html.Append("  </div>  <div class='carteirinha--tras--ass'> <img src = 'https://prebellisolucoes.com/ser.png' alt='' class='carteirinha--tras--ass--img'>                            </div>                        </div></div>");
            html.Append("<style> *{ margin:0;        padding: 0;            box-sizing: border-box; ");
            html.Append(".box-carteirinha{    width: 100%;        display: flex;            align-items: center;            justify-content: center;            flex-wrap: wrap;        }");
            html.Append(" .carteirinha--frente{        width: 8.5cm;        height: 5.5cm;            border-radius: 16px;        border: 2px solid black;            background-image: url('https://prebellisolucoes.com/bg-frente.png');            background-size: cover;        padding: 0.2cm 0.2cm;        overflow: hidden;        display: flex;            justify-content: space-between;            align-items: space-between;            row-gap: 10px;            flex-wrap: wrap;        }        .carteirinha--frente--glumb{            width:1.5cm;            height: 1.7cm;        }");
            html.Append(" .carteirinha--frente--loja{            width: 1.7cm;        height: 1.7cm;        }        .carteirinha--frente--head{            width:4.5cm;            display: flex;            flex-direction: column;            align-items: center;            row-gap: 8px;}            .carteirinha--frente--head--cim{                width: 100%;                padding: 0.2cm;                background-color: #f5f5f5;                border-radius: 4px;                box-shadow: 2px 2px 6px 2px #0000006b;                display: flex;                flex-direction: column;                row-gap: 16px;}");
            html.Append(" .carteirinha--frente--head--cim--title{                    font-size: 8px;            font-weight: 600;            line-height: 24px;            font-family: Roboto, sans-serif;            letter-spacing: .03125em;        margin: 0;            line-height: 0;            text-align: center;        }                .carteirinha--frente--head--cim--numero{                    font-size: 14px;                    font-weight: 600;                    line-height: 24px;                    font-family: Roboto, sans-serif;                    letter-spacing: .03125em;                    margin: 0;                    line-height: 0;                    text-align: center;                    color: #4e9ac2;    text-shadow: 1px 1px #000000e0;                }");
            html.Append(" .carteirinha--frente--head--nome{                width: 100%;        padding: 0.2cm;            background-color: #f5f5f5;                border-radius: 4px;            box-shadow: 2px 2px 6px 2px #0000006b;                display: flex;            flex-direction: column;            row-gap: 8px;        }                .carteirinha--frente--head--nome--title{                    font-size: 8px;                    font-weight: 600;                    line-height: 24px;                    font-family: Roboto, sans-serif;                    letter-spacing: .03125em;                    margin: 0;                    line-height: 0;                    text-align: center;                    text-transform: uppercase; }");
            html.Append("                .carteirinha--frente--head--nome--grau{                font-size: 8px;                font-weight: 600;                line-height: 24px;                font-family: Roboto, sans-serif;                letter-spacing: .03125em;            margin: 0;                line-height: 0;                text-align: center;                font-style: italic;}  ");
            html.Append("   .carteirinha--frente--corpo{       width: 100%;       display: flex; justify-content: center;  }.carteirinha--frente--corpo--box{    width: 5cm;   background-color: #f5f5f5;  border-radius: 4px;  box-shadow: 2px 2px 6px 2px #0000006b;  margin-left: -7px; display: flex;padding:3px; margin-top:-15px;}.carteirinha--frente--corpo--box--infos{width: 70%;display: flex;flex-direction: column;justify-content: space-between;}.carteirinha--frente--corpo--box--infos--item{ padding: 0.07cm;border-bottom: 1px solid #000;  line-height: 1;margin-bottom: 0;font-weight:500; }.carteirinha--frente--corpo--box--foto{   width: 30%; border-radius: 4px;box-shadow: 2px 2px 6px 2px #0000006b; } ");
            html.Append(".carteirinha--frente--footer{ width: 100%;  display: flex; justify-content: center;}.carteirinha--frente--footer--inner{ width:5cm;display: flex;  justify-content: space-between;}.carteirinha--frente--footer--inner--item{ width:40%; padding: 0.1cm;                    background-color: #f5f5f5;                 border-radius: 4px;               box-shadow: 2px 2px 6px 2px #0000006b;       display: flex;                    flex-direction: column;}     .carteirinha--frente--footer--inner--item--text{    text-align: center;    line-height: 1;margin: 0;}");
            html.Append(".carteirinha--tras{        width: 8.5cm;        height: 5.5cm;            border-radius: 16px;        border: 2px solid black;            background-image: url('https://prebellisolucoes.com/bg-tras.png');            background-size: cover;        padding: 0.5cm 0.2cm;        overflow: hidden;        display: flex;            justify-content: space-between;            align-items: space-between;            row-gap: 10px;            flex-wrap: wrap;  }");
            html.Append(".carteirinha--tras--title{width: 100%;     color:#fff;        font-size: 13px;    margin: 0;     font-weight: 600; margin-top:-14px;            text-align: center;        }        .carteirinha--tras--text{            width: 100%;            color:#fff;            font-size: 13px;            margin:0;            line-height: 1;            text-align: center;        }        .carteirinha--tras--graus{            width: 100%;            display: flex;            justify-content: space-between;            height: fit-content;}");
            html.Append(".carteirinha--tras--graus--item{                width: 32%;        display: flex;            flex-direction: column;        padding: 0.1cm;            background-color: #f5f5f5;                border-radius: 4px;            box-shadow: 2px 2px 6px 2px #0000006b;                height: fit-content;       }                .carteirinha--tras--graus--item--text{                 text-align: center; line-height: 1;                    margin: 0;                    font-weight: 600;}       .carteirinha--tras--ass{            width: 100%;            display: flex;            justify-content: center;            height: fit-content;}            .carteirinha--tras--ass--img{width: 30%;}");



            html.Append("</style>");
            var htmlContent = html.ToString();
           
            ConversionOptions conversionOptions = new ConversionOptions(PageSize.A4,PageOrientation.Landscape);
            
            byte[] pdfByteArray = Converter.Convert(htmlContent, null, conversionOptions);

            return pdfByteArray;

        }
        public string ConverteGrau(UsuarioModels macom)
        {
            if (macom.isMestre)
            {
                return "Mestre Maçom";
  }
            else if (macom.isCompanheiro)
            {
                return "Companheiro de Ofício";
           }
            else
            {
                return "Aprendiz Admitido";
           }
        }
        public string MesEAno(DateTime data)
        {
            return data.Month.ToString().PadLeft(2, '0')+"/"+data.Year.ToString();
        }
        public string FormataData(string dataRecebida )
        {
            if (dataRecebida == null || dataRecebida =="")
            {
                return "XXXX";
            }
            else
            {

                var dataFormatada = (dataRecebida.Split(' ')[0]);
                return dataFormatada;
            }
        }
        public Image ByteToImage(byte[] arquivo)
        {
            using (MemoryStream mStream = new MemoryStream(arquivo))
            {
                return Image.FromStream(mStream);
            }
        }

    }
}
