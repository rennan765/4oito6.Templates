using _4oito6.Contact.Domain.Services.Contracts.Mapper;
using _4oito6.Contact.Domain.Services.Implementation;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Domain.Model.Entities;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static _4oito6.Contact.Tests.TestCases.AddressTestCases;

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

            var addresses = GetAddresses(district, city);
            var expectedResult = addresses.Select(a => a.ToAddressResponse()).ToList();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByDistrictAndCityAsync(district, city))
                .ReturnsAsync(addresses);

            //Act
            var result = await service.GetAddressByDistrictAndCityAsync(district, city).ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }

        [Fact(DisplayName = "GetUserAddressAsync_ShouldReturnNull")]
        [Trait("GetUserAddressAsync", "ContactService")]
        public async Task GetUserAddressAsync_ShouldReturnNull()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByUserAsync())
                .ReturnsAsync((Address)null)
                .Verifiable();

            //Act
            var result = await service.GetUserAddressAsync().ConfigureAwait(false);

            //Assert
            result.Should().BeNull();
            mocker.Verify();
        }

        [Fact(DisplayName = "GetUserAddressAsync_ShouldReturnResponse")]
        [Trait("GetUserAddressAsync", "ContactService")]
        public async Task GetUserAddressAsync_ShouldReturnResponse()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ContactService>();

            var address = GetAddresses(quantity: 1).FirstOrDefault();
            var expectedResult = address.ToAddressResponse();

            mocker.GetMock<IAddressBus>()
                .Setup(b => b.GetByUserAsync())
                .ReturnsAsync(address)
                .Verifiable();

            //Act
            var result = await service.GetUserAddressAsync().ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
        }
    }
}