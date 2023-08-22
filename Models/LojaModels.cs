using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class LojaModels
    {
        [Key]
        public int Id { get; set; }
        public string NomeLoja { get; set; }
        public int NumeroLoja { get; set; }
        public int Veneravel { get; set; }
        public DateTime DataFundacao    { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Oriente { get; set; }
        public string Rito { get; set; }
        public bool Ativa { get; set; }
    }
}
