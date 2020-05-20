using _4oito6.Template.Domain.Model.Entities;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace _4oito6.Contact.Tests.TestCases
{
    internal static class AddressTestCases
    {
        internal static IList<Address> GetAddresses(string district = null, string city = null, int quantity = 50)
        {
            return new Faker<Address>()
                .CustomInstantiator(f => new Address
                (
                    id: f.Random.Long(1, 9999999),
                    street: f.Random.String(),
                    number: f.Random.Int(1, 5000).ToString(),

                    complement: f.Random.String(),
                    district: district ?? f.Random.String(),
                    city: city ?? f.Random.String(),

                    state: f.Random.String().Substring(1, 2),
                    postalCode: f.Random.Int(1, 99999999).ToString()

                ))
                .Generate(quantity)
                .ToList();
        }
    }
}