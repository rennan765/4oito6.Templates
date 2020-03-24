using DataModel = _4oito6.Template.Infra.Data.Model.Entities;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Mapper
{
    public static class AddressMapper
    {
        public static DomainModel.Address ToDomainModel(this DataModel.Address address)
            => new DomainModel.Address(address.Id, address.Street, address.Number, address.Complement, address.District, address.City, address.State, address.PostalCode);

        public static DataModel.Address ToDataModel(this DomainModel.Address address)
            => new DataModel.Address
            {
                Id = address.Id,
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                District = address.District,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            };
    }
}