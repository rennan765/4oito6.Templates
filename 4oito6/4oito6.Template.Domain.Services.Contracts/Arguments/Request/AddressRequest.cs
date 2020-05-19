namespace _4oito6.Template.Domain.Services.Contracts.Arguments.Request
{
    public class AddressRequest
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}