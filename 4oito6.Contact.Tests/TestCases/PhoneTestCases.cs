using _4oito6.Contact.Domain.Model.Views;
using Bogus;
using System.Linq;

namespace _4oito6.Contact.Tests.TestCases
{
    internal static class PhoneTestCases
    {
        internal static IQueryable<ViewPhone> GetPhones(string localCode, int quantity = 50)
        {
            return new Faker<ViewPhone>()
                .CustomInstantiator(f => new ViewPhone
                {
                    Id = f.Random.Long(1, 999999999),
                    LocalCode = localCode ?? f.Random.Int(1, 99).ToString(),
                    Number = f.Random.Int(1, 999999999).ToString(),
                    IdUser = f.Random.Int(1, 999999999)
                })
                .Generate(quantity)
                .AsQueryable();
        }
    }
}