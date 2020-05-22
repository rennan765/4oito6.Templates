using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Core.Connection
{
    public interface IAsyncDbConnection : IDisposable
    {
        IDbTransaction Transaction { get; }

        string ConnectionString { get; }

        int ConnectionTimeOut { get; }

        string Database { get; }

        ConnectionState State { get; }

        Task<IEnumerable<dynamic>> QueryAsync(CommandDefinition command);

        Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command);

        Task<IEnumerable<dynamic>> QueryAsync(string sql, object parameters, IDbTransaction transaction = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters, IDbTransaction transaction = null);

        Task<int> ExecuteAsync(CommandDefinition command);

        IDbTransaction BeginTransaction();

        IDbTransaction BeginTransaction(IsolationLevel level);

        void ChangeDatabase(string databaseName);

        void Open();

        void Close();

        void CreateCommand();
    }
}