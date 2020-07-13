using _4oito6.Contact.Domain.Views;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Interfaces.Repositories
{
    //TODO Move to a contracts project
    //TODO Create a ViewRepositoryBase
    public interface IViewAddressRepository : IDisposable
    {
        Task<IQueryable<ViewAddress>> GetAsync(Expression<Func<ViewAddress, bool>> where);
    }
}