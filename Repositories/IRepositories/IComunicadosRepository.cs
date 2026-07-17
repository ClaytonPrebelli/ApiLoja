using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IComunicadosRepository
    {
        ComunicadosModels CadastrarComunicado(ComunicadosModels comunicado);
        ComunicadosModels VerComunicado(int id);
        ComunicadosModels AtualizarComunicado(ComunicadosModels comunicado);
        bool DeletarComunicado(int id);
        List<ComunicadosModels> ListarTodos();
        List<ComunicadosModels> ListarPorGrau(string grau);
        List<ComunicadosModels> ListarRecentes(int quantidade);
    }
}
