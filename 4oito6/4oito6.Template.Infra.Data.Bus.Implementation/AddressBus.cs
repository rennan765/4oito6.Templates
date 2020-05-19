using _4oito6.Infra.Data.Bus.Core.Implementation;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Bus.Contracts.Mapper;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using System;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Bus.Implementation
{
    public class AddressBus : BusBase, IAddressBus
    {
        private readonly IAddressRepository _addressRepository;

        public AddressBus(IUnitOfWork unit, IAddressRepository addressRepository)
            : base(unit, new IDisposable[] { addressRepository })
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<Address> GetByInfoAsync(string street, string number, string complement, string district, string city, string state, string postalCode)
        {
            var address = await _addressRepository
                .GetAsync
                (
                    a =>
                        a.Street == street && a.Number == number & a.Complement == complement &&
                        a.District == district & a.City == city && a.State == state &&
                        a.PostalCode == postalCode
                )
                .ConfigureAwait(false);

            return address?.ToDomainModel();
        }
    }
}