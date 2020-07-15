using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Application.Contracts.Interfaces
{
    public interface IContactAppService : IAppServiceBase
    {
        Task<IQueryable<ViewPhone>> GetUserPhonesAsync();

        Task<IQueryable<ViewPhone>> GetPhonesByLocalCodeAsync(string localCode);

        Task<IQueryable<ViewAddress>> GetUserAddressAsync();

        Task<IQueryable<ViewAddress>> GetAddressByDistrictAndCityAsync(string district, string city);

        Task<AddressFromPostalCodeResponse> GetFromWebServiceByPostalCodeAsync(string postalCode);
    }
}