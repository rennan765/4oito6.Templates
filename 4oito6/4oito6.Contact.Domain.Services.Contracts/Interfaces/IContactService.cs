using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Services.Contracts.Interfaces
{
    public interface IContactService : IServiceBase
    {
        Task<IList<AddressResponse>> GetAddressByDistrictAndCityAsync(string district, string city);

        Task<IList<PhoneResponse>> GetPhonesByLocalCodeAsync(string localCode);

        Task<IList<PhoneResponse>> GetUserPhonesAsync();

        Task<AddressResponse> GetUserAddressAsync();
    }
}