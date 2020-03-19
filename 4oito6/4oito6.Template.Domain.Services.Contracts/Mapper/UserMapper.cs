using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;

namespace _4oito6.Template.Domain.Services.Contracts.Mapper
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(this User user) => new UserResponse { Id = user.Id };
    }
}