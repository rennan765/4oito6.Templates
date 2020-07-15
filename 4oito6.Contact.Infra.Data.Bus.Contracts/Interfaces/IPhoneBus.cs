using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Infra.Data.Bus.Core.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IPhoneBus : IBusBase
    {
        Task<IQueryable<ViewPhone>> GetByUserAsync();

        Task<IQueryable<ViewPhone>> GetByLocalCodeAsync(string localCode);
    }
}