using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class CategoriaFinanceiroModels
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}
