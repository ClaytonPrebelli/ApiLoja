using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class FotosRepository : IFotosRepository
    {
        private readonly DataContext _dataContext;

        public FotosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

       
        public string CadastrarFotosUser(FotosModels foto)
        {
            try
            {
                _dataContext.Fotos.Add(foto);
                _dataContext.SaveChanges();
                return "Ok";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string CadastrarFotosLojas(FotosLojasModels foto)
        {
            try
            {
                _dataContext.FotosLojas.Add(foto);
                _dataContext.SaveChanges();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string CadastrarFotosNoticias(FotosNoticiaModels foto)
        {
            try
            {
                _dataContext.FotosNoticias.Add(foto);
                _dataContext.SaveChanges();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public ICollection<FotosNoticiaModels> VerFotoNoticia(int id)
        {
            var noticia = _dataContext.FotosNoticias.Where(x=>x.NoticiasId == id).ToList();
            return noticia;
        }
        public ICollection<FotosLojasModels> VerFotoLoja(int id)
        {
            var noticia = _dataContext.FotosLojas.Where(x => x.LojasId == id).ToList();
            return noticia;
        }
        public FotosModels VerFotoUser(int id)
        {
            var foto = _dataContext.Fotos.Where(x => x.Id == id).FirstOrDefault();
            return foto;
        }

    }
}
