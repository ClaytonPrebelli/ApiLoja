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

            CriarEntradaFinanceira(cobranca);

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
            var existente = _dataContext.Cobrancas.Find(cobranca.Id);
            if (existente == null) return cobranca;

            existente.Descricao = cobranca.Descricao;
            existente.Ref = cobranca.Ref;
            existente.CategoriaCobrancaId = cobranca.CategoriaCobrancaId;
            existente.UsuarioModelsId = cobranca.UsuarioModelsId;
            existente.Valor = cobranca.Valor;
            existente.Pago = cobranca.Pago;
            existente.MesReferencia = cobranca.MesReferencia;
            existente.StatusPagamento = cobranca.StatusPagamento;
            existente.Vencimento = cobranca.Vencimento;
            existente.DataPagamento = cobranca.DataPagamento;

            _dataContext.SaveChanges();

            SincronizarFinanceiroDaCobranca(existente);

            return existente;
        }

        public bool DeletarCobranca(int id)
        {
            var cobranca = _dataContext.Cobrancas.Find(id);
            if (cobranca == null) return false;

            var entrada = _dataContext.Financeiro
                .FirstOrDefault(x => x.Observacao == $"#Cob:{id}");
            if (entrada != null)
            {
                var data = entrada.Data;
                _dataContext.Financeiro.Remove(entrada);
                _dataContext.SaveChanges();
                InvalidarSaldoAFrente(data.Month, data.Year);
            }

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
            var cobranca = _dataContext.Cobrancas
                .Include(x => x.CategoriasCobrancas)
                .FirstOrDefault(x => x.Id == id);
            if (cobranca == null) return false;

            cobranca.Pago = true;
            cobranca.DataPagamento = dataPagamento;
            cobranca.StatusPagamento = "Pago";

            var entrada = _dataContext.Financeiro
                .FirstOrDefault(x => x.Observacao == $"#Cob:{cobranca.Id}");

            if (entrada != null)
            {
                entrada.Pago = true;
                entrada.DataPagamento = dataPagamento;
                entrada.Data = dataPagamento;
            }
            else
            {
                CriarEntradaFinanceira(cobranca, dataPagamento);
            }

            _dataContext.SaveChanges();
            InvalidarSaldoAFrente(dataPagamento.Month, dataPagamento.Year);

            return true;
        }

        public List<CategoriasCobrancasModels> ListarCategorias()
        {
            return _dataContext.CategoriasCobrancas.AsNoTracking().ToList();
        }

        private void CriarEntradaFinanceira(CobrancasModels cobranca, DateTime? dataPagamento = null)
        {
            var categoriaCobranca = _dataContext.CategoriasCobrancas
                .FirstOrDefault(x => x.Id == cobranca.CategoriaCobrancaId);

            var categoriaFinanceiroId = 0;
            if (categoriaCobranca != null)
            {
                categoriaFinanceiroId = _dataContext.CategoriasFinanceiro
                    .Where(x => x.Tipo == "Entrada" && x.Nome == categoriaCobranca.CategoriaNome)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            }

            if (categoriaFinanceiroId == 0)
            {
                var outras = _dataContext.CategoriasFinanceiro
                    .FirstOrDefault(x => x.Tipo == "Entrada" && x.Nome == "Outros");
                categoriaFinanceiroId = outras?.Id ?? 1;
            }

            var entrada = new FinanceiroModels
            {
                Tipo = "Entrada",
                CategoriaFinanceiroId = categoriaFinanceiroId,
                Descricao = $"Cobrança: {cobranca.Descricao} ({cobranca.MesReferencia ?? cobranca.Vencimento.ToString("MM/yyyy")})",
                Valor = cobranca.Valor,
                Data = dataPagamento ?? cobranca.Vencimento,
                UsuarioModelsId = cobranca.UsuarioModelsId,
                Observacao = $"#Cob:{cobranca.Id}",
                Pago = cobranca.Pago,
                DataPagamento = cobranca.Pago ? (dataPagamento ?? cobranca.DataPagamento) : null
            };
            _dataContext.Financeiro.Add(entrada);
            _dataContext.SaveChanges();

            if (cobranca.Pago)
                InvalidarSaldoAFrente(entrada.Data.Month, entrada.Data.Year);
        }

        private void SincronizarFinanceiroDaCobranca(CobrancasModels cobranca)
        {
            var entrada = _dataContext.Financeiro
                .FirstOrDefault(x => x.Observacao == $"#Cob:{cobranca.Id}");

            if (entrada == null)
            {
                CriarEntradaFinanceira(cobranca);
                return;
            }

            var categoriaCobranca = _dataContext.CategoriasCobrancas
                .FirstOrDefault(x => x.Id == cobranca.CategoriaCobrancaId);

            var categoriaFinanceiroId = entrada.CategoriaFinanceiroId;
            if (categoriaCobranca != null)
            {
                var found = _dataContext.CategoriasFinanceiro
                    .Where(x => x.Tipo == "Entrada" && x.Nome == categoriaCobranca.CategoriaNome)
                    .Select(x => x.Id)
                    .FirstOrDefault();
                if (found != 0) categoriaFinanceiroId = found;
            }

            entrada.Descricao = $"Cobrança: {cobranca.Descricao} ({cobranca.MesReferencia ?? cobranca.Vencimento.ToString("MM/yyyy")})";
            entrada.Valor = cobranca.Valor;
            entrada.Data = cobranca.Pago ? (cobranca.DataPagamento ?? cobranca.Vencimento) : cobranca.Vencimento;
            entrada.UsuarioModelsId = cobranca.UsuarioModelsId;
            entrada.CategoriaFinanceiroId = categoriaFinanceiroId;
            entrada.Pago = cobranca.Pago;
            entrada.DataPagamento = cobranca.Pago ? cobranca.DataPagamento : null;

            _dataContext.SaveChanges();
            InvalidarSaldoAFrente(entrada.Data.Month, entrada.Data.Year);
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
    }
}
