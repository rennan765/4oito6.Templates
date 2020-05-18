using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Contact.Domain.Services.Contracts.Interfaces;
using _4oito6.Contact.Domain.Services.Contracts.Mapper;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Domain.Services.Core.Implementation.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Services.Implementation
{
    public class ContactService : ServiceBase, IContactService
    {
        private readonly IPhoneBus _phoneBus;
        private readonly IAddressBus _addressBus;

        public ContactService(IPhoneBus phoneBus, IAddressBus addressBus)
            : base(new IDisposable[] { phoneBus, addressBus })
        {
            _phoneBus = phoneBus ?? throw new ArgumentNullException(nameof(phoneBus));
            _addressBus = addressBus ?? throw new ArgumentNullException(nameof(addressBus));
        }

        public async Task<IList<AddressResponse>> GetAddressByDistrictAndCityAsync(string district, string city)
            => (await _addressBus.GetByDistrictAndCityAsync(district, city).ConfigureAwait(false))
            .Select(a => a.ToAddressResponse())
            .ToList();

        public async Task<IList<PhoneResponse>> GetPhonesByLocalCodeAsync(string localCode)
            => (await _phoneBus.GetByLocalCodeAsync(localCode).ConfigureAwait(false))
                .Select(p => p.ToPhoneResponse())
                .ToList();

        public async Task<AddressResponse> GetUserAddressAsync()
        {
            var address = await _addressBus.GetByUserAsync().ConfigureAwait(false);

            if (address == null)
            {
                return null;
            }

            return address.ToAddressResponse();
        }

        public async Task<IList<PhoneResponse>> GetUserPhonesAsync()
            => (await _phoneBus.GetByUserAsync().ConfigureAwait(false))
                .Select(p => p.ToPhoneResponse())
                .ToList();
    }
}