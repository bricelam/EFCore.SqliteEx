using System;
using System.Linq;
using Bricelam.EntityFrameworkCore.Sqlite.Test;
using Xunit;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    public class TimeSpanTests
    {
        [Fact]
        public void Days()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(1, 12, 0, 0, 0)),
                x => x.Select(e => e.Value.Days),
                "SELECT CAST(days(\"e\".\"Value\") AS INTEGER)");
        }

        [Fact]
        public void TotalDays()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(1, 12, 0, 0, 0)),
                x => x.Select(e => e.Value.TotalDays),
                "SELECT days(\"e\".\"Value\")");
        }

        [Fact]
        public void Hours()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(1, 2, 24, 0, 0)),
                x => x.Select(e => e.Value.Hours),
                "SELECT (days(\"e\".\"Value\") * 24) % 24");
        }

        [Fact]
        public void TotalHours()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(1, 2, 24, 0, 0)),
                x => x.Select(e => e.Value.TotalHours),
                "SELECT days(\"e\".\"Value\") * 24");
        }

        [Fact]
        public void Minutes()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 1, 26, 24, 0)),
                x => x.Select(e => e.Value.Minutes),
                "SELECT (days(\"e\".\"Value\") * 1440) % 60");
        }

        [Fact]
        public void TotalMinutes()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 1, 26, 24, 0)),
                x => x.Select(e => e.Value.TotalMinutes),
                "SELECT days(\"e\".\"Value\") * 1440");
        }

        [Fact]
        public void Seconds()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 0, 1, 26, 400)),
                x => x.Select(e => e.Value.Seconds),
                "SELECT (days(\"e\".\"Value\") * 86400) % 60");
        }

        [Fact]
        public void TotalSeconds()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 0, 1, 26, 400)),
                x => x.Select(e => e.Value.TotalSeconds),
                "SELECT days(\"e\".\"Value\") * 86400");
        }

        [Fact]
        public void Milliseconds()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 0, 0, 8, 640)),
                x => x.Select(e => e.Value.Milliseconds),
                "SELECT (days(\"e\".\"Value\") * 86400000) % 1000");
        }

        [Fact]
        public void TotalMilliseconds()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 0, 0, 8, 640)),
                x => x.Select(e => e.Value.TotalMilliseconds),
                "SELECT days(\"e\".\"Value\") * 86400000");
        }

        [Fact]
        public void Ticks()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(0, 0, 0, 0, 1)),
                x => x.Select(e => e.Value.Ticks),
                "SELECT CAST(days(\"e\".\"Value\") * 864000000000 AS INTEGER)");
        }

        [Fact]
        public void FromDays()
        {
            Test(
                TestEntity.Create(value: 0.0001),
                x => x.Select(e => TimeSpan.FromDays(e.Value)),
                "SELECT timespan(\"e\".\"Value\")");
        }

        void Test<TValue, TResult>(
            TestEntity<TValue> seed,
            Func<IQueryable<TestEntity<TValue>>, IQueryable<TResult>> query,
            string expectedSql)
        {
            using (var db = new TestContext<TestEntity<TValue>>())
            {
                db.Database.EnsureCreated();

                db.Add(seed);
                db.SaveChanges();

                var result = query(db.Entities).ToList();
                Assert.Contains(expectedSql, db.Sql);

                var oracleResult = query(Queryable.AsQueryable(new[] { seed })).ToList();
                Assert.Equal(oracleResult, result, new TestEqualityComparer<TResult>());
            }
        }
    }
}
