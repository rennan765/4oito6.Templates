using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Template.Domain.Model.ValueObjects;
using System.Collections.Generic;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class User : EntityBase
    {
        public int Id { get; private set; }
        public Name Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public Address Address { get; private set; }

        public IList<Phone> Phones { get; private set; }

        public User(int id, Name name, string email, string cpf, Address address, IList<Phone> phones)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            Address = address;
            Phones = phones;
        }

        public User(int id, Name name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public User(Name name, string email, string cpf, Address address)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Address = address;
        }

        public User(Name name, string email, string cpf, Address address, IList<Phone> phones)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Address = address;
            Phones = phones;
        }

        public void Update(string firstName, string middleName, string lastName, string email, string cpf)
        {
            Name = new Name(firstName, middleName, lastName);
            Email = email;
            Cpf = cpf;
        }

        public void ChangeAddress(Address address)
        {
            Address = address;
        }

        public void ChangePhones(IList<Phone> phones)
        {
            Phones = phones;
        }

        public void RemoveAddress()
        {
            Address = null;
        }

        public void RemovePhones()
        {
            Phones = new List<Phone>();
        }
    }
}