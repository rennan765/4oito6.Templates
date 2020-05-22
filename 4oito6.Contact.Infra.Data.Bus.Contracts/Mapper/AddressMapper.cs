using DataModel = _4oito6.Template.Infra.Data.Model.Entities;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Contact.Infra.Data.Bus.Contracts.Mapper
{
    public static class AddressMapper
    {
        public static DomainModel.Address ToDomainModel(this DataModel.Address dto)
            => new DomainModel.Address
            (
                id: dto.Id,
                street: dto.Street,
                number: dto.Number,

                complement: dto.Complement,
                district: dto.District,
                city: dto.City,

                state: dto.State,
                postalCode: dto.PostalCode
            );
    }
}