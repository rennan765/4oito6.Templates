using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Contact.Domain.Services.Contracts.Mapper
{
    public static class PhoneMapper
    {
        public static PhoneResponse ToPhoneResponse(this Phone phone)
            => new PhoneResponse
            {
                Id = phone.Id,
                LocalCode = phone.LocalCode,
                Number = phone.Number
            };
    }
}