using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class FotosModels
    {
         
        [Key]
        public int Id { get; set; }
        public string FotoName { get; set; }
        [Required]
        public byte[] FotoFile { get; set; }
        public int UsuarioId { get; set; }
    }

}
