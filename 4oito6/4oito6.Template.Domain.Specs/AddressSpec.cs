using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Infra.CrossCutting.Extensions;
using _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Domain.Specs
{
    public class AddressSpec : BusinessSpec
    {
        public AddressSpec(Address entity) : base(entity)
        {
            if (string.IsNullOrEmpty(entity.Street))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O logradouro é obrigátório.");

            if (!string.IsNullOrEmpty(entity.Number) && !entity.Number.IsNumeric())
                AddMessage(BusinessSpecStatus.InvalidInputs, "Existe algum caractere não numérico no campo número.");

            if (string.IsNullOrEmpty(entity.District))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O bairro é obrigátório.");

            if (string.IsNullOrEmpty(entity.City))
                AddMessage(BusinessSpecStatus.InvalidInputs, "A cidade é obrigátória.");

            if (string.IsNullOrEmpty(entity.State))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O estado é obrigátório.");
            else if (entity.State.Length != 2)
                AddMessage(BusinessSpecStatus.InvalidInputs, "O estado deve ser informado no  formato de sigla.");

            if (string.IsNullOrEmpty(entity.PostalCode))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O CEP é obrigátório.");
            else if (!entity.PostalCode.IsNumeric())
                AddMessage(BusinessSpecStatus.InvalidInputs, "Existe algum caractere não numérico no campo CEP.");
            else if (entity.PostalCode.Length != 8)
                AddMessage(BusinessSpecStatus.InvalidInputs, "O CEP precisa ter 8 caracteres.");
        }
    }
}