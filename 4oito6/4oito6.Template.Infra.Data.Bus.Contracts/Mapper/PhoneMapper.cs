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
            var dmUser = new DataModel.User
            {
                FirstName = user.Name.FirstName,
                MiddleName = user.Name.MiddleName,
                LastName = user.Name.LastName,
                Email = user.Email,
                Cpf = user.Cpf
            };

            if (user.Id > 0)
                dmUser.Id = user.Id;

            foreach (var p in phones)
            {
                var up = new DataModel.UserPhone();

                if (p.Id > 0)
                    up.IdPhone = p.Id;
                else
                    up.Phone = new DataModel.Phone
                    {
                        LocalCode = p.LocalCode,
                        Number = p.Number
                    };

                if (user.Id > 0)
                    up.IdUser = user.Id;
                else
                    up.User = dmUser;

                list.Add(up);
            }

            return list;
        }
    }
}