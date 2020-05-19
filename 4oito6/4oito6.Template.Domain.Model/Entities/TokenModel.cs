using _4oito6.Domain.Model.Core.Entities;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class TokenModel : EntityBase
    {
        public int IdUser { get; private set; }
        public string Email { get; private set; }
        public object Token { get; private set; }
        public string RefreshToken { get; private set; }

        public TokenModel(int idUser, string email, object token)
        {
            IdUser = idUser;
            Email = email;
            Token = token;
        }

        public TokenModel(int idUser, string email, object token, string refreshToken)
            : this(idUser, email, token)
        {
            RefreshToken = refreshToken;
        }
    }
}