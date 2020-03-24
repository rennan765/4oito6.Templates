using System.Collections.Generic;
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
        {
            var list = new List<DataModel.UserPhone>();
            var dmUser = new DataModel.User(user.Id, user.Name.FirstName, user.Name.MiddleName, user.Name.LastName, user.Email, user.Cpf, user.Address.ToDataModel(), null);

            foreach (var p in phones)
            {
                var phone = p.Id > 0 ?
                    new DataModel.Phone(p.Id, p.LocalCode, p.Number) :
                    new DataModel.Phone(p.LocalCode, p.Number);

                list.Add(new DataModel.UserPhone(phone, dmUser));
            }

            return list;
        }
    }
}