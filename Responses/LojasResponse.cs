using ApiLoja.Models;

namespace ApiLoja.Responses
{
    public class LojasResponse
    {
        public int Id { get; set; }
        public string NomeLoja { get; set; }
        public int NumeroLoja { get; set; }
        public int Veneravel { get; set; }
        public DateTime DataFundacao { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Oriente { get; set; }
        public string Rito { get; set; }
        public bool Ativa { get; set; }
        public string? Instagram { get; set; }
        public virtual ICollection<DocumentosModels> Documentos { get; set; }
        
        public  string? VeneravelNome { get; set; }
        public string? VeneravelTelefone { get; set; }
    }
}

