using System.Collections.Generic;
using System.Linq;
using DataModel = _4oito6.Template.Infra.Data.Model.Entities;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Mapper
{
    public static class PhoneMapper
    {
        public static DomainModel.Phone ToDomainModel(this DataModel.Phone phone)
            => new DomainModel.Phone(phone.Id, phone.LocalCode, phone.Number);

        public static DomainModel.Phone ToDomainModel(this DataModel.UserPhone phone)
            => new DomainModel.Phone(phone.IdPhone, phone.Phone?.LocalCode, phone.Phone?.Number);

        public static IList<DataModel.UserPhone> ToDataModel(this IList<DomainModel.Phone> phones, DomainModel.User user)
            => phones.Select(p => new DataModel.UserPhone(user.Id, p.Id)).ToList();
    }
}