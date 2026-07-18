using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IFinanceiroRepository
    {
        FinanceiroModels CadastrarEntrada(FinanceiroModels financeiro);
        FinanceiroModels CadastrarSaida(FinanceiroModels financeiro);
        FinanceiroModels VerLancamento(int id);
        FinanceiroModels AtualizarLancamento(FinanceiroModels financeiro);
        bool DeletarLancamento(int id);
        List<FinanceiroModels> ListarEntradas(int? mes, int? ano, int? categoriaId);
        List<FinanceiroModels> ListarSaidas(int? mes, int? ano, int? categoriaId);
        List<FinanceiroModels> ListarTodos(int? mes, int? ano, string? tipo, bool? pago);
        double ObterSaldo(int? mes, int? ano);
        bool MarcarComoPago(int id, DateTime dataPagamento);
        List<CategoriaFinanceiroModels> ListarCategorias(string? tipo);
        List<FinanceiroModels> ListarPorMembro(int usuarioId);
    }
}
