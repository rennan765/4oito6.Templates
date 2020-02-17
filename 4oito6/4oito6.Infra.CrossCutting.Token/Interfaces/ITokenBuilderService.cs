using _4oito6.Infra.CrossCutting.Token.Models;

namespace _4oito6.Infra.CrossCutting.Token.Interfaces
{
    public interface ITokenBuilderService
    {
        object BuildToken(int id, string email, string image);

        RefreshTokenModel BuildRefreshToken(int id, string email, string image);

        TokenModel GetToken();
    }
}