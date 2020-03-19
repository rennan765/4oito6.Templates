using _4oito6.Infra.Data.Transactions.Contracts.Enum;
using System;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Transactions.Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction(DataSource dataSource);

        void Commit(DataSource dataSource);

        void Rollback(DataSource dataSource);

        void OpenDapperConnection();

        void CloseDapperConnection();

        Task SaveEntityChangesAsync();
    }
}