using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs;

namespace _4oito6.Template.Domain.Specs
{
    public class PhoneSpecs : BusinessSpec
    {
        public PhoneSpecs(Phone entity) : base(entity)
        {
            if (string.IsNullOrEmpty(entity.LocalCode))
                AddMessage(BusinessSpecStatus.InvalidInputs, PhoneSpecMessages.DddObrigatorio);
            else if (entity.LocalCode.Length != 2)
                AddMessage(BusinessSpecStatus.InvalidInputs, PhoneSpecMessages.DddInvalido);

            if (string.IsNullOrEmpty(entity.Number))
                AddMessage(BusinessSpecStatus.InvalidInputs, PhoneSpecMessages.NumeroObrigatorio);
            else if (entity.Number.Length < 8 || entity.Number.Length > 9)
                AddMessage(BusinessSpecStatus.InvalidInputs, PhoneSpecMessages.NumeroInvalido);
        }
    }
}