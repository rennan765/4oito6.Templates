using _4oito6.Contact.Domain.Application.Contracts.Interfaces;
using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Domain.Services.Contracts.Interfaces;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using _4oito6.Domain.Application.Core.Implementation.Base;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Linq;
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

        public Task<IQueryable<ViewAddress>> GetAddressByDistrictAndCityAsync(string district, string city)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<ViewPhone>> GetPhonesByLocalCodeAsync(string localCode)
            => _contactService.GetPhonesByLocalCodeAsync(localCode);

        public Task<IQueryable<ViewAddress>> GetUserAddressAsync()
            => _contactService.GetUserAddressAsync();

        public Task<IQueryable<ViewPhone>> GetUserPhonesAsync()
            => _contactService.GetUserPhonesAsync();

        public Task<AddressFromPostalCodeResponse> GetFromWebServiceByPostalCodeAsync(string postalCode)
            => _contactService.GetFromWebServiceByPostalCodeAsync(postalCode);
    }
}