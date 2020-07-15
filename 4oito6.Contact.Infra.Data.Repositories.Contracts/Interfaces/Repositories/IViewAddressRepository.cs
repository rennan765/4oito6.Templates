using _4oito6.Contact.Domain.Model.Views;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Repositories.Contracts.Interfaces.Repositories
{
    //TODO Create a ViewRepositoryBase
    public interface IViewAddressRepository : IDisposable
    {
        Task<IQueryable<ViewAddress>> GetAsync(Expression<Func<ViewAddress, bool>> where);
    }
}