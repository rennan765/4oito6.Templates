namespace _4oito6.Contact.Domain.Services.Contracts.Arguments.Response
{
    public class AddressResponse
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}