using _4oito6.Infra.Data.Model.Core;
using System.Collections.Generic;

namespace _4oito6.Template.Infra.Data.Model.Entities
{
    public class User : DataModelBase
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public long? IdAddress { get; private set; }

        public virtual Address Address { get; private set; }
        public virtual IList<UserPhone> Phones { get; private set; }

        public User(int id, string firstName, string middleName, string lastName, string email, string cpf, long? idAddress, Address address, IList<UserPhone> phones)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Cpf = cpf;
            IdAddress = idAddress;
            Address = address;
            Phones = phones;
        }

        protected User()
        {
            Phones = new List<UserPhone>();
        }
    }
}