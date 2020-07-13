using _4oito6.Contact.Domain.Interfaces.Repositories;
using _4oito6.Contact.Domain.Views;
using _4oito6.Contact.Infra.Data.Context;
using _4oito6.Contact.Infra.Data.Context.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Repositories
{
    public class ViewAddressRepository : IViewAddressRepository
    {
        private ContactContext _context;
        private bool _disposedValue;

        public ViewAddressRepository(ContactContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IQueryable<ViewAddress>> GetAsync(Expression<Func<ViewAddress, bool>> where)
            => Task.FromResult
            (
                _context.ViewAddress.FromSqlRaw(SqlCommandSource.Phone).AsNoTracking().Where(where)
            );

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                    _context = null;
                }

                _disposedValue = true;
            }
        }

        ~ViewAddressRepository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}