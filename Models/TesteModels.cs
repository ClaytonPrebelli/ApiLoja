using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class TesteModels
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
