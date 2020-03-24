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

            var user = new DataModel.User
            {
                Id = entity.Id,
                FirstName = entity.Name.FirstName,
                MiddleName = entity.Name.MiddleName,
                LastName = entity.Name.LastName,
                Email = entity.Email,
                Cpf = entity.Cpf,
                Phones = phones
            };

            if ((address?.Id ?? 0) > 0)
                user.IdAddress = address?.Id;
            else
                user.Address = address;

            return user;
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