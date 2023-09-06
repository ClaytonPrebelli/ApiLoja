using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class FotosNoticiaModels
    {
        [Key]
        public int Id { get; set; }
        public string FotoName { get; set; }

        public byte[] FotoFile { get; set; }
        public int NoticiasId { get; set; }
    }
}
