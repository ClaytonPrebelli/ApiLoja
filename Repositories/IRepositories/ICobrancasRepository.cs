using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface ICobrancasRepository
    {
        CobrancasModels CadastrarCobranca(CobrancasModels cobranca);
        CobrancasModels VerCobranca(int id);
        CobrancasModels AtualizarCobranca(CobrancasModels cobranca);
        bool DeletarCobranca(int id);
        List<CobrancasModels> ListarPorMembro(int usuarioId);
        List<CobrancasModels> ListarTodas(int? categoriaId, bool? paga);
        bool MarcarComoPaga(int id, DateTime dataPagamento);
        List<CategoriasCobrancasModels> ListarCategorias();
    }
}
