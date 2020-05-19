using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Template.Domain.Model.ValueObjects;
using Entities = _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Domain.Specs.User
{
    public class UserSpec : BusinessSpec
    {
        public UserSpec(Entities.User entity) : base(entity)
        {
            ValidateName(entity.Name);

            //TODO: Insert Cpf validation
            //TODO: Insert Email validation
        }

        private void ValidateName(Name name)
        {
            if (string.IsNullOrEmpty(name.FirstName))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O primeiro nome é obrigatório.");

            if (string.IsNullOrEmpty(name.LastName))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O sobrenome é obrigatório.");
        }
    }
}