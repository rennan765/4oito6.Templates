using _4oito6.Infra.Data.Bus.Core.Contracts;
using _4oito6.Template.Domain.Model.Entities;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IAddressBus : IBusBase
    {
        Task<Address> GetByInfoAsync(string street, string number, string complement, string district, string city, string state, string postalCode);
    }
}