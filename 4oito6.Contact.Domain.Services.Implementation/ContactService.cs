using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Domain.Services.Contracts.Interfaces;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Domain.Services.Core.Implementation.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Services.Implementation
{
    public class ContactService : ServiceBase, IContactService
    {
        private readonly IPhoneBus _phoneBus;
        private readonly IAddressBus _addressBus;
        private readonly IPostalCodeClientService _postalCode;

        public ContactService(IPhoneBus phoneBus, IAddressBus addressBus, IPostalCodeClientService postalCode)
            : base(new IDisposable[] { phoneBus, addressBus, postalCode })
        {
            _phoneBus = phoneBus ?? throw new ArgumentNullException(nameof(phoneBus));
            _addressBus = addressBus ?? throw new ArgumentNullException(nameof(addressBus));
            _postalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        }

        public Task<IQueryable<ViewAddress>> GetAddressByDistrictAndCityAsync(string district, string city)
            => _addressBus.GetByDistrictAndCityAsync(district, city);

        public Task<AddressFromPostalCodeResponse> GetFromWebServiceByPostalCodeAsync(string postalCode)
            => _postalCode.GetAddressAsync(postalCode);

        public Task<IQueryable<ViewPhone>> GetPhonesByLocalCodeAsync(string localCode)
            => _phoneBus.GetByLocalCodeAsync(localCode);

        public Task<IQueryable<ViewAddress>> GetUserAddressAsync()
            => _addressBus.GetByUserAsync();

        public Task<IQueryable<ViewPhone>> GetUserPhonesAsync()
            => _phoneBus.GetByUserAsync();
    }
}