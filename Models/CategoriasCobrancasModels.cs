using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class CategoriasCobrancasModels
    {
        [Key]
        public int Id { get; set; }
        public string CategoriaNome { get; set; }
    }
}
