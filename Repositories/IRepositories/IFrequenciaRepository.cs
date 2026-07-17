using ApiLoja.Models;

namespace ApiLoja.Repositories.IRepositories
{
    public interface IFrequenciaRepository
    {
        FrequenciaModels RegistrarPresenca(FrequenciaModels frequencia);
        List<FrequenciaModels> ListarPorData(DateTime dataReuniao);
        List<FrequenciaModels> ListarPorMembro(int usuarioId);
        FrequenciaModels VerificarPresenca(int usuarioId, DateTime dataReuniao);
        bool TogglePresenca(int usuarioId, DateTime dataReuniao);
        List<FrequenciaModels> ListarTodas(int? mes, int? ano);
        void SalvarAlteracoes();
    }
}
