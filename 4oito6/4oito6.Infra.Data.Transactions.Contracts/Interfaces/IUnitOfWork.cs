using _4oito6.Infra.Data.Core.Connection;
using _4oito6.Infra.Data.Transactions.Contracts.Enum;
using Microsoft.EntityFrameworkCore;
using System;

namespace _4oito6.Infra.Data.Transactions.Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        IAsyncDbConnection Connection { get; }

        void BeginTransaction(DataSource dataSource);

        void Commit(DataSource dataSource);

        void Rollback(DataSource dataSource);

        void OpenConnection(DataSource dataSource);

        void CloseConnection(DataSource dataSource);
    }
}