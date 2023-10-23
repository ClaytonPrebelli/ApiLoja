using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface ICandidatosRepository
    {
        CandidatosModels CadastrarCandidato(CandidatosModels candidato);
    }
}
