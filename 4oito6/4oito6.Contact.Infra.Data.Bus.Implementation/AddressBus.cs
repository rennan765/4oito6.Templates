using _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Contact.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Implementation
{
    public class AddressBus : BusBase, IAddressBus
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ITokenBuilderService _tokenBuilderService;

        public AddressBus(IUnitOfWork unit, IAddressRepository addressRepository, ITokenBuilderService tokenBuilderService)
            : base(unit, new IDisposable[] { addressRepository })
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _tokenBuilderService = tokenBuilderService ?? throw new ArgumentNullException(nameof(tokenBuilderService));
        }

        public async Task<IList<Address>> GetByDistrictAndCityAsync(string district, string city)
            => (await _addressRepository.ListAsync(a => a.District == district & a.City == city).ConfigureAwait(false))
                .Select(a => a.ToDomainModel())
                .ToList();

        public async Task<Address> GetByUserAsync()
        {
            var token = _tokenBuilderService.GetToken();

            var address = await _addressRepository.GetAsync(a => a.Users.Any(u => u.Id == token.Id)).ConfigureAwait(false);

            if (address == null)
            {
                return null;
            }

            return address.ToDomainModel();
        }
    }
}