using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiLoja.Models
{
    public class UsuarioModels
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? CIM { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime Nascimento { get; set; }
        public string Naturalidade { get; set; }
        public string Estado { get; set; }
        public string Nacionalidade { get; set; }
        public string EstadoCivil { get; set; }
        public string? TipoSanguineo { get; set; }
        public string CEP { get; set; }
        public string Profissao { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public string? Pai { get; set; }
        public string Mae { get; set; }
        public DateTime? Iniciacao { get; set; }
        public DateTime? Elevacao { get; set; }
        public DateTime? Exaltacao { get; set; }
        public bool isMestreInstalado { get; set; }
        public DateTime? DataInstalacao { get; set; }
        public string Observacoes { get; set; }
        public string ContatoEmergencia { get; set; }
        public string FoneEmergencia { get; set; }
        public bool isCandidato { get; set; }
        public bool isAprendiz { get; set; }
        public bool isCompanheiro { get; set; }
        public bool isMestre { get; set; }
        public bool isAdmin { get; set; }
        public bool isSuperAdmin { get; set; } = false;
        public string Pass { get; set; }
        public DateTime? DataAfiliacao { get; set; }
        public string FormaAfiliacao { get; set; }
        public int? CargoId { get; set; }
        public int StatusId { get; set; }
        public virtual CargosModels? Cargo { get; set; }
        public virtual StatusModels Status { get; set; }
        public virtual ICollection<FamiliaresModels> Familiares { get; set; }
        public virtual ICollection<DocumentosModels> Documentos { get; set; }
        [JsonIgnore]
        public virtual ICollection<CobrancasModels> Cobrancas { get; set;}
    }
}
