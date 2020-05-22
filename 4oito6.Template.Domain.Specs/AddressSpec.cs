using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using _4oito6.Infra.CrossCutting.Extensions;
using _4oito6.Template.Domain.Model.Entities;
using _4oito6.Template.Infra.CrossCutting.Messages.Domain.Specs;

namespace _4oito6.Template.Domain.Specs
{
    public class AddressSpec : BusinessSpec
    {
        public AddressSpec(Address entity) : base(entity)
        {
            if (string.IsNullOrEmpty(entity.Street))
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.LogradouroObrigatorio);

            if (!string.IsNullOrEmpty(entity.Number) && !entity.Number.IsNumeric())
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.NumeroNaoNumerico);

            if (string.IsNullOrEmpty(entity.District))
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.BairroObrigatorio);

            if (string.IsNullOrEmpty(entity.City))
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.CidadeObrigatoria);

            if (string.IsNullOrEmpty(entity.State))
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.EstadoObrigatorio);
            else if (entity.State.Length != 2)
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.EstadoInvalido);

            if (string.IsNullOrEmpty(entity.PostalCode))
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.CepObrigatorio);
            else if (!entity.PostalCode.IsNumeric())
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.CepNaoNumerico);
            else if (entity.PostalCode.Length != 8)
                AddMessage(BusinessSpecStatus.InvalidInputs, AddressSpecMessages.CepInvalido);
        }
    }
}