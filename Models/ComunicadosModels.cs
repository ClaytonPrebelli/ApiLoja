using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class ComunicadosModels
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Graus { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int AutorId { get; set; }
        public virtual UsuarioModels? Autor { get; set; }
        public virtual ICollection<ComunicadosFotoModels>? FotosComunicados { get; set; }
    }
}
