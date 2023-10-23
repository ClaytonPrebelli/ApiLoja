using ApiLoja.Data;
using ApiLoja.Models;
using ApiLoja.Repositories.IRepositories;

namespace ApiLoja.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _dataContext;
        public TokenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public string GerarToken(int id)
        {
            
            var tokenString = Guid.NewGuid();
            TokenModels novoToken = new TokenModels()
            {
                Ativo = true,
                Id = 0,
                Token = tokenString.ToString(),
                QuemIndica = id

            };
            try
            {
                _dataContext.Token.Add(novoToken);
                _dataContext.SaveChanges();
                return novoToken.Token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public TokenModels ValidarToken(string token)
        {
            var result = _dataContext.Token.Where(x => x.Token == token && x.Ativo).FirstOrDefault();
            if (result == null)
            {
                return result;
            }
            else
            {
                return result;
            }
        }
        public TokenModels DesabilitarToken(TokenModels token)
        {
            try
            {
                token.Ativo = false;
                _dataContext.Update(token);
                _dataContext.SaveChanges();
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
