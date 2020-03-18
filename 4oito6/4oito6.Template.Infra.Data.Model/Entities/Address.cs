using _4oito6.Infra.Data.Model.Core;
using System.Collections.Generic;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class Address : DataModelBase
    {
        public long Id { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        public virtual IList<User> Users { get; private set; }

        public Address()
        {
            Users = new List<User>();
        }

        public Address(long id, string street, string number, string complement, string district, string city, string state, string postalCode, IList<User> users)
        {
            Id = id;
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            City = city;
            State = state;
            PostalCode = postalCode;
            Users = users;
        }

        public Address(long id, string street, string number, string complement, string district, string city, string state, string postalCode)
            : base()
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
    }
}