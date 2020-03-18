using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Domain.Model.ValueObjects;

namespace _4oito6.Template.Domain.Specs
{
    public class UserSpec : BusinessSpec<User>
    {
        public UserSpec(User entity) : base(entity)
        {
            ValidateName(Entity.Name);

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