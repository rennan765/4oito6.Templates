using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Services.Contracts.Arguments.Response;
using _4oito6.Contact.Domain.Services.Contracts.Interfaces;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Implementation.Base;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Application.Implementation.Implmentation
{
    public class ContactAppService : AppServiceBase, IContactAppService
    {
        private readonly IContactService _contactService;

        public ContactAppService(IUnitOfWork unit, IContactService contactService)
            : base(unit, new IDisposable[] { contactService })
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        public async Task<ResponseMessage<IList<AddressResponse>>> GetAddressByDistrictAndCityAsync(string district, string city)
        {
            var data = await _contactService.GetAddressByDistrictAndCityAsync(district, city)
                .ConfigureAwait(false);

            return new ResponseMessage<IList<AddressResponse>>
            {
                Data = data,
                StatusCode = (int)HttpStatusCode.OK,
                TotalRows = data.Count()
            };
        }

        public async Task<ResponseMessage<AddressResponse>> GetUserAddressAsync()
        {
            var data = await _contactService.GetUserAddressAsync().ConfigureAwait(false);

            return new ResponseMessage<AddressResponse>
            {
                Data = data,
                StatusCode = (int)HttpStatusCode.OK,
                TotalRows = data != null ? 1 : 0
            };
        }

        public async Task<ResponseMessage<IList<PhoneResponse>>> GetPhonesByLocalCodeAsync(string localCode)
        {
            var data = await _contactService.GetPhonesByLocalCodeAsync(localCode).ConfigureAwait(false);

            return new ResponseMessage<IList<PhoneResponse>>
            {
                Data = data,
                StatusCode = (int)HttpStatusCode.OK,
                TotalRows = data.Count()
            };
        }

        public async Task<ResponseMessage<IList<PhoneResponse>>> GetUserPhonesAsync()
        {
            var data = await _contactService.GetUserPhonesAsync().ConfigureAwait(false);

            return new ResponseMessage<IList<PhoneResponse>>
            {
                Data = data,
                StatusCode = (int)HttpStatusCode.OK,
                TotalRows = data.Count()
            };
        }

        public async Task<ResponseMessage<AddressFromPostalCodeResponse>> GetFromWebServiceByPostalCodeAsync(string postalCode)
        {
            var data = await _contactService.GetFromWebServiceByPostalCodeAsync(postalCode).ConfigureAwait(false);

            return new ResponseMessage<AddressFromPostalCodeResponse>
            {
                Data = data,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}