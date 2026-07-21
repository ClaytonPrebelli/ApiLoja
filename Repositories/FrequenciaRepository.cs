using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;
using ApiLoja.Responses;
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
                    existente.Observacao = item.Observacao;
                    existente.TipoSessao = item.TipoSessao;
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

            var thursdays = new List<DateTime>();
            var current = primeiroDia;
            while (current <= ultimoDia)
            {
                if (current.DayOfWeek == DayOfWeek.Thursday)
                    thursdays.Add(current);
                current = current.AddDays(1);
            }

            if (thursdays.Count >= 2) datas.Add(thursdays[1]);
            if (thursdays.Count >= 4) datas.Add(thursdays[3]);

            var datasComRegistros = _dataContext.Frequencia
                .Where(x => x.DataReuniao.Month == mes && x.DataReuniao.Year == ano)
                .Select(x => x.DataReuniao.Date)
                .Distinct()
                .ToList();

            foreach (var d in datasComRegistros)
            {
                if (!datas.Any(x => x.Date == d))
                    datas.Add(d);
            }

            return datas.OrderBy(x => x).ToList();
        }

        public List<FrequenciaHistoricoResponse> ListarHistorico(DateTime dataInicio, DateTime dataFim)
        {
            var todosRegistros = _dataContext.Frequencia.AsNoTracking()
                .Where(x => x.DataReuniao.Date >= dataInicio.Date && x.DataReuniao.Date <= dataFim.Date)
                .Include(x => x.Usuario)
                .ToList();

            var datasReuniao = todosRegistros
                .Select(x => x.DataReuniao.Date)
                .Distinct()
                .Count();

            if (datasReuniao == 0)
                return new List<FrequenciaHistoricoResponse>();

            var agrupado = todosRegistros
                .GroupBy(x => x.UsuarioModelsId)
                .Select(g => new FrequenciaHistoricoResponse
                {
                    UsuarioId = g.Key,
                    Nome = g.First().Usuario?.Nome ?? string.Empty,
                    Cargo = g.First().Usuario?.Cargo?.Nome,
                    TotalReunioes = datasReuniao,
                    Presencas = g.Count(x => x.Presente),
                    Percentual = Math.Round((double)g.Count(x => x.Presente) / datasReuniao * 100, 1)
                })
                .OrderBy(x => x.Nome)
                .ToList();

            return agrupado;
        }
    }
}
