using _4oito6.Domain.Model.Core.Entities;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class Address : EntityBase
    {
        public long Id { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        public Address(long id, string street, string number, string complement, string district, string city, string state, string postalCode)
        {
            Id = id;
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        public Address(string street, string number, string complement, string district, string city, string state, string postalCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            City = city;
            State = state;
            PostalCode = postalCode;
        }
    }
}