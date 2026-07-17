using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Repositories
{
    public class FinanceiroRepository : IFinanceiroRepository
    {
        private readonly DataContext _dataContext;

        public FinanceiroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public FinanceiroModels CadastrarEntrada(FinanceiroModels financeiro)
        {
            financeiro.Tipo = "Entrada";
            _dataContext.Financeiro.Add(financeiro);
            _dataContext.SaveChanges();
            return financeiro;
        }

        public FinanceiroModels CadastrarSaida(FinanceiroModels financeiro)
        {
            financeiro.Tipo = "Saida";
            _dataContext.Financeiro.Add(financeiro);
            _dataContext.SaveChanges();
            return financeiro;
        }

        public FinanceiroModels VerLancamento(int id)
        {
            return _dataContext.Financeiro.AsNoTracking()
                .Include(x => x.CategoriaFinanceiro)
                .Include(x => x.Usuario)
                .FirstOrDefault(x => x.Id == id);
        }

        public FinanceiroModels AtualizarLancamento(FinanceiroModels financeiro)
        {
            _dataContext.Financeiro.Update(financeiro);
            _dataContext.SaveChanges();
            return financeiro;
        }

        public bool DeletarLancamento(int id)
        {
            var lancamento = _dataContext.Financeiro.Find(id);
            if (lancamento == null) return false;

            _dataContext.Financeiro.Remove(lancamento);
            _dataContext.SaveChanges();
            return true;
        }

        public List<FinanceiroModels> ListarEntradas(int? mes, int? ano, int? categoriaId)
        {
            var query = _dataContext.Financeiro.AsNoTracking()
                .Where(x => x.Tipo == "Entrada")
                .Include(x => x.CategoriaFinanceiro)
                .Include(x => x.Usuario)
                .AsQueryable();

            if (mes.HasValue)
                query = query.Where(x => x.Data.Month == mes.Value);
            if (ano.HasValue)
                query = query.Where(x => x.Data.Year == ano.Value);
            if (categoriaId.HasValue)
                query = query.Where(x => x.CategoriaFinanceiroId == categoriaId.Value);

            return query.OrderByDescending(x => x.Data).ToList();
        }

        public List<FinanceiroModels> ListarSaidas(int? mes, int? ano, int? categoriaId)
        {
            var query = _dataContext.Financeiro.AsNoTracking()
                .Where(x => x.Tipo == "Saida")
                .Include(x => x.CategoriaFinanceiro)
                .Include(x => x.Usuario)
                .AsQueryable();

            if (mes.HasValue)
                query = query.Where(x => x.Data.Month == mes.Value);
            if (ano.HasValue)
                query = query.Where(x => x.Data.Year == ano.Value);
            if (categoriaId.HasValue)
                query = query.Where(x => x.CategoriaFinanceiroId == categoriaId.Value);

            return query.OrderByDescending(x => x.Data).ToList();
        }

        public List<FinanceiroModels> ListarTodos(int? mes, int? ano, string? tipo)
        {
            var query = _dataContext.Financeiro.AsNoTracking()
                .Include(x => x.CategoriaFinanceiro)
                .Include(x => x.Usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(x => x.Tipo == tipo);
            if (mes.HasValue)
                query = query.Where(x => x.Data.Month == mes.Value);
            if (ano.HasValue)
                query = query.Where(x => x.Data.Year == ano.Value);

            return query.OrderByDescending(x => x.Data).ToList();
        }

        public double ObterSaldo(int? mes, int? ano)
        {
            var query = _dataContext.Financeiro.AsNoTracking().AsQueryable();

            if (mes.HasValue)
                query = query.Where(x => x.Data.Month == mes.Value);
            if (ano.HasValue)
                query = query.Where(x => x.Data.Year == ano.Value);

            var entradas = query.Where(x => x.Tipo == "Entrada").Sum(x => x.Valor);
            var saidas = query.Where(x => x.Tipo == "Saida").Sum(x => x.Valor);
            return entradas - saidas;
        }

        public List<CategoriaFinanceiroModels> ListarCategorias(string? tipo)
        {
            var query = _dataContext.CategoriasFinanceiro.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(x => x.Tipo == tipo);

            return query.ToList();
        }

        public List<FinanceiroModels> ListarPorMembro(int usuarioId)
        {
            return _dataContext.Financeiro.AsNoTracking()
                .Where(x => x.UsuarioModelsId == usuarioId)
                .Include(x => x.CategoriaFinanceiro)
                .OrderByDescending(x => x.Data)
                .ToList();
        }
    }
}
