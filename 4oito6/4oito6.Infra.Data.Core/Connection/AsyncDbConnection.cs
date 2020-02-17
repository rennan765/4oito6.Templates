using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace _4oito6.Infra.Data.Core.Connection
{
    public class AsyncDbConnection : IAsyncDbConnection
    {
        private bool _disposedValue;
        private IDbConnection _conn;

        public AsyncDbConnection(IDbConnection conn)
        {
            _conn = conn;
            _disposedValue = false;
        }

        public IDbTransaction Transaction { get; private set; }

        public string ConnectionString => _conn.ConnectionString;

        public int ConnectionTimeOut => _conn.ConnectionTimeout;

        public ConnectionState State => _conn.State;

        public string Database => _conn.Database;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _conn?.Dispose();
                    _conn = null;

                    Transaction?.Dispose();
                    Transaction = null;
                }

                _disposedValue = true;
            }
        }

        ~AsyncDbConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(CommandDefinition command)
            => await _conn.QueryAsync(command).ConfigureAwait(false);

        public async Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command)
            => await _conn.QueryAsync<T>(command).ConfigureAwait(false);

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object parameters, IDbTransaction transaction = null)
            => await _conn.QueryAsync(sql: sql, param: parameters, transaction: transaction)
                .ConfigureAwait(false);

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters, IDbTransaction transaction = null)
            => await _conn.QueryAsync<T>(sql: sql, param: parameters, transaction: transaction)
                .ConfigureAwait(false);

        public async Task<int> ExecuteAsync(CommandDefinition command)
            => await _conn.ExecuteAsync(command).ConfigureAwait(false);

        public IDbTransaction BeginTransaction() => _conn.BeginTransaction();

        public IDbTransaction BeginTransaction(IsolationLevel level) => _conn.BeginTransaction(level);

        public void ChangeDatabase(string databaseName)
        {
            _conn.ChangeDatabase(databaseName);
        }

        public void Open()
        {
            _conn.Open();
        }

        public void Close()
        {
            _conn.Close();
        }

        public void CreateCommand()
        {
            _conn.CreateCommand();
        }
    }
}