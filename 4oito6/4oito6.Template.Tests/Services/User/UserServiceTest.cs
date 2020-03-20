using _4oito6.Template.Domain.Model.Entities;
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

namespace _4oito6.Template.Tests.Services.User
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "CreateUserAsync_EmailExists")]
        [Trait("UserService", "Services Tests")]
        public async Task CreateUserAsync_EmailExists()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.EmailExists);
            var expectedMessages = new string[] { "E-mail já cadastrado." };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email))
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
        [Trait("UserService", "Services Tests")]
        public async Task CreateUserAsync_InvalidAddress()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var serviceMock = mocker.CreateInstance<UserService>();

            var request = GetRequest(TestCase.InvalidAddress);
            var expectedMessages = new string[] { "O bairro é obrigátório.", "O estado deve ser informado no  formato de sigla." };

            mocker.GetMock<IUserBus>()
                .Setup(b => b.ExistsEmailAsync(request.Email))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByInfoAsync(request.Address.Street, request.Address.Number, request.Address.Complement, request.Address.District, request.Address.City, request.Address.State, request.Address.PostalCode))
                .ReturnsAsync((Address)null)
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
        [Trait("UserService", "Services Tests")]
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
                .Setup(b => b.ExistsEmailAsync(request.Email))
                .ReturnsAsync(false)
                .Verifiable();

            mocker.GetMock<IPhoneBus>()
                .Setup(b => b.GetByNumbersAsync(It.Is<IList<Tuple<string, string>>>(ts => comparison.Compare(ts, tuples).AreEqual)))
                .ReturnsAsync(new List<Phone>())
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
    }
}