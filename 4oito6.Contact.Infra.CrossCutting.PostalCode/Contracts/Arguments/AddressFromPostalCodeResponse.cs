using _4oito6.Contact.Infra.CrossCutting.PostalCode.Model;

namespace _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments
{
    public class AddressFromPostalCodeResponse
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public static explicit operator AddressFromPostalCodeResponse(AddressFromPostalCode address)
            => new AddressFromPostalCodeResponse
            {
                Street = address.Street,
                District = address.District,

                City = address.City,
                State = address.State
            };
    }
}