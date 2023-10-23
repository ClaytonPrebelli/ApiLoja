using System.ComponentModel.DataAnnotations;

namespace ApiLoja.Models
{
    public class CandidatosModels
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataExpedicao { get; set; }
        public DateTime Nascimento { get; set; }
        public bool AcreditaSerSupremo { get; set; }
        public string Religiao { get; set; }
        public string Naturalidade { get; set; }
        public string Estado { get; set; }
        public string Nacionalidade { get; set; }
        public string EstadoCivil { get; set; }
        public string? TipoSanguineo { get; set; }
        public string CEP { get; set; }
        public string Profissao { get; set; }
        public string Endereco { get; set; }
        public string TempoMoradia { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
        public string? Pai { get; set; }
        public bool PaiMacom { get; set; }
        public string Renda { get; set; }
        public string Mae { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataSindicancia { get; set; }
        
        public string Motivos { get; set; }
        public string Vicios { get; set; }
        public string Aptidoes { get; set; }
        public string ContatoEmergencia { get; set; }
        public string FoneEmergencia { get; set; }
        public bool FamiliaConcorda { get; set; }
        public int StatusId { get; set; }
        public int QuemIndica { get; set; }

        public virtual StatusModels Status { get; set; }
        public virtual ICollection<FamiliaresModels> Familiares { get; set; }
        public virtual ICollection<DocumentosModels> Documentos { get; set; }
    }
}
