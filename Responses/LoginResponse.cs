namespace ApiLoja.Responses
{
    public class LoginResponse
    {
        public string Nome { get; set; }
        public bool isCandidato { get; set; }
        public bool isAprendiz { get; set; }
        public bool isCompanheiro { get; set; }
        public bool isMestre { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
    }
}
