using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Interfaces;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using System.Threading.Tasks;

namespace _4oito6.Template.Domain.Application.Contracts.Interfaces
{
    public interface IUserAppService : IAppServiceBase
    {
        Task<ResponseMessage<UserResponse>> CreateUserAsync(UserRequest request);
    }
}