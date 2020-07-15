using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Repositories.Contracts.Interfaces.Repositories;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Implementation
{
    public class AddressBus : BusBase, IAddressBus
    {
        private readonly IViewAddressRepository _addressRepository;
        private readonly ITokenBuilderService _tokenBuilderService;

        public AddressBus(IUnitOfWork unit, IViewAddressRepository addressRepository, ITokenBuilderService tokenBuilderService)
            : base(unit, new IDisposable[] { addressRepository })
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _tokenBuilderService = tokenBuilderService ?? throw new ArgumentNullException(nameof(tokenBuilderService));
        }

        public Task<IQueryable<ViewAddress>> GetByDistrictAndCityAsync(string district, string city)
            => _addressRepository.GetAsync(a => a.District == district && a.City == city);

        public async Task<IQueryable<ViewAddress>> GetByUserAsync()
        {
            var tokenModel = await _tokenBuilderService.GetTokenAsync().ConfigureAwait(false);

            return await _addressRepository.GetAsync(a => a.IdUser == tokenModel.Id).ConfigureAwait(false);
        }
    }
}