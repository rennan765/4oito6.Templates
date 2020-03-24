using _4oito6.Template.Domain.Services.Implementation;
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
            var expectedMessages = new string[] { "E-mail já cadastrado." };

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

        [Fact(DisplayName = "CreateUserAsync_InvalidAddress")]
        [Trait("CreateUserAsync", "UserService")]
        public async Task CreateUserAsync_InvalidAddress()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.InvalidAddress);
            var expectedMessages = new string[] { "O bairro é obrigátório.", "O estado deve ser informado no  formato de sigla." };

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
            var expectedMessages = new string[] { "O DDD precisa ter 2 caracteres.", "O DDD precisa ter 8 ou 9 caracteres.", "O DDD precisa ter 2 caracteres.", "O DDD precisa ter 8 ou 9 caracteres." };

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
            var expectedMessages = new string[] { "O primeiro nome é obrigatório." };

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

        [Fact(DisplayName = "UpdateUserAsync_UserNotFount")]
        [Trait("UpdateUserAsync", "UserService")]
        public async Task UpdateUserAsync_UserNotFount()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequestToUpdate(TestCase.EmailExists);
            var expectedMessages = new string[] { "Usuário não encontrado." };

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
            var expectedMessages = new string[] { "E-mail já cadastrado." };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.GetByIdAsync(request.Id ?? 0))
                .ReturnsAsync(userDb)
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
    }
}