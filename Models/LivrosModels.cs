using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class LivrosModels
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }
        public bool isAprendiz { get; set; }
        public bool isCompanheiro { get; set; }
        public bool isMestre { get; set; }
    }
}
