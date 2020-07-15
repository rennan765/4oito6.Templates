using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Domain.Services.Core.Contracts.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Services.Contracts.Interfaces
{
    public interface IContactService : IServiceBase
    {
        Task<IQueryable<ViewAddress>> GetAddressByDistrictAndCityAsync(string district, string city);

        Task<IQueryable<ViewPhone>> GetPhonesByLocalCodeAsync(string localCode);

        Task<IQueryable<ViewPhone>> GetUserPhonesAsync();

        Task<IQueryable<ViewAddress>> GetUserAddressAsync();

        Task<AddressFromPostalCodeResponse> GetFromWebServiceByPostalCodeAsync(string postalCode);
    }
}