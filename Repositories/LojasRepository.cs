using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;

namespace ApiLoja.Repositories
{
    public class LojasRepository : ILojasRepository
    {
        private readonly DataContext _dataContext;
        private readonly IFotosRepository _fotos;
        private readonly IUsuariosRepository _usuariosRepository;

        public LojasRepository(DataContext dataContext, IFotosRepository fotos,IUsuariosRepository usuariosRepository)
        {
            _dataContext = dataContext;
            _fotos = fotos;
            _usuariosRepository = usuariosRepository;
        }
        public LojaModels CadastrarLoja(LojaModels loja)
        {
            try
            {
                _dataContext.Lojas.Add(loja);
                _dataContext.SaveChanges();
                return loja;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LojaModels VerLoja(int id)
        {
            var loja = _dataContext.Lojas.FirstOrDefault(x => x.Id == id && x.Ativa);
            if (loja == null)
            {
                return null;
            }
            else
            {
                //var foto = _fotos.VerFotoLoja(loja.Id).ToList();
                //if (foto.Any())
                //{
                //    loja.Fotos = foto[0];
                //}
                return loja;
            }
        }
        public IEnumerable<LojasResponse> VerLojasAtivas()
        {
            var lojas = _dataContext.Lojas.Where(x=>x.Ativa == true).ToList();
            List<LojasResponse> listaLojas = new List<LojasResponse>();
            foreach(var loja in lojas)
            {
                var dadosLoja = new LojasResponse();
                dadosLoja.Id = loja.Id;
                dadosLoja.Veneravel = loja.Veneravel;
                dadosLoja.Rito = loja.Rito;
                dadosLoja.NomeLoja = loja.NomeLoja;
                dadosLoja.NumeroLoja = loja.NumeroLoja;
                dadosLoja.Ativa = loja.Ativa;
                dadosLoja.DataFundacao = loja.DataFundacao;
                dadosLoja.Documentos = loja.Documentos;
                dadosLoja.Endereco = loja.Endereco;
                dadosLoja.Estado = loja.Estado;
                dadosLoja.Oriente = loja.Oriente;
                dadosLoja.Instagram = loja.Instagram;
                
                //var foto = _fotos.VerFotoLoja(loja.Id).ToList();
                //if (foto.Any())
                //{
                //    dadosLoja.Fotos = foto[0];
                //}
                var veneravel = _usuariosRepository.VerUsuario(loja.Veneravel);
                if (veneravel!=null)
                {
                    dadosLoja.VeneravelNome = veneravel.Nome;
                    dadosLoja.VeneravelTelefone = veneravel.Fone;
                   
                }
                listaLojas.Add(dadosLoja);
            }
            return listaLojas;
        }
        public IEnumerable<LojaModels> VerLojas()
        {
            var lojas = _dataContext.Lojas.ToList();
            //foreach (var loja in lojas)
            //{
            //    var foto = _fotos.VerFotoLoja(loja.Id).ToList();
            //    if (foto.Any())
            //    {
            //        loja.Fotos = foto[0];
            //    }
            //}
            return lojas;
        }
    }
}
