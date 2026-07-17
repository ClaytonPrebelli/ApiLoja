using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ApiLoja.Repositories
{
    public class FrequenciaRepository : IFrequenciaRepository
    {
        private readonly DataContext _dataContext;

        public FrequenciaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public FrequenciaModels RegistrarPresenca(FrequenciaModels frequencia)
        {
            _dataContext.Frequencia.Add(frequencia);
            _dataContext.SaveChanges();
            return frequencia;
        }

        public List<FrequenciaModels> ListarPorData(DateTime dataReuniao)
        {
            return _dataContext.Frequencia.AsNoTracking()
                .Where(x => x.DataReuniao.Date == dataReuniao.Date)
                .Include(x => x.Usuario)
                .OrderBy(x => x.Usuario.Nome)
                .ToList();
        }

        public List<FrequenciaModels> ListarPorMembro(int usuarioId)
        {
            return _dataContext.Frequencia.AsNoTracking()
                .Where(x => x.UsuarioModelsId == usuarioId)
                .OrderByDescending(x => x.DataReuniao)
                .ToList();
        }

        public FrequenciaModels VerificarPresenca(int usuarioId, DateTime dataReuniao)
        {
            return _dataContext.Frequencia.AsNoTracking()
                .FirstOrDefault(x => x.UsuarioModelsId == usuarioId && x.DataReuniao.Date == dataReuniao.Date);
        }

        public bool TogglePresenca(int usuarioId, DateTime dataReuniao)
        {
            var registro = _dataContext.Frequencia
                .FirstOrDefault(x => x.UsuarioModelsId == usuarioId && x.DataReuniao.Date == dataReuniao.Date);

            if (registro != null)
            {
                registro.Presente = !registro.Presente;
            }
            else
            {
                registro = new FrequenciaModels
                {
                    UsuarioModelsId = usuarioId,
                    DataReuniao = dataReuniao,
                    Presente = true
                };
                _dataContext.Frequencia.Add(registro);
            }

            _dataContext.SaveChanges();
            return registro.Presente;
        }

        public List<FrequenciaModels> ListarTodas(int? mes, int? ano)
        {
            var query = _dataContext.Frequencia.AsNoTracking()
                .Include(x => x.Usuario)
                .AsQueryable();

            if (mes.HasValue)
                query = query.Where(x => x.DataReuniao.Month == mes.Value);

            if (ano.HasValue)
                query = query.Where(x => x.DataReuniao.Year == ano.Value);

            return query.OrderByDescending(x => x.DataReuniao)
                .ThenBy(x => x.Usuario.Nome)
                .ToList();
        }

        public void SalvarAlteracoes()
        {
            _dataContext.SaveChanges();
        }
    }
}
