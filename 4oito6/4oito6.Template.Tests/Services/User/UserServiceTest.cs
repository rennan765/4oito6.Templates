using _4oito6.Template.Domain.Services.Contracts.Arguments.Request;
using _4oito6.Template.Domain.Services.Implementation;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Tests.Services.User.TestCases;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
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
    }
}