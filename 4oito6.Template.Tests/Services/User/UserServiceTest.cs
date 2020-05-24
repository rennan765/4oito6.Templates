using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Template.Domain.Services.Implementation;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Services;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs.User;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static _4oito6.Template.Tests.Services.User.TestCases.UserTestCases;
using Entities = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Tests.Services.User
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "CreateUserAsync_EmailExists")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_EmailExists()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.EmailExists);
            var expectedMessages = new string[] { UserServiceMessages.EmailJaCadastrado };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(true)
                .Verifiable();

            //Act
            var response = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.Conflict);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_InvalidEmail")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_InvalidEmail()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = new UserRequest
            {
                Email = "invalid email",
                Cpf = "45237248063",

                FirstName = "Fulano",
                MiddleName = "de Tal",
                LastName = "da Silva",
            };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            var expectedMessages = new string[] { UserSpecMessages.EmailInvalido };

            //Act
            var result = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            result.Should().BeNull();
            serviceMock.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.BadRequest);
            comparison.Compare(expectedMessages, serviceMock.GetMessages()).AreEqual.Should().BeTrue();
        }

        [Fact(DisplayName = "CreateUserAsync_InvalidCpf")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_InvalidCpf()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = new UserRequest
            {
                Email = "teste@teste.com",
                Cpf = "0",

                FirstName = "Fulano",
                MiddleName = "de Tal",
                LastName = "da Silva",
            };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            var expectedMessages = new string[] { UserSpecMessages.CpfInvalido };

            //Act
            var result = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            result.Should().BeNull();
            serviceMock.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.BadRequest);
            comparison.Compare(expectedMessages, serviceMock.GetMessages()).AreEqual.Should().BeTrue();
        }

        [Fact(DisplayName = "CreateUserAsync_InvalidAddress")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_InvalidAddress()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.InvalidAddress);
            var expectedMessages = new string[] { AddressSpecMessages.BairroObrigatorio, AddressSpecMessages.EstadoInvalido };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Entities.Address)null)
                .Verifiable();

            //Act
            var response = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.BadRequest);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_InvalidPhone")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_InvalidPhone()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.InvalidPhone);
            var tuples = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();
            var expectedMessages = new string[] { PhoneSpecMessages.DddInvalido, PhoneSpecMessages.NumeroInvalido, PhoneSpecMessages.DddInvalido, PhoneSpecMessages.NumeroInvalido };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup(b => b.GetByNumbersAsync(It.Is<IList<Tuple<string, string>>>(ts => comparison.Compare(ts, tuples).AreEqual)))
                .ReturnsAsync(new List<Entities.Phone>())
                .Verifiable();

            //Act
            var response = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.BadRequest);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_InvalidUser")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_InvalidUser()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.InvalidUser);
            var expectedMessages = new string[] { UserSpecMessages.PrimeiroNomeObrigatorio };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            //Act
            var response = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.BadRequest);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_PerfectWay_NoPhonesOrAddressInDb")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_PerfectWay_NoPhonesOrAddressInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.PerfectWay);
            var user = GetUserByRequest(request);

            var userWithId = GetUserWithId(user);
            var expectedResult = GetResponseByUser(userWithId);

            var phones = request.Phones
                .Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number))
                .ToList();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Entities.Address)null)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phones).AreEqual)
                    )
                )
                .ReturnsAsync(new List<Entities.Phone>())
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.CreateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, user).AreEqual)))
                .ReturnsAsync(userWithId)
                .Verifiable();

            //Act
            var result = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_PerfectWay_HavePhoneInDb")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_PerfectWay_HavePhoneInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.PerfectWay);
            var phonesRequest = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();

            var phonesFromDb = GetPhonesFromDb(request);
            var user = GetUserByRequest(request, phonesFromDb);

            var userWithId = GetUserWithId(user);
            var expectedResult = GetResponseByUser(userWithId);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Entities.Address)null)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phonesRequest).AreEqual)
                    )
                )
                .ReturnsAsync(phonesFromDb)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.CreateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, user).AreEqual)))
                .ReturnsAsync(userWithId)
                .Verifiable();

            //Act
            var result = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_PerfectWay_HaveAddressInDb")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_PerfectWay_HaveAddressInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.PerfectWay);
            var phonesRequest = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();

            var addressFromDb = GetAddressFromDb(request);
            var user = GetUserByRequest(request, null, addressFromDb);

            var userWithId = GetUserWithId(user);
            var expectedResult = GetResponseByUser(userWithId);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync(addressFromDb)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phonesRequest).AreEqual)
                    )
                )
                .ReturnsAsync(new List<Entities.Phone>())
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.CreateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, user).AreEqual)))
                .ReturnsAsync(userWithId)
                .Verifiable();

            //Act
            var result = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "CreateUserAsync_PerfectWay_HavePhoneAndAddressInDb")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_PerfectWay_HavePhoneAndAddressInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.PerfectWay);
            var phonesRequest = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();

            var addressFromDb = GetAddressFromDb(request);
            var phonesFromDb = GetPhonesFromDb(request);
            var user = GetUserByRequest(request, phonesFromDb, addressFromDb);

            var userWithId = GetUserWithId(user);
            var expectedResult = GetResponseByUser(userWithId);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, null))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync(addressFromDb)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phonesRequest).AreEqual)
                    )
                )
                .ReturnsAsync(phonesFromDb)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.CreateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, user).AreEqual)))
                .ReturnsAsync(userWithId)
                .Verifiable();

            //Act
            var result = await serviceMock.CreateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_UserNotFound")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_UserNotFound()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.EmailExists);
            var expectedMessages = new string[] { UserServiceMessages.UsuarioNaoEncontrado };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync((Entities.User)null)
                .Verifiable();

            //Act
            var response = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.NotFound);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_NotLoggedUser")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_NotLoggedUser()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.EmailExists);
            var userDb = GetUserToUpdate(TestCase.EmailExists);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id + 1, "another@e.mail", null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            var expectedMessages = new string[] { UserServiceMessages.EditarProprioUsuario };

            //Act
            var response = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            (serviceMock.GetStatusCode() == HttpStatusCode.Forbidden).Should().BeTrue();
            comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_EmailExists")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_EmailExists()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.EmailExists);
            var userDb = GetUserToUpdate(TestCase.EmailExists);
            var expectedMessages = new string[] { UserServiceMessages.EmailJaCadastrado };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(true)
                .Verifiable();

            //Act
            var response = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.Conflict);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_InvalidEmail")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_InvalidEmail()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();

            var request = new UserRequest
            {
                Id = 8,
                Email = "invalid email",
                Cpf = "45237248063",

                FirstName = "Fulano",
                MiddleName = "de Tal",
                LastName = "da Silva",
            };

            var userDb = GetUserToUpdate(TestCase.EmailExists);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            var expectedMessages = new string[] { UserSpecMessages.EmailInvalido };

            //Act
            var result = await service.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            result.Should().BeNull();
            service.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.BadRequest);
            new CompareLogic().Compare(expectedMessages, service.GetMessages()).AreEqual.Should().BeTrue();
        }

        [Fact(DisplayName = "UpdateUserAsync_InvalidCpf")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_InvalidCpf()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();

            var request = new UserRequest
            {
                Id = 8,
                Email = "teste@teste.com",
                Cpf = "0",

                FirstName = "Fulano",
                MiddleName = "de Tal",
                LastName = "da Silva",
            };

            var userDb = GetUserToUpdate(TestCase.EmailExists);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            var expectedMessages = new string[] { UserSpecMessages.CpfInvalido };

            //Act
            var result = await service.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            result.Should().BeNull();
            service.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.BadRequest);
            new CompareLogic().Compare(expectedMessages, service.GetMessages()).AreEqual.Should().BeTrue();
        }

        [Fact(DisplayName = "UpdateUserAsync_InvalidAddress")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_InvalidAddress()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.InvalidAddress);
            var userDb = GetUserToUpdate(TestCase.InvalidAddress);
            var expectedMessages = new string[] { AddressSpecMessages.BairroObrigatorio, AddressSpecMessages.EstadoInvalido };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Entities.Address)null)
                .Verifiable();

            //Act
            var response = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.BadRequest);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_InvalidPhone")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_InvalidPhone()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.InvalidPhone);
            var userDb = GetUserToUpdate(TestCase.InvalidPhone);

            var tuples = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();
            var expectedMessages = new string[] { PhoneSpecMessages.DddInvalido, PhoneSpecMessages.NumeroInvalido, PhoneSpecMessages.DddInvalido, PhoneSpecMessages.NumeroInvalido };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup(b => b.GetByNumbersAsync(It.Is<IList<Tuple<string, string>>>(ts => comparison.Compare(ts, tuples).AreEqual)))
                .ReturnsAsync(new List<Entities.Phone>())
                .Verifiable();

            //Act
            var response = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.BadRequest);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_InvalidUser")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_InvalidUser()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.InvalidUser);
            var userDb = GetUserToUpdate(TestCase.InvalidUser);

            var tuples = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();
            var expectedMessages = new string[] { UserSpecMessages.PrimeiroNomeObrigatorio };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            //Act
            var response = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            serviceMock.IsSatisfied().Should().BeFalse();
            Assert.True(serviceMock.GetStatusCode() == HttpStatusCode.BadRequest);
            Assert.True(comparison.Compare(serviceMock.GetMessages(), expectedMessages).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_PerfectWay_NoPhonesOrAddressInDb")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_PerfectWay_NoPhonesOrAddressInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.PerfectWay);
            var userDb = GetUserToUpdate(TestCase.PerfectWay);
            var expectedResult = GetResponseByUser(userDb);

            var phones = request.Phones
                .Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number))
                .ToList();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Entities.Address)null)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phones).AreEqual)
                    )
                )
                .ReturnsAsync(new List<Entities.Phone>())
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.UpdateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, userDb).AreEqual)))
                .Verifiable();

            //Act
            var result = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_PerfectWay_HavePhoneInDb")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_PerfectWay_HavePhoneInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.PerfectWay);
            var userDb = GetUserToUpdate(TestCase.PerfectWay);

            var phonesDb = GetPhonesFromDb(request);
            var expectedResult = GetResponseByUser(userDb);

            userDb.ChangePhones
            (
                request.Phones
                    .Where(p => !phonesDb.Any(db => db.LocalCode == p.LocalCode & db.Number == p.Number))
                    .Select(p => new Entities.Phone(p.LocalCode, p.Number))
                    .Concat(phonesDb)
                    .ToList()
            );

            var phones = request.Phones
                .Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number))
                .ToList();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Entities.Address)null)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phones).AreEqual)
                    )
                )
                .ReturnsAsync(phonesDb)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.UpdateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, userDb).AreEqual)))
                .Verifiable();

            //Act
            var result = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_PerfectWay_HaveAddressInDb")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_PerfectWay_HaveAddressInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.PerfectWay);
            var userDb = GetUserToUpdate(TestCase.PerfectWay);

            var addressDb = GetAddressFromDb(request);
            userDb.ChangeAddress(addressDb);
            var expectedResult = GetResponseByUser(userDb);

            var phones = request.Phones
                .Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number))
                .ToList();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync(addressDb)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phones).AreEqual)
                    )
                )
                .ReturnsAsync(new List<Entities.Phone>())
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.UpdateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, userDb).AreEqual)))
                .Verifiable();

            //Act
            var result = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "UpdateUserAsync_PerfectWay_HavePhoneAndAddressInDb")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_PerfectWay_HavePhoneAndAddressInDb()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.PerfectWay);
            var userDb = GetUserToUpdate(TestCase.PerfectWay);

            var addressDb = GetAddressFromDb(request);
            userDb.ChangeAddress(addressDb);

            var phonesDb = GetPhonesFromDb(request);
            var expectedResult = GetResponseByUser(userDb);

            userDb.ChangePhones
            (
                request.Phones
                    .Where(p => !phonesDb.Any(db => db.LocalCode == p.LocalCode & db.Number == p.Number))
                    .Select(p => new Entities.Phone(p.LocalCode, p.Number))
                    .Concat(phonesDb)
                    .ToList()
            );

            var phonesRequest = request.Phones.Select(phone => new Tuple<string, string>(phone.LocalCode, phone.Number)).ToList();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
                .Verifiable();

            var token = new Entities.TokenModel(userDb.Id, userDb.Email, null);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetTokenAsync())
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email, request.Id))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync(addressDb)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup
                (
                    b => b.GetByNumbersAsync
                    (
                        It.Is<IList<Tuple<string, string>>>(p => comparison.Compare(p, phonesRequest).AreEqual)
                    )
                )
                .ReturnsAsync(phonesDb)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.UpdateUserAsync(It.Is<Entities.User>(u => comparison.Compare(u, userDb).AreEqual)))
                .Verifiable();

            //Act
            var result = await serviceMock.UpdateUserAsync(request).ConfigureAwait(false);

            //Assert
            serviceMock.IsSatisfied().Should().BeTrue();
            Assert.True(comparison.Compare(expectedResult, result).AreEqual);
            mocker.Verify();
        }

        [Fact(DisplayName = "LoginAsync_ShouldReturnNull_DueToInvalidEmail")]
        [Trait("LoginAsync", "UserService")]
        public async Task LoginAsync_ShouldReturnNull_DueToInvalidEmail()
        {
            //Assert
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();

            var request = new LoginRequest
            {
                Email = "teste@teste.com"
            };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByEmailAsync(request.Email))
                .ReturnsAsync((Entities.User)null)
                .Verifiable();

            //Act
            var result = await service.LoginAsync(request).ConfigureAwait(false);

            //Assert
            service.IsSatisfied().Should().BeFalse();
            result.Should().BeNull();
            mocker.Verify();
        }

        [Fact(DisplayName = "LoginAsync_ShouldExecuteSuccessfully")]
        [Trait("LoginAsync", "UserService")]
        public async Task LoginAsync_ShouldExecuteSuccessfully()
        {
            //Assert
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();

            var request = new LoginRequest
            {
                Email = "teste@teste.com"
            };

            var user = GetUserToLogin(request.Email);
            var tokenModel = GetTokenFromUser(user);

            var expectedResult = new LoginResponse(tokenModel.Token, tokenModel.IdUser, tokenModel.Email, tokenModel.RefreshToken);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByEmailAsync(request.Email))
                .ReturnsAsync(user)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.LoginAsync(It.Is<Entities.User>(u => comparison.Compare(u, user).AreEqual)))
                .ReturnsAsync(tokenModel)
                .Verifiable();

            //Act
            var result = await service.LoginAsync(request).ConfigureAwait(false);

            //Assert
            service.IsSatisfied().Should().BeTrue();
            comparison.Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "RefreshLoginAsync_ShouldReturnNull_DueToTokenExpired")]
        [Trait("RefreshLoginAsync", "UserService")]
        public async Task RefreshLoginAsync_ShouldReturnNull_DueToTokenExpired()
        {
            //Assert
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();
            var refreshToken = "abc";

            mocker.GetMock<IUserBus>()
                .Setup(b => b.IsRefreshTokenValid(refreshToken))
                .ReturnsAsync(false)
                .Verifiable();

            var expectedMessages = new string[] { UserServiceMessages.TokenExpirado };

            //Act
            var response = await service.RefreshLoginAsync(refreshToken).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            service.IsSatisfied().Should().BeFalse();
            (service.GetStatusCode() == HttpStatusCode.Unauthorized).Should().BeTrue();
            new CompareLogic().Compare(service.GetMessages(), expectedMessages).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "RefreshLoginAsync_ShouldReturnNull_DueToUserNotFound")]
        [Trait("RefreshLoginAsync", "UserService")]
        public async Task RefreshLoginAsync_ShouldReturnNull_DueToUserNotFound()
        {
            //Assert
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();

            var user = GetUserToLogin("teste@gmail.com");
            var token = GetRefreshTokenFromIdUser(user.Id);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.IsRefreshTokenValid(token.RefreshToken))
                .ReturnsAsync(true)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetRefreshTokenAsync(token.RefreshToken))
                .ReturnsAsync(token)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(user.Id))
                .ReturnsAsync((Entities.User)null)
                .Verifiable();

            var expectedMessages = new string[] { UserServiceMessages.NomeUsuarioInvalido };

            //Act
            var response = await service.RefreshLoginAsync(token.RefreshToken).ConfigureAwait(false);

            //Assert
            response.Should().BeNull();
            service.IsSatisfied().Should().BeFalse();
            (service.GetStatusCode() == HttpStatusCode.Forbidden).Should().BeTrue();
            new CompareLogic().Compare(service.GetMessages(), expectedMessages).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "RefreshLoginAsync_ShouldReturnResponse")]
        [Trait("RefreshLoginAsync", "UserService")]
        public async Task RefreshLoginAsync_ShouldReturnResponse()
        {
            //Assert
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<UserService>();

            var user = GetUserToLogin("teste@gmail.com");
            var refreshToken = GetRefreshTokenFromIdUser(user.Id);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.IsRefreshTokenValid(refreshToken.RefreshToken))
                .ReturnsAsync(true)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetRefreshTokenAsync(refreshToken.RefreshToken))
                .ReturnsAsync(refreshToken)
                .Verifiable();

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(refreshToken.IdUser))
                .ReturnsAsync(user)
                .Verifiable();

            var token = GetTokenFromUser(user);

            mocker.GetMock<IUserBus>()
                .Setup(b => b.RefreshLoginAsync(refreshToken.RefreshToken, user))
                .ReturnsAsync(token)
                .Verifiable();

            var expectedResult = new LoginResponse(token.Token, token.IdUser, token.Email, token.RefreshToken);

            //Act
            var result = await service.RefreshLoginAsync(refreshToken.RefreshToken).ConfigureAwait(false);

            //Assert
            comparison.Compare(expectedResult, result).AreEqual.Should().BeTrue();
            service.IsSatisfied().Should().BeTrue();
            (service.GetStatusCode() == HttpStatusCode.OK).Should().BeTrue();
            mocker.Verify();
        }
    }
}