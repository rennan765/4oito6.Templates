using _4oito6.Contact.Domain.Views;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Contact.Domain.Interfaces.Repositories
{
    //TODO Move to a contracts project
    //TODO Create a ViewRepositoryBase
    public interface IViewPhoneRepository : IDisposable
    {
        Task<IQueryable<ViewPhone>> GetAsync(Expression<Func<ViewPhone, bool>> where);
    }
}