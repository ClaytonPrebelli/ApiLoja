using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLoja.Models
{
    public class CobrancasModels
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Ref { get; set; }
        [ForeignKey("CategoriasCobrancas")]
        public int CategoriaCobrancaId { get; set; }
        public int? UsuarioModelsId { get; set; }
        public double Valor { get; set; }
        public bool Pago { get; set; }
        public string? MesReferencia { get; set; }
        public string? StatusPagamento { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public virtual CategoriasCobrancasModels CategoriasCobrancas { get; set; }
        public virtual UsuarioModels? Usuario { get; set; }
    }
}
