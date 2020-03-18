using _4oito6.Domain.Services.Core.Implementation.Base;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Services.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using System;
using System.Threading.Tasks;

namespace _4oito6.Template.Domain.Services.Implementation
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserBus _userBus;

        public UserService(IUserBus userBus)
            : base(new[] { userBus })
        {
            _userBus = userBus ?? throw new ArgumentNullException(nameof(userBus));
        }

        public Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}