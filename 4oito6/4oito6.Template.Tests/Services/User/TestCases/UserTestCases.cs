using _4oito6.Template.Domain.Model.ValueObjects;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using Bogus;
using System.Collections.Generic;
using System.Linq;
using Entities = _4oito6.Template.Domain.Model.Entities;

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
            if (request.Address == null)
                request.Address = new AddressRequest();

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

        internal static Entities.User GetUserByRequest(UserRequest request, IList<Entities.Phone> phones = null, Entities.Address address = null)
        {
            if (address == null)
                address = new Entities.Address(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode);

            if (phones == null)
                phones = new List<Entities.Phone>();

            var list =
                (
                    phones.Any() ?
                        request.Phones.Where(p => !phones.Any(db => db.LocalCode == p.LocalCode & db.Number == p.Number)) :
                        request.Phones
                )
                .Select(p => new Entities.Phone(p.LocalCode, p.Number)).ToList();

            list.ForEach(p => phones.Add(p));

            return new Entities.User(new Name(request.FirstName, request.MiddleName, request.LastName), request.Email, request.Cpf, address, phones);
        }

        internal static Entities.User GetUserWithId(Entities.User user)
            => new Entities.User(50, user.Name, user.Email, user.Cpf, user.Address, user.Phones);

        internal static UserResponse GetResponseByUser(Entities.User user)
            => new UserResponse { Id = user.Id };

        internal static IList<Entities.Phone> GetPhonesFromDb(UserRequest request)
        {
            if (!request.Phones.Any())
                return null;

            var phoneRequest = request.Phones.FirstOrDefault();

            return new List<Entities.Phone>
            {
                new Entities.Phone(33, phoneRequest.LocalCode, phoneRequest.Number)
            };
        }

        internal static Entities.Address GetAddressFromDb(UserRequest request)
            => new Entities.Address(84, request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode);

        internal static UserRequest GetRequestToUpdate(TestCase testCase)
        {
            var request = GetRequest(testCase);
            request.Id = 8;

            return request;
        }

        internal static Entities.User GetUserToUpdate(TestCase testCase)
        {
            var request = GetRequestToUpdate(testCase);

            Entities.Address address = null;

            if (request.Address != null)
                address = new Entities.Address
                (
                    90,
                    request.Address.Street,
                    request.Address.Number,
                    request.Address.Complement,
                    request.Address.District,
                    request.Address.City,
                    request.Address.State,
                    request.Address.PostalCode
                );

            var idPhone = 88;

            return new Entities.User
                (
                    request.Id ?? 0,
                    new Name(request.FirstName, request.MiddleName, request.LastName),
                    request.Email,
                    request.Cpf,
                    address,
                    request.Phones.Select(r => new Entities.Phone(++idPhone, r.LocalCode, r.Number)).ToList()
                );
        }

        internal static Entities.User GetUserToLogin(string email)
            => new Faker<Entities.User>()
                .CustomInstantiator(f =>
                    new Entities.User
                    (
                        f.Random.Int(1, 99),
                        new Name(f.Random.String(), f.Random.String(), f.Random.String()),
                        email,
                        f.Random.Long(111111111111, 666666666666).ToString()
                    )
                )
                .Generate(1)
                .First();

        internal static Entities.TokenModel GetTokenFromUser(Entities.User user)
            => new Faker<Entities.TokenModel>()
                .CustomInstantiator(f => new Entities.TokenModel(user.Id, user.Email, f.Random.String()))
                .Generate(1)
                .First();
    }
}