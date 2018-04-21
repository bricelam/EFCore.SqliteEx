using System;
using System.Linq;
using Bricelam.EntityFrameworkCore.Sqlite.Test;
using Xunit;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    public class TimeSpanTests
    {
        [Fact]
        public void TotalDays()
        {
            Test(
                TestEntity.Create(value: new TimeSpan(20, 20, 8)),
                x => x.Select(e => e.Value.TotalDays),
                "SELECT days(\"e\".\"Value\")");
        }

        [Fact]
        public void FromDays()
        {
            Test(
                TestEntity.Create(value: 0.847314814815),
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
                Assert.Equal(oracleResult, result);
            }
        }
    }
}
