using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Repositories
{
    public class NoticiasRepositoy:INoticiasRepository
    {
        private readonly DataContext _dataContext;
        private readonly IFotosRepository _fotosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        public NoticiasRepositoy(DataContext dataContext,IFotosRepository fotosRepository, IUsuariosRepository usuariosRepository)
        {
            _dataContext = dataContext;
            _fotosRepository = fotosRepository;
            _usuariosRepository = usuariosRepository;
        }

        public NoticiasModels CadastrarNoticia(NoticiasModels noticia)
        {
            _dataContext.Noticias.Add(noticia);
            _dataContext.SaveChanges();
            return noticia;
        }

        public NoticiasResponse ListarNoticias(int page, int pageSize)
        {
            if (pageSize==null)
            {
                pageSize = 10;
            }
            var registros = _dataContext.Noticias.Count();
            var noticias = _dataContext.Noticias
                .AsNoTracking()
                .Include(x => x.Autor)
                .Include(x => x.FotosNoticias)
                .OrderByDescending(x => x.DataPublicacao)
                .Skip((page-1)==0?0:(page-1)*pageSize)
                .Take(pageSize)
                .ToList();

            var result = new NoticiasResponse();
            result.TotalPaginas =(registros/pageSize)+1;
            result.Page = page;
            result.Resultados = registros;
            result.Noticias = noticias;
            return result;

        }
        public NoticiasModels VerNoticia(int id)
        {
            var noticia = _dataContext.Noticias
                .AsNoTracking()
                .Include(x=>x.FotosNoticias)
                .Include(x=>x.Autor)
                .FirstOrDefault(x => x.Id == id)
                ;
            if (noticia == null)
            {
                return null;
            }
            else
            {
                return noticia;
            }
        }

    }
}
