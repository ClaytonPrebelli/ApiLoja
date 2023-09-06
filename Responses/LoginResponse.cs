using ApiLoja.Models;

namespace ApiLoja.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool isCandidato { get; set; }
        public bool isAprendiz { get; set; }
        public bool isCompanheiro { get; set; }
        public bool isMestre { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public int LojaId { get; set; }
        public string? Titulo { get; set; }
        public byte[]?  Foto { get; set; }
    }
}
