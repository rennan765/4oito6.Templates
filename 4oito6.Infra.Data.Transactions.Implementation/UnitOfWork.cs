using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Infra.Data.Transactions.Contracts.Enum;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Transactions.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private IAsyncDbConnection _conn;
        private bool _disposedValue;

        public UnitOfWork(DbContext context, IAsyncDbConnection conn)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
            _disposedValue = false;
        }

        public void BeginTransaction(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.EntityFramework:
                    _context.Database.BeginTransaction();
                    break;

                case DataSource.Dapper:
                    _conn.BeginTransaction();
                    break;
            }
        }

        public void CloseDapperConnection()
        {
            _conn.Close();
        }

        public void Commit(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.EntityFramework:
                    _context.Database.CommitTransaction();
                    break;

                case DataSource.Dapper:
                    _conn.Transaction.Commit();
                    break;
            }
        }

        public void OpenDapperConnection()
        {
            _conn.Open();
        }

        public void Rollback(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.EntityFramework:
                    _context.Database.RollbackTransaction();
                    break;

                case DataSource.Dapper:
                    _conn.Transaction.Rollback();
                    break;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                    _context = null;

                    _conn?.Dispose();
                    _conn = null;
                }

                _disposedValue = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveEntityChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}