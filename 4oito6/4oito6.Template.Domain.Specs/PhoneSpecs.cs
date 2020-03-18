using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Domain.Specs
{
    public class PhoneSpecs : BusinessSpec<Phone>
    {
        public PhoneSpecs(Phone entity) : base(entity)
        {
            if (string.IsNullOrEmpty(Entity.LocalCode))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O DDD é obrigatório.");
            else if (entity.LocalCode.Length != 2)
                AddMessage(BusinessSpecStatus.InvalidInputs, "O DDD precisa ter 2 caracteres.");

            if (string.IsNullOrEmpty(Entity.Number))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O número é obrigatório.");
            else if (entity.Number.Length < 8 || entity.Number.Length > 9)
                AddMessage(BusinessSpecStatus.InvalidInputs, "O DDD precisa ter 8 ou 9 caracteres.");
        }
    }
}