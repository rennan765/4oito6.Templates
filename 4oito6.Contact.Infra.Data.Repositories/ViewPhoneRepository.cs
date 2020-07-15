using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.Data.Context;
using _4oito6.Contact.Infra.Data.Context.Command;
using _4oito6.Contact.Infra.Data.Repositories.Contracts.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _4oito6.Contact.Infra.Data.Repositories
{
    public class ViewPhoneRepository : IViewPhoneRepository
    {
        private ContactContext _context;
        private bool _disposedValue;

        public ViewPhoneRepository(ContactContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IQueryable<ViewPhone>> GetAsync(Expression<Func<ViewPhone, bool>> where)
            => Task.FromResult
            (
                _context.ViewPhone.FromSqlRaw(SqlCommandSource.Phone).AsNoTracking().Where(where)
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

        ~ViewPhoneRepository()
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