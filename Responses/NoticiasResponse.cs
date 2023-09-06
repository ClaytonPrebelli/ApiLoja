using ApiLoja.Models;

namespace ApiLoja.Responses
{
    public class NoticiasResponse
    {
        
        public int Resultados { get; set; }
        public int Page { get; set; }
        public int TotalPaginas { get; set; }

        public ICollection<NoticiasModels> Noticias { get; set; }
    }
}
