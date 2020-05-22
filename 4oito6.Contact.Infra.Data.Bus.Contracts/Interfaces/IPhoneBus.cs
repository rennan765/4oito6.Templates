using _4oito6.Infra.Data.Bus.Core.Contracts;
using _4oito6.Template.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IPhoneBus : IBusBase
    {
        Task<IList<Phone>> GetByUserAsync();

        Task<IList<Phone>> GetByLocalCodeAsync(string localCode);
    }
}