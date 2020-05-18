using DataModel = _4oito6.Template.Infra.Data.Model.Entities;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Contact.Infra.Data.Bus.Contracts.Mapper
{
    public static class PhoneMapper
    {
        public static DomainModel.Phone ToDomainModel(this DataModel.Phone dto)
            => new DomainModel.Phone
            (
                id: dto.Id,
                localCode: dto.LocalCode,
                number: dto.Number
            );
    }
}