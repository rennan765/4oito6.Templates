using _4oito6.Template.Domain.Model.ValueObjects;
using System.Linq;
using DataModel = _4oito6.Template.Infra.Data.Model.Entities;
using DomainModel = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Mapper
{
    public static class UserMapper
    {
        public static DataModel.User ToDataModel(this DomainModel.User entity)
        {
            var phones = entity.Phones.Select(p => new DataModel.UserPhone(entity.Id, p.Id)).ToList();

            var address = entity.Address != null ?
                new DataModel.Address(entity.Address.Id, entity.Address.Street, entity.Address.Number, entity.Address.Complement, entity.Address.District, entity.Address.City, entity.Address.State, entity.Address.PostalCode) :
                null;

            return new DataModel.User(entity.Id, entity.Name.FirstName, entity.Name.MiddleName, entity.Name.LastName, entity.Email, entity.Cpf, address.Id, address, phones);
        }

        public static DomainModel.User ToDomainModel(this DataModel.User dataModel)
        {
            var phones = dataModel
                .Phones.Select(pp => new DomainModel.Phone(pp.IdPhone, pp.Phone?.LocalCode, pp.Phone?.Number))
                .ToList();

            var address = dataModel.Address != null ?
                new DomainModel.Address(dataModel.Address.Id, dataModel.Address.Street, dataModel.Address.Number, dataModel.Address.Complement, dataModel.Address.District, dataModel.Address.City, dataModel.Address.State, dataModel.Address.PostalCode) :
                null;

            var name = new Name(dataModel.FirstName, dataModel.MiddleName, dataModel.LastName);

            return new DomainModel.User(dataModel.Id, name, dataModel.Email, dataModel.Cpf, address, phones);
        }
    }
}