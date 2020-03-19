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
        private readonly IPhoneBus _phoneBus;
        private readonly IAddressBus _addressBus;

        public UserService(IUserBus userBus, IPhoneBus phoneBus, IAddressBus addressBus)
            : base(new[] { userBus, phoneBus, addressBus })
        {
            _userBus = userBus ?? throw new ArgumentNullException(nameof(userBus));
            _phoneBus = phoneBus ?? throw new ArgumentNullException(nameof(phoneBus));
            _addressBus = addressBus ?? throw new ArgumentNullException(nameof(addressBus));
        }

        //public UserService(IUserBus userBus)
        //    : base(new[] { userBus })
        //{
        //    _userBus = userBus ?? throw new ArgumentNullException(nameof(userBus));
        //}

        public Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}