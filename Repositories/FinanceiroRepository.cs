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
            InvalidarSaldoAFrente(financeiro.Data.Month, financeiro.Data.Year);
            return financeiro;
        }

        public FinanceiroModels CadastrarSaida(FinanceiroModels financeiro)
        {
            financeiro.Tipo = "Saida";
            if (!financeiro.Pago) financeiro.Pago = false;
            _dataContext.Financeiro.Add(financeiro);
            _dataContext.SaveChanges();
            InvalidarSaldoAFrente(financeiro.Data.Month, financeiro.Data.Year);
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
            InvalidarSaldoAFrente(financeiro.Data.Month, financeiro.Data.Year);
            SincronizarCobrancaDoFinanceiro(financeiro);
            return financeiro;
        }

        public bool DeletarLancamento(int id)
        {
            var lancamento = _dataContext.Financeiro.Find(id);
            if (lancamento == null) return false;

            var cobrancaId = ObterCobrancaIdDaObservacao(lancamento.Observacao);
            var data = lancamento.Data;
            _dataContext.Financeiro.Remove(lancamento);
            _dataContext.SaveChanges();
            InvalidarSaldoAFrente(data.Month, data.Year);

            if (cobrancaId.HasValue)
                LimparVinculoCobranca(cobrancaId.Value);

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

        public List<FinanceiroModels> ListarTodos(int? mes, int? ano, string? tipo, bool? pago)
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
            if (pago.HasValue)
                query = query.Where(x => x.Pago == pago.Value);

            return query.OrderByDescending(x => x.Data).ToList();
        }

        public double ObterSaldo(int? mes, int? ano)
        {
            if (mes.HasValue && ano.HasValue)
            {
                var saldoCache = _dataContext.Saldo
                    .FirstOrDefault(x => x.Mes == mes.Value && x.Ano == ano.Value);
                if (saldoCache != null)
                    return saldoCache.Valor;
            }

            var query = _dataContext.Financeiro.AsNoTracking().AsQueryable();

            if (mes.HasValue && ano.HasValue)
                query = query.Where(x => (x.Data.Year < ano.Value) || (x.Data.Year == ano.Value && x.Data.Month <= mes.Value));
            else if (ano.HasValue)
                query = query.Where(x => x.Data.Year <= ano.Value);

            var entradas = query.Where(x => x.Tipo == "Entrada" && x.Pago).Sum(x => x.Valor);
            var saidas = query.Where(x => x.Tipo == "Saida" && x.Pago).Sum(x => x.Valor);
            var valor = entradas - saidas;

            if (mes.HasValue && ano.HasValue)
            {
                var saldoExistente = _dataContext.Saldo
                    .FirstOrDefault(x => x.Mes == mes.Value && x.Ano == ano.Value);
                if (saldoExistente != null)
                {
                    saldoExistente.Valor = valor;
                    saldoExistente.DataAtualizacao = DateTime.Now;
                }
                else
                {
                    _dataContext.Saldo.Add(new SaldoModels
                    {
                        Mes = mes.Value,
                        Ano = ano.Value,
                        Valor = valor,
                        DataAtualizacao = DateTime.Now
                    });
                }
                _dataContext.SaveChanges();
            }

            return valor;
        }

        private void InvalidarSaldoAFrente(int mes, int ano)
        {
            var saldosParaRemover = _dataContext.Saldo
                .Where(x => (x.Ano > ano) || (x.Ano == ano && x.Mes >= mes))
                .ToList();

            if (saldosParaRemover.Any())
            {
                _dataContext.Saldo.RemoveRange(saldosParaRemover);
                _dataContext.SaveChanges();
            }
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

        public bool MarcarComoPago(int id, DateTime dataPagamento)
        {
            var lancamento = _dataContext.Financeiro.Find(id);
            if (lancamento == null) return false;

            lancamento.Pago = true;
            lancamento.DataPagamento = dataPagamento;
            _dataContext.SaveChanges();
            InvalidarSaldoAFrente(lancamento.Data.Month, lancamento.Data.Year);

            var cobrancaId = ObterCobrancaIdDaObservacao(lancamento.Observacao);
            if (cobrancaId.HasValue)
            {
                var cobranca = _dataContext.Cobrancas.Find(cobrancaId.Value);
                if (cobranca != null)
                {
                    cobranca.Pago = true;
                    cobranca.DataPagamento = dataPagamento;
                    cobranca.StatusPagamento = "Pago";
                    _dataContext.SaveChanges();
                }
            }

            return true;
        }

        private int? ObterCobrancaIdDaObservacao(string? observacao)
        {
            if (string.IsNullOrEmpty(observacao)) return null;
            if (!observacao.StartsWith("#Cob:")) return null;
            if (int.TryParse(observacao.Substring(5), out int id))
                return id;
            return null;
        }

        private void SincronizarCobrancaDoFinanceiro(FinanceiroModels financeiro)
        {
            var cobrancaId = ObterCobrancaIdDaObservacao(financeiro.Observacao);
            if (!cobrancaId.HasValue) return;

            var cobranca = _dataContext.Cobrancas.Find(cobrancaId.Value);
            if (cobranca == null) return;

            cobranca.Valor = financeiro.Valor;
            cobranca.Descricao = financeiro.Descricao.Replace("Cobrança: ", "").Split(" (")[0];
            cobranca.UsuarioModelsId = financeiro.UsuarioModelsId;
            cobranca.Pago = financeiro.Pago;
            cobranca.DataPagamento = financeiro.DataPagamento;
            cobranca.StatusPagamento = financeiro.Pago ? "Pago" : "Pendente";

            if (financeiro.CategoriaFinanceiroId > 0)
            {
                var catFinanceiro = _dataContext.CategoriasFinanceiro.Find(financeiro.CategoriaFinanceiroId);
                if (catFinanceiro != null)
                {
                    var catCobranca = _dataContext.CategoriasCobrancas
                        .FirstOrDefault(x => x.CategoriaNome == catFinanceiro.Nome);
                    if (catCobranca != null)
                        cobranca.CategoriaCobrancaId = catCobranca.Id;
                }
            }

            _dataContext.SaveChanges();
        }

        private void LimparVinculoCobranca(int cobrancaId)
        {
            var cobranca = _dataContext.Cobrancas.Find(cobrancaId);
            if (cobranca == null) return;

            cobranca.Pago = false;
            cobranca.DataPagamento = null;
            cobranca.StatusPagamento = "Pendente";
            _dataContext.SaveChanges();
        }
    }
}
