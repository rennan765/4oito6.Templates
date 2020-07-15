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
        private TemplateContext _template;
        private ContactContext _contact;
        private IAsyncDbConnection _conn;
        private bool _disposedValue;

        private UnitOfWork()
        {
            _disposedValue = false;
        }

        public UnitOfWork(TemplateContext template, IAsyncDbConnection conn) : this()
        {
            _template = template ?? throw new ArgumentNullException(nameof(template));
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
        }

        public UnitOfWork(ContactContext contact, IAsyncDbConnection conn) : this()
        {
            _contact = contact ?? throw new ArgumentNullException(nameof(contact));
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
        }

        public void BeginTransaction(DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.TemplateContext:
                    _template?.Database.BeginTransaction();
                    break;

                case DataSource.ContactContext:
                    _contact?.Database.BeginTransaction();
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
                    _template?.Database.CommitTransaction();
                    break;

                case DataSource.ContactContext:
                    _contact?.Database.BeginTransaction();
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
                    _template?.Database.RollbackTransaction();
                    break;

                case DataSource.ContactContext:
                    _contact?.Database.BeginTransaction();
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
                    _template?.Dispose();
                    _template = null;

                    _contact?.Dispose();
                    _contact = null;

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
                    return _template?.SaveChangesAsync();

                case DataSource.ContactContext:
                    return _contact?.SaveChangesAsync();

                case DataSource.Dapper:
                default:
                    throw new ArgumentException($"{nameof(dataSource)} is invalid.");
            }
        }
    }
}