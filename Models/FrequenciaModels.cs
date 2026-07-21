using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class FrequenciaModels
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioModelsId { get; set; }
        public DateTime DataReuniao { get; set; }
        public bool Presente { get; set; }
        public string? Observacao { get; set; }
        public string? TipoSessao { get; set; }
        public virtual UsuarioModels? Usuario { get; set; }
    }
}
