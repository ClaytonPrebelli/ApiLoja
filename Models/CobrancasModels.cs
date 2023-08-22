using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class CobrancasModels
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Ref { get; set; }
        public int CategoriaCobrancaId { get; set; }
        public double Valor { get; set; }
        public bool Pago { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public virtual CategoriasCobrancasModels CategoriasCobrancas { get; set; }
    }
}
