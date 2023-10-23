using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class TokenModels
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public int QuemIndica { get; set; }
        public bool Ativo { get; set; }
    }
}
