using _4oito6.Template.Domain.Model.Entities;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace _4oito6.Contact.Tests.TestCases
{
    internal static class PhoneTestCases
    {
        internal static IList<Phone> GetPhones(string localCode, int quantity = 50)
        {
            return new Faker<Phone>()
                .CustomInstantiator(f => new Phone
                (
                    id: f.Random.Long(1, 999999999),
                    localCode: localCode ?? f.Random.Int(1, 99).ToString(),
                    number: f.Random.Int(1, 999999999).ToString()
                ))
                .Generate(quantity)
                .ToList();
        }
    }
}