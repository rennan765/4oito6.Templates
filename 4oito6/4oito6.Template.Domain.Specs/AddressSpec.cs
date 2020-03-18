using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Infra.CrossCutting.Extensions;
using _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Domain.Specs
{
    public class AddressSpec : BusinessSpec<Address>
    {
        public AddressSpec(Address entity) : base(entity)
        {
            if (string.IsNullOrEmpty(Entity.Street))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O logradouro é obrigátório.");

            if (!string.IsNullOrEmpty(Entity.Number) && !Entity.Number.IsNumeric())
                AddMessage(BusinessSpecStatus.InvalidInputs, "Existe algum caractere não numérico no campo número.");

            if (!string.IsNullOrEmpty(Entity.District))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O bairro é obrigátório.");

            if (!string.IsNullOrEmpty(Entity.City))
                AddMessage(BusinessSpecStatus.InvalidInputs, "A cidade é obrigátória.");

            if (!string.IsNullOrEmpty(Entity.State))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O estado é obrigátório.");
            else if (Entity.State.Length != 2)
                AddMessage(BusinessSpecStatus.InvalidInputs, "O estado deve ser informado no  formato de sigla.");

            if (string.IsNullOrEmpty(Entity.PostalCode))
                AddMessage(BusinessSpecStatus.InvalidInputs, "O CEP é obrigátório.");
            else if (!Entity.PostalCode.IsNumeric())
                AddMessage(BusinessSpecStatus.InvalidInputs, "Existe algum caractere não numérico no campo CEP.");
            else if (Entity.PostalCode.Length != 8)
                AddMessage(BusinessSpecStatus.InvalidInputs, "O CEP precisa ter 8 caracteres.");
        }
    }
}