using _4oito6.Contact.Domain.Model.Views;
using Bogus;
using System.Linq;

namespace _4oito6.Contact.Tests.TestCases
{
    internal static class AddressTestCases
    {
        internal static IQueryable<ViewAddress> GetAddresses(string district = null, string city = null, int quantity = 50)
        {
            return new Faker<ViewAddress>()
                .CustomInstantiator(f => new ViewAddress
                {
                    Id = f.Random.Long(1, 9999999),
                    Street = f.Random.String(),
                    Number = f.Random.Int(1, 5000).ToString(),

                    Complement = f.Random.String(),
                    District = district ?? f.Random.String(),
                    City = city ?? f.Random.String(),

                    State = f.Random.String().Substring(1, 2),
                    PostalCode = f.Random.Int(1, 99999999).ToString(),
                    IdUser = f.Random.Int(1, 5000)
                })
                .Generate(quantity)
                .AsQueryable();
        }
    }
}