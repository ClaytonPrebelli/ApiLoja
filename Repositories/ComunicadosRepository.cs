using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Repositories
{
    public class ComunicadosRepository : IComunicadosRepository
    {
        private readonly DataContext _dataContext;

        public ComunicadosRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ComunicadosModels CadastrarComunicado(ComunicadosModels comunicado)
        {
            _dataContext.Comunicados.Add(comunicado);
            _dataContext.SaveChanges();
            return comunicado;
        }

        public ComunicadosModels VerComunicado(int id)
        {
            return _dataContext.Comunicados.AsNoTracking()
                .Include(x => x.Autor)
                .Include(x => x.FotosComunicados)
                .FirstOrDefault(x => x.Id == id);
        }

        public ComunicadosModels AtualizarComunicado(ComunicadosModels comunicado)
        {
            _dataContext.Comunicados.Update(comunicado);
            _dataContext.SaveChanges();
            return comunicado;
        }

        public bool DeletarComunicado(int id)
        {
            var comunicado = _dataContext.Comunicados.Find(id);
            if (comunicado == null) return false;

            var fotos = _dataContext.ComunicadosFotos.Where(f => f.ComunicadoId == id).ToList();
            _dataContext.ComunicadosFotos.RemoveRange(fotos);
            _dataContext.Comunicados.Remove(comunicado);
            _dataContext.SaveChanges();
            return true;
        }

        public List<ComunicadosModels> ListarTodos()
        {
            return _dataContext.Comunicados.AsNoTracking()
                .Include(x => x.Autor)
                .OrderByDescending(x => x.DataPublicacao)
                .ToList();
        }

        public List<ComunicadosModels> ListarPorGrau(string grau)
        {
            return _dataContext.Comunicados.AsNoTracking()
                .Include(x => x.Autor)
                .Where(x => x.Graus.Contains(grau))
                .OrderByDescending(x => x.DataPublicacao)
                .ToList();
        }

        public List<ComunicadosModels> ListarRecentes(int quantidade)
        {
            return _dataContext.Comunicados.AsNoTracking()
                .Include(x => x.Autor)
                .OrderByDescending(x => x.DataPublicacao)
                .Take(quantidade)
                .ToList();
        }
    }
}
