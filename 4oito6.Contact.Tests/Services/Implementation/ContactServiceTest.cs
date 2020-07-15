using _4oito6.Contact.Domain.Services.Implementation;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;
using static _4oito6.Contact.Tests.TestCases.AddressTestCases;
using static _4oito6.Contact.Tests.TestCases.PhoneTestCases;

namespace _4oito6.Contact.Tests.Services.Implementation
{
    public class ContactServiceTest
    {
        [Fact(DisplayName = "GetAddressByDistrictAndCityAsync_ShouldExecuteSuccessfully")]
        [Trait("GetAddressByDistrictAndCityAsync", "ContactService")]
        public async Task GetAddressByDistrictAndCityAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            var district = "Barreto";
            var city = "Niterói";

            var expectedResult = GetAddresses(district, city);

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByDistrictAndCityAsync(district, city))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            //Act
            var result = await service.GetAddressByDistrictAndCityAsync(district, city).ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "GetPhonesByLocalCodeAsync_ShouldExecuteSuccessfully")]
        [Trait("GetPhonesByLocalCodeAsync", "ContactService")]
        public async Task GetPhonesByLocalCodeAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            var localCode = "21";
            var expectedResult = GetPhones(localCode);

            mocker.GetMock<IPhoneBus>()
                .Setup(p => p.GetByLocalCodeAsync(localCode))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            //Act
            var result = await service.GetPhonesByLocalCodeAsync(localCode).ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "GetUserAddressAsync_ShouldExecuteSuccessfully")]
        [Trait("GetUserAddressAsync", "ContactService")]
        public async Task GetUserAddressAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            var expectedResult = GetAddresses(quantity: 1);

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByUserAsync())
                .ReturnsAsync(expectedResult)
                .Verifiable();

            //Act
            var result = await service.GetUserAddressAsync().ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "GetUserPhonesAsync_ShouldExecuteSuccessfully")]
        [Trait("GetPhonesByLocalCodeAsync", "ContactService")]
        public async Task GetUserPhonesAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            var localCode = "21";
            var expectedResult = GetPhones(localCode);

            mocker.GetMock<IPhoneBus>()
                .Setup(p => p.GetByUserAsync())
                .ReturnsAsync(expectedResult)
                .Verifiable();

            //Act
            var result = await service.GetUserPhonesAsync().ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "GetFromWebServiceByPostalCodeAsync_ShouldExecuteSuccessfully")]
        [Trait("GetFromWebServiceByPostalCodeAsync", "ContactService")]
        public async Task GetFromWebServiceByPostalCodeAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            var postalCode = "24030030";
            var expectedResult = new AddressFromPostalCodeResponse
            {
                Street = "Rua Doutor Froes da Cruz",
                District = "Centro",
                City = "Niterói",
                State = "RJ"
            };

            mocker.GetMock<IPostalCodeClientService>()
                .Setup(cs => cs.GetAddressAsync(postalCode))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            //Act
            var result = await service.GetFromWebServiceByPostalCodeAsync(postalCode).ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }
    }
}