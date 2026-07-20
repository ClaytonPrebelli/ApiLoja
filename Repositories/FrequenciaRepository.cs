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
                .Include(x => x.Usuario)
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

        public bool DeletarPresenca(int id)
        {
            var registro = _dataContext.Frequencia.Find(id);
            if (registro == null) return false;

            _dataContext.Frequencia.Remove(registro);
            _dataContext.SaveChanges();
            return true;
        }

        public List<FrequenciaModels> SalvarLista(List<FrequenciaModels> lista)
        {
            foreach (var item in lista)
            {
                var existente = _dataContext.Frequencia
                    .FirstOrDefault(x => x.UsuarioModelsId == item.UsuarioModelsId
                                     && x.DataReuniao.Date == item.DataReuniao.Date);

                if (existente != null)
                {
                    existente.Presente = item.Presente;
                }
                else
                {
                    _dataContext.Frequencia.Add(item);
                }
            }

            _dataContext.SaveChanges();
            return lista;
        }

        public List<DateTime> ListarDatasReuniao(int mes, int ano)
        {
            var datas = new List<DateTime>();
            var primeiroDia = new DateTime(ano, mes, 1);
            var ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);

            var current = primeiroDia;
            while (current <= ultimoDia)
            {
                if (current.DayOfWeek == DayOfWeek.Thursday)
                {
                    datas.Add(current);
                }
                current = current.AddDays(1);
            }

            return datas;
        }
    }
}
