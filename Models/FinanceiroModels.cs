using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class FinanceiroModels
    {
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int CategoriaFinanceiroId { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public bool Pago { get; set; }
        public DateTime? DataPagamento { get; set; }
        public int? UsuarioModelsId { get; set; }
        public string? Observacao { get; set; }
        public virtual UsuarioModels? Usuario { get; set; }
        public virtual CategoriaFinanceiroModels? CategoriaFinanceiro { get; set; }
    }
}
