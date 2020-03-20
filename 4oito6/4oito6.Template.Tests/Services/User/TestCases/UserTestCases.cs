using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using System.Collections.Generic;

namespace _4oito6.Template.Tests.Services.User.TestCases
{
    internal static class UserTestCases
    {
        internal enum TestCase
        {
            EmailExists,
            OnlyAddress,
            OnlyPhones,
            InvalidAddress,
            InvalidPhone,
            InvalidUser,
            PerfectWay
        };

        private static UserRequest GetAddress(UserRequest request, bool isValid = true)
        {
            request.Address.Street = "Avenida Rio Branco";
            request.Address.Number = "156";
            request.Address.Complement = "Loja 1";

            request.Address.District = isValid ? "Centro" : "";
            request.Address.City = "Rio de Janeiro";

            request.Address.State = isValid ? "RJ" : "RJan";
            request.Address.PostalCode = "20040003";

            return request;
        }

        private static UserRequest GetPhones(UserRequest request, bool isValid = true)
        {
            request.Phones = new List<UserPhoneRequest>
            {
                new UserPhoneRequest
                {
                    LocalCode = isValid ? "21" : "357",
                    Number = isValid ? "912345678" : "83"
                },
                new UserPhoneRequest
                {
                    LocalCode = isValid ? "11" : "357",
                    Number = isValid ? "34567890" : "83"
                }
            };

            return request;
        }

        private static UserRequest GetUser(UserRequest request, bool isValid = true)
        {
            request.FirstName = isValid ? "Fulano" : string.Empty;
            request.MiddleName = "de Tal";

            request.LastName = "da Silva";
            request.Cpf = "45237248063";

            return request;
        }

        internal static UserRequest GetRequest(TestCase testCase)
        {
            var request = new UserRequest
            {
                Email = "teste@teste.com"
            };

            if (testCase == TestCase.EmailExists)
                return request;

            request = GetUser(request, testCase == TestCase.PerfectWay || (testCase != TestCase.PerfectWay && testCase != TestCase.InvalidUser));

            if (testCase == TestCase.OnlyAddress)
                request = GetAddress(request);

            if (testCase == TestCase.InvalidAddress)
                request = GetAddress(request, false);

            if (testCase == TestCase.OnlyPhones)
                request = GetPhones(request);

            if (testCase == TestCase.InvalidPhone)
                request = GetPhones(request, false);

            if (testCase == TestCase.PerfectWay)
            {
                request = GetAddress(request);
                request = GetPhones(request);
            }

            return request;
        }
    }
}