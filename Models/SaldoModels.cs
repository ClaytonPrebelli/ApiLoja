using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLoja.Models
{
    [Table("Saldo")]
    public class SaldoModels
    {
        [Key]
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public double Valor { get; set; }
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
