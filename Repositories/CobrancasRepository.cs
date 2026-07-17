using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Repositories
{
    public class CobrancasRepository : ICobrancasRepository
    {
        private readonly DataContext _dataContext;

        public CobrancasRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CobrancasModels CadastrarCobranca(CobrancasModels cobranca)
        {
            _dataContext.Cobrancas.Add(cobranca);
            _dataContext.SaveChanges();
            return cobranca;
        }

        public CobrancasModels VerCobranca(int id)
        {
            return _dataContext.Cobrancas.AsNoTracking()
                .Include(x => x.CategoriasCobrancas)
                .Include(x => x.Usuario)
                .FirstOrDefault(x => x.Id == id);
        }

        public CobrancasModels AtualizarCobranca(CobrancasModels cobranca)
        {
            _dataContext.Cobrancas.Update(cobranca);
            _dataContext.SaveChanges();
            return cobranca;
        }

        public bool DeletarCobranca(int id)
        {
            var cobranca = _dataContext.Cobrancas.Find(id);
            if (cobranca == null) return false;

            _dataContext.Cobrancas.Remove(cobranca);
            _dataContext.SaveChanges();
            return true;
        }

        public List<CobrancasModels> ListarPorMembro(int usuarioId)
        {
            return _dataContext.Cobrancas.AsNoTracking()
                .Where(x => x.UsuarioModelsId == usuarioId)
                .Include(x => x.CategoriasCobrancas)
                .OrderByDescending(x => x.Vencimento)
                .ToList();
        }

        public List<CobrancasModels> ListarTodas(int? categoriaId, bool? paga)
        {
            var query = _dataContext.Cobrancas.AsNoTracking()
                .Include(x => x.CategoriasCobrancas)
                .Include(x => x.Usuario)
                .AsQueryable();

            if (categoriaId.HasValue)
                query = query.Where(x => x.CategoriaCobrancaId == categoriaId.Value);
            if (paga.HasValue)
                query = query.Where(x => x.Pago == paga.Value);

            return query.OrderByDescending(x => x.Vencimento).ToList();
        }

        public bool MarcarComoPaga(int id, DateTime dataPagamento)
        {
            var cobranca = _dataContext.Cobrancas.Find(id);
            if (cobranca == null) return false;

            cobranca.Pago = true;
            cobranca.DataPagamento = dataPagamento;
            _dataContext.SaveChanges();
            return true;
        }

        public List<CategoriasCobrancasModels> ListarCategorias()
        {
            return _dataContext.CategoriasCobrancas.AsNoTracking().ToList();
        }
    }
}
