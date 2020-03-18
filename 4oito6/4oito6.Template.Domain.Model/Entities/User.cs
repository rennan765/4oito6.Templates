using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Template.Domain.Model.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace _4oito6.Template.Domain.Model.Entities
{
    public class User : EntityBase
    {
        public int Id { get; private set; }
        public Name Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public Address Address { get; private set; }

        public IList<Phone> Phones { get; set; }

        public void ChangeAddress(Address address)
        {
            Address = address;
        }

        public void AddPhone(string localCode, string number)
        {
            if (!Phones.Any(p => p.LocalCode == localCode && p.Number == number))
                Phones.Add(new Phone(localCode, number));
        }

        public void RemovePhone(string localCode, string number)
        {
            var phone = Phones.FirstOrDefault(p => p.LocalCode == localCode && p.Number == number);

            if (phone != null)
                Phones.Remove(phone);
        }

        public void ChangePhone(string oldLocalCode, string oldNumber, string newLocalCode, string newNumber)
        {
            var oldPhone = Phones.FirstOrDefault(p => p.LocalCode == oldLocalCode && p.Number == oldNumber); ;

            if (oldPhone != null)
                Phones.Remove(oldPhone);

            if (!Phones.Any(p => p.LocalCode == newLocalCode && p.Number == newNumber))
                Phones.Add(new Phone(newLocalCode, newNumber));
        }
    }
}