using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Contact.Domain.Services.Contracts.Mapper
{
    public static class AddressMapper
    {
        public static AddressResponse ToAddressResponse(this Address address)
            => new AddressResponse
            {
                Id = address.Id,
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                District = address.District,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            };
    }
}