using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using System.Threading.Tasks;

namespace _4oito6.Template.Domain.Services.Contracts.Interfaces
{
    public interface IUserService : IServiceBase
    {
        Task<UserResponse> CreateUserAsync(UserRequest request);

        Task<UserResponse> UpdateUserAsync(UserRequest request);
    }
}