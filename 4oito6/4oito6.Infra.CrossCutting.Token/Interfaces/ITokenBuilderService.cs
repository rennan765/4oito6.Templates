using _4oito6.Infra.CrossCutting.Token.Models;
using System.Threading.Tasks;

namespace _4oito6.Infra.CrossCutting.Token.Interfaces
{
    public interface ITokenBuilderService
    {
        Task<object> BuildTokenAsync(int id, string email, string image);

        Task<RefreshTokenModel> BuildRefreshTokenAsync(int id);

        Task<TokenModel> GetTokenAsync();

        Task<RefreshTokenModel> GetRefreshTokenAsync(string key);
    }
}