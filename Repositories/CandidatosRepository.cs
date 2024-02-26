using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ceTe.DynamicPDF.HtmlConverter;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Text;

namespace ApiLoja.Repositories
{
    public class CandidatosRepository : ICandidatosRepository
    {
        private readonly DataContext _dataContext;
        public CandidatosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CandidatosModels CadastrarCandidato(CandidatosModels candidato)
        {
            try
            {

                _dataContext.Candidatos.Add(candidato);
                _dataContext.SaveChanges();
                return candidato;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public IEnumerable<CandidatosModels> VerCandidatos()
        {
            var candidatos = _dataContext.Candidatos
                .ToList();
            return candidatos;
        }
        public CandidatosModels VerCandidato(int id)
        {
            var candidato = _dataContext.Candidatos.Where(x => x.Id == id)
                .Include(x => x.Familiares)
                .FirstOrDefault();
            return candidato;
        }
        public byte[] MontarFicha(CandidatosModels candidato, int idade)
        {
            var html = new StringBuilder();

            html.Append("<div class='container-fluid ficha'><div class='container ficha--inner'  #htmlData><div class='ficha--inner--head'></div>            <div class='separador col-12' id='separador1'><h2 class='separador--title'>Dados Pessoais</h2></div>");
            html.Append($"<div class='pessoais'><div class='pessoais--info '>                    <div class='pessoais--info--linha'>             <p class='pessoais--info--linha--text nome'><strong>Nome:</strong>{candidato.Nome}</p><p class='pessoais--info--linha--text '><strong>CPF:</strong> {candidato.CPF }</p><p class='pessoais--info--linha--text '><strong>Idade:</strong> {idade} anos</p></div><div class='pessoais--info--linha'><p class='pessoais--info--linha--text '><strong>RG:</strong> {candidato.RG }</p><p class='pessoais--info--linha--text '><strong>Data Exped..</strong> {FormataData(candidato.DataExpedicao) }</p><p class='pessoais--info--linha--text '><strong>Data Nasc.</strong> {FormataData(candidato.Nascimento) }</p></div>");
            html.Append($"<div class='pessoais--info--linha'><p class='pessoais--info--linha--text '><strong>Religião:</strong> {candidato.Religiao}</p><p class='pessoais--info--linha--text '><strong>Acredita em um Ser Supremo:</strong> { TrueParaSim(candidato.AcreditaSerSupremo) }</p><p class='pessoais--info--linha--text '><strong>Estado Civil:</strong> { candidato.EstadoCivil}</p></div>");
            html.Append($"<div class='pessoais--info--linha'>          <p class='pessoais--info--linha--text '><strong>Naturalidade:</strong> {candidato.Naturalidade}/{  candidato.Estado }</p><p class='pessoais--info--linha--text '><strong>Nacionalidade:</strong> {  candidato.Nacionalidade }</p><p class='pessoais--info--linha--text '><strong>Tipo.Sang.:</strong> <span class='sangue'> { ConverteSangue(candidato.TipoSanguineo) }</span></p></div>");
            html.Append($" <div class='pessoais--info--linha'> <p class='pessoais--info--linha--text '><strong>Pai:</strong> {candidato.Pai}</p><p class='pessoais--info--linha--text '><strong>Pai Maçom:</strong> { TrueParaSim(candidato.PaiMacom) }</p><p class='pessoais--info--linha--text '><strong>Mãe:</strong> { candidato.Mae.ToUpper() }</p></div>");
            html.Append($" <div class='pessoais--info--linha'>              <p class='pessoais--info--linha--text '><strong>Profissão:</strong> {candidato.Profissao}</p><p class='pessoais--info--linha--text '><strong>Renda:</strong> {  candidato.Renda }</p><p class='pessoais--info--linha--text '><strong>Família concorda:</strong> {TrueParaSim(candidato.FamiliaConcorda) }</p></div>");
            html.Append($"<div class='pessoais--info--linha'><p class='pessoais--info--linha--text '><strong>Telefone:</strong> {candidato.Fone}</p><p class='pessoais--info--linha--text '><strong>E-mail:</strong> {  candidato.Email}</p></div><div class='pessoais--info--linha'><p class='pessoais--info--linha--text '><strong>Endereço:</strong> {  candidato.Endereco} , { candidato.Numero},  { candidato.Bairro} / CEP: { candidato.CEP }- {  candidato.Cidade }</p><p class='pessoais--info--linha--text '><strong>Tempo Moradia:</strong> { candidato.TempoMoradia }</p></div>");
            html.Append($" <div class='pessoais--info--linha'><p class='pessoais--info--linha--text '><strong>Vícios:</strong> {candidato.Vicios}</p><p class='pessoais--info--linha--text '><strong>Aptidões:</strong> { candidato.Aptidoes }</p></div><div class='pessoais--info--linha'><p class='pessoais--info--linha--text '><strong>Motivos a entrar na Ordem:</strong> { candidato.Motivos }</p></div></div></div>");
            html.Append(" <div class='separador col-12' id='separador3'><h2 class='separador--title'>Familiares</h2>        </div>");
            html.Append($" <div class='pessoais'> <div class='pessoais--familiares'>");
            foreach (var familiar in candidato.Familiares)
            {
                html.Append($" <div class='pessoais--familiares--box'><div class='pessoais--familiares--item'> <p class='pessoais--familiares--item--text parent'><strong>Nome:</strong> {familiar.FamiliarNome} ({familiar.Relacao}) - {VerIdade(familiar.NascimentoFamiliar) } anos</p><p class='pessoais--familiares--item--tex nasc'><strong>Data Nasc.:</strong> {FormataData(familiar.NascimentoFamiliar)  }</p><p class='pessoais--familiares--item--text tel'><strong>Tel:</strong> { familiar.Telefone }</p></div></div>");
            }
            html.Append("  </div></div>");
            html.Append("<style>");
            html.Append("*{margin:0;padding:0;box-sizing:border-box;}html{border:0;padding:0;}body{border:0;padding:0;display:flex;}.ficha{width:100%;border:0;padding: 0;background-image:url(https://prebellisolucoes.com/bg_blue.jpg);background-size: cover;}.ficha--inner{border:1px solid #343e99;  width:100%;   height: 100%;   background-color: #fff;        padding: 0; }");
            html.Append(".ficha--inner--head{background-image: url(https://prebellisolucoes.com/head-fichaCandidato.png);    width:100%;            height: 3cm;            background-size: 100% 100%;}");
            html.Append(".separador{    width: 100%;            background-color: #343e99;    color:#fff;}    .separador--title{        text-align: center;    }.pessoais{    margin-top:0;    padding: 8px 8px;    column-gap: 8px;   display: flex;}");
            html.Append(" .pessoais--familiares{    width: 100%;        border: 1px solid #343e99;}    .pessoais--familiares--box{            display: flex;                flex-direction: column;                border-bottom: 1px solid #000;        color:#000;    }    .pessoais--familiares--item{            display: flex;                justify-content: space-between;                align-items: center;            color:#000;}        .pessoais--familiares--item--text{                padding: 8px 8px 0 8px;                color:#000;        }");
            html.Append(".pessoais--foto{    width: 25%;        }   .pessoais--info{    width:100%;    border: 1px solid #343e99;}    .pessoais--info--linha{ padding:8px 0;           border-top: none;            border-left: none;            border-right: none;            border-bottom: 1px solid #000;        display: flex;            justify-content: space-between;        }        .pessoais--info--linha--text{            padding: 8px 8px 0 8px;            color:#000;            font-size: 10pt;        }");
            html.Append(".nome{    width: 65%;            font-size: 15pt;        }.cim{    width:29%;     font-size: 15pt;}.sangue{    color:#aa0000;    font-weight: 600;}.loja{    font-size: 15pt;}.cargo,.titulo{width: 48%;}.datas{width: 33%;}");
            html.Append(".parent{    width: 59%;            margin-bottom: 0;        }.tel{    width:18%;    margin-bottom: 0;}.nasc{    width:22%;    margin-bottom: 0;    margin-top: 8px;}");
            html.Append("</style>");

            var htmlContent = html.ToString();
            ConversionOptions conversionOptions = new ConversionOptions();
            conversionOptions.BottomMargin = 0;
            conversionOptions.TopMargin = 0;
            conversionOptions.LeftMargin = 0;
            conversionOptions.RightMargin = 0;
            byte[] pdfByteArray = Converter.Convert(htmlContent, null, conversionOptions);

            return pdfByteArray;
        }
        public string TrueParaSim(bool condicao)
        {
            if (condicao)
            {
                return "Sim";
            }
            else
            {
                return "Não";
            }
        }
        public string ConverteSangue(string sangue)
        {
            if (sangue == null || sangue =="" || sangue ==" ")
            {
                return "Não Sabe";
            }
            else
            {
                return sangue;
            }
        }
        public string VerIdade(DateTime nascimento)
        {
            var birthdate = nascimento;
            var today = new DateTime();
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;

            return age.ToString();
        }
        public string FormataData(DateTime data)
        {
            var dataFormatada = data.Day.ToString().PadLeft(2, '0')+"/"+data.Month.ToString().PadLeft(2, '0')+"/"+data.Year.ToString();
            return dataFormatada;
        }
    }
}
