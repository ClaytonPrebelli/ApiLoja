using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class CargosModels
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
