using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExRelationalConnection : IRelationalConnection
    {
        readonly IRelationalConnection _connection;

        public SqliteExRelationalConnection(IRelationalConnection connection)
            => _connection = connection;

        public string ConnectionString
            => _connection.ConnectionString;

        public DbConnection DbConnection
            => _connection.DbConnection;

        public Guid ConnectionId
            => _connection.ConnectionId;

        public int? CommandTimeout
        {
            get => _connection.CommandTimeout;
            set => _connection.CommandTimeout = value;
        }

        public bool IsMultipleActiveResultSetsEnabled
            => _connection.IsMultipleActiveResultSetsEnabled;

        public IDbContextTransaction CurrentTransaction
            => _connection.CurrentTransaction;

        public SemaphoreSlim Semaphore
            => _connection.Semaphore;

        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
            => _connection.BeginTransaction(isolationLevel);

        public IDbContextTransaction BeginTransaction()
            => _connection.BeginTransaction();

        public Task<IDbContextTransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default)
            => _connection.BeginTransactionAsync(isolationLevel, cancellationToken);

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => _connection.BeginTransactionAsync(cancellationToken);

        public bool Close()
            => _connection.Close();

        public void CommitTransaction()
            => _connection.CommitTransaction();

        public void Dispose()
            => _connection.Dispose();

        public bool Open(bool errorsExpected = false)
        {
            if (!_connection.Open(errorsExpected))
                return false;

            ((SqliteConnection)_connection.DbConnection).AddEFCoreExtensions();

            return true;
        }

        public async Task<bool> OpenAsync(CancellationToken cancellationToken, bool errorsExpected = false)
        {
            if (!await _connection.OpenAsync(cancellationToken, errorsExpected))
                return false;

            ((SqliteConnection)_connection.DbConnection).AddEFCoreExtensions();

            return true;
        }

        public void RegisterBufferable(Microsoft.EntityFrameworkCore.Query.Internal.IBufferable bufferable)
            => _connection.RegisterBufferable(bufferable);

        public Task RegisterBufferableAsync(
            Microsoft.EntityFrameworkCore.Query.Internal.IBufferable bufferable,
            CancellationToken cancellationToken)
            => _connection.RegisterBufferableAsync(bufferable, cancellationToken);

        public void ResetState()
            => _connection.ResetState();

        public void RollbackTransaction()
            => _connection.RollbackTransaction();

        public IDbContextTransaction UseTransaction(DbTransaction transaction)
            => _connection.UseTransaction(transaction);
    }
}
