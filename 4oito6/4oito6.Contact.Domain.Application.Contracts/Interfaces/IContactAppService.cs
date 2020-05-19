using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Application.Contracts.Interfaces
{
    public interface IContactAppService : IAppServiceBase
    {
        Task<ResponseMessage<IList<PhoneResponse>>> GetUserPhonesAsync();

        Task<ResponseMessage<IList<PhoneResponse>>> GetPhonesByLocalCodeAsync(string localCode);

        Task<ResponseMessage<AddressResponse>> GetUserAddressAsync();

        Task<ResponseMessage<IList<AddressResponse>>> GetAddressByDistrictAndCityAsync(string district, string city);

        Task<ResponseMessage<AddressFromPostalCodeResponse>> GetFromWebServiceByPostalCodeAsync(string postalCode);
    }
}