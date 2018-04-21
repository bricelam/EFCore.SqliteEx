using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestContext<TEntity> : DbContext
        where TEntity : class
    {
        readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        readonly TestLoggerFactory _loggerFactory = new TestLoggerFactory();

        public DbSet<TEntity> Entities { get; set; }

        public string Sql
            => _loggerFactory.Logger.Sql;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _connection.Open();
            _connection.AddEFCoreExtensions();

            options
                .UseSqlite(_connection)
                .ExtendSqlite()
                .UseLoggerFactory(_loggerFactory);
        }

        public override void Dispose()
            => _connection.Dispose();
    }
}
