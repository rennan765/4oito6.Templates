using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Infra.Data.Bus.Core.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IAddressBus : IBusBase
    {
        Task<IQueryable<ViewAddress>> GetByUserAsync();

        Task<IQueryable<ViewAddress>> GetByDistrictAndCityAsync(string district, string city);
    }
}