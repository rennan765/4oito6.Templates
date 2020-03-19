using _4oito6.Template.Domain.Model.Entities;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IAddressBus : IDisposable
    {
        Address GetByInfo(string street, string number, string complement, string district, string city, string state, string postalCode);
    }
}