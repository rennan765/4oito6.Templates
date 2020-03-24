using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Implementation.Base;
using _4oito6.Infra.Data.Transactions.Contracts.Enum;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Domain.Application.Contracts.Interfaces;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Services.Contracts.Interfaces;
using System;
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
            Unit.BeginTransaction(DataSource.EntityFramework);

            var response = await _userService.CreateUserAsync(request).ConfigureAwait(false);

            var message = new ResponseMessage<UserResponse>
            {
                Data = response,
                StatusCode = (int)_userService.GetStatusCode()
            };

            if (!_userService.IsSatisfied())
                message.Errors = _userService.GetMessages();

            if (_userService.IsSatisfied())
                Unit.Commit(DataSource.EntityFramework);
            else
                Unit.Rollback(DataSource.EntityFramework);

            return message;
        }

        public async Task<ResponseMessage<UserResponse>> UpdateUserAsync(UserRequest request)
        {
            Unit.BeginTransaction(DataSource.EntityFramework);

            var response = await _userService.UpdateUserAsync(request).ConfigureAwait(false);

            var message = new ResponseMessage<UserResponse>
            {
                Data = response,
                StatusCode = (int)_userService.GetStatusCode()
            };

            if (!_userService.IsSatisfied())
                message.Errors = _userService.GetMessages();

            if (_userService.IsSatisfied())
                Unit.Commit(DataSource.EntityFramework);
            else
                Unit.Rollback(DataSource.EntityFramework);

            return message;
        }
    }
}