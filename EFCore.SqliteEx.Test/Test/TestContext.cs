using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Bricelam.EntityFrameworkCore.Sqlite.Test
{
    class TestContext<TProperty> : DbContext
    {
        readonly TProperty _value;
        readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
        readonly TestLoggerFactory _loggerFactory = new TestLoggerFactory();

        public TestContext(TProperty value)
            => _value = value;

        public DbSet<TestEntity<TProperty>> Entities { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<TestEntity<TProperty>>()
                .HasData(new TestEntity<TProperty> { Id = 1, Value = _value });

        public override void Dispose()
            => _connection.Dispose();
    }

    static class TestContext
    {
        public static TestContext<TProperty> Create<TProperty>(TProperty value)
            => new TestContext<TProperty>(value);
    }
}
