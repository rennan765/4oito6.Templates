using _4oito6.Contact.Infra.Data.Context;
using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Infra.Data.Transactions.Contracts.Enum;
using _4oito6.Infra.Data.Transactions.Contracts.Interfaces;
using _4oito6.Template.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Transactions.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private TemplateContext _templateContext;
        private ContactContext _contactContext;
        private IAsyncDbConnection _conn;
        private bool _disposedValue;

        public UnitOfWork(TemplateContext templateContext, ContactContext contactContext, IAsyncDbConnection conn)
            : this(templateContext, conn)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
        }

        public UnitOfWork(TemplateContext templateContext, IAsyncDbConnection conn)
        {
            _templateContext = templateContext ?? throw new ArgumentNullException(nameof(templateContext));
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
            _disposedValue = false;
        }

        public void BeginTransaction(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.TemplateContext:
                    _templateContext.Database.BeginTransaction();
                    break;

                case DataSource.ContactContext:
                    _contactContext.Database.BeginTransaction();
                    break;

                case DataSource.Dapper:
                    _conn.BeginTransaction();
                    break;

                default:
                    throw new ArgumentException($"{nameof(dataSource)} is invalid.");
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
                case DataSource.TemplateContext:
                    _templateContext.Database.CommitTransaction();
                    break;

                case DataSource.ContactContext:
                    _contactContext.Database.BeginTransaction();
                    break;

                case DataSource.Dapper:
                    _conn.Transaction.Commit();
                    break;

                default:
                    throw new ArgumentException($"{nameof(dataSource)} is invalid.");
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
                case DataSource.TemplateContext:
                    _templateContext.Database.RollbackTransaction();
                    break;

                case DataSource.ContactContext:
                    _contactContext.Database.BeginTransaction();
                    break;

                case DataSource.Dapper:
                    _conn.Transaction.Rollback();
                    break;

                default:
                    throw new ArgumentException($"{nameof(dataSource)} is invalid.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _templateContext?.Dispose();
                    _templateContext = null;

                    _contactContext?.Dispose();
                    _contactContext = null;

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

        public Task SaveEntityChangesAsync(DataSource dataSource = DataSource.TemplateContext)
        {
            switch (dataSource)
            {
                case DataSource.TemplateContext:
                    return _templateContext.SaveChangesAsync();

                case DataSource.ContactContext:
                    return _contactContext.SaveChangesAsync();

                default:
                    throw new ArgumentException($"{nameof(dataSource)} is invalid.");
            }
        }
    }
}