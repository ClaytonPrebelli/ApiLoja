using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class ComunicadosFotoModels
    {
        [Key]
        public int Id { get; set; }
        public string FotoName { get; set; }
        public string FotoFile { get; set; }
        public int ComunicadoId { get; set; }
        public virtual ComunicadosModels? Comunicado { get; set; }
    }
}
