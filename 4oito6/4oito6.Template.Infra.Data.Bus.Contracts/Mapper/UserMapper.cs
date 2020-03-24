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
            var phones = entity.Phones.ToDataModel(entity);
            var address = entity.Address?.ToDataModel();

            return new DataModel.User(entity.Id, entity.Name.FirstName, entity.Name.MiddleName, entity.Name.LastName, entity.Email, entity.Cpf, address, phones);
        }

        public static DomainModel.User ToDomainModel(this DataModel.User dataModel)
        {
            var phones = dataModel.Phones.Select(pp => pp.ToDomainModel()).ToList();
            var address = dataModel.Address?.ToDomainModel();
            var name = new Name(dataModel.FirstName, dataModel.MiddleName, dataModel.LastName);

            return new DomainModel.User(dataModel.Id, name, dataModel.Email, dataModel.Cpf, address, phones);
        }
    }
}