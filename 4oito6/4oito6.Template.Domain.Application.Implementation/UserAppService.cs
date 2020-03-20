using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Implementation.Base;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Domain.Application.Contracts.Interfaces;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Services.Contracts.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Template.Domain.Application.Implementation
{
    public class UserAppService : AppServiceBase, IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService, IUnitOfWork unit)
            : base(unit, new IDisposable[] { userService })
        {
            _userService = userService;
        }

        public async Task<ResponseMessage<UserResponse>> CreateUserAsync(UserRequest request)
        {
            var response = await _userService.CreateUserAsync(request).ConfigureAwait(false);

            var message = new ResponseMessage<UserResponse>
            {
                Data = response,
                StatusCode = (int)(_userService.IsSatisfied() ? HttpStatusCode.OK : HttpStatusCode.BadRequest)
            };

            if (_userService.IsSatisfied())
                message.Message = string.Concat(_userService.GetMessages());
            else
                message.Errors = _userService.GetMessages();

            return message;
        }
    }
}