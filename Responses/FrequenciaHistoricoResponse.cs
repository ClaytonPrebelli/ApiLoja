namespace ApiLoja.Responses
{
    public class FrequenciaHistoricoResponse
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        public int TotalReunioes { get; set; }
        public int Presencas { get; set; }
        public double Percentual { get; set; }
    }
}
