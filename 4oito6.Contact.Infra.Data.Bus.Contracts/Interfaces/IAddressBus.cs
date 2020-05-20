using _4oito6.Infra.Data.Bus.Core.Contracts;
using _4oito6.Template.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IAddressBus : IBusBase
    {
        Task<Address> GetByUserAsync();

        Task<IList<Address>> GetByDistrictAndCityAsync(string district, string city);
    }
}