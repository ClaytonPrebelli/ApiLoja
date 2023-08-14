using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class FamiliaresModels
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string FamiliarNome { get; set; }
        public DateTime NascimentoFamiliar { get; set; }
        public string Relacao { get; set; }
      
    }
}
