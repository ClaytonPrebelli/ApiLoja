using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class DocumentosModels
    {

        [Key]
        public int Id { get; set; }
        public string DocName { get; set; }
        [Required]
        public byte[] DocFile { get; set; }
        public int UsuarioId { get; set; }
    }
}
