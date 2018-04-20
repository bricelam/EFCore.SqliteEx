using System;
using System.Linq;
using System.Linq.Expressions;
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
                new TimeSpan(20, 20, 8),
                v => v.TotalDays,
                "SELECT days(\"e\".\"Value\")");
        }

        [Fact]
        public void FromDays()
        {
            Test(
                0.847314814815,
                v => TimeSpan.FromDays(v),
                "SELECT timespan(\"e\".\"Value\")");
        }

        void Test<TValue, TResult>(
            TValue value,
            Expression<Func<TValue, TResult>> projection,
            string expectedSql)
        {
            using (var db = TestContext.Create(value))
            {
                db.Database.EnsureCreated();

                var parameter = Expression.Parameter(typeof(TestEntity<TValue>), "e");
                var result = db.Entities
                    .Where(e => e.Id == 1)
                    .Select(
                        (Expression<Func<TestEntity<TValue>, TResult>>)Expression.Lambda(
                            Expression.Invoke(projection, Expression.Property(parameter, "Value")),
                            parameter))
                    .First();

                Assert.Contains(expectedSql, db.Sql);

                var expectedResult = projection.Compile().Invoke(value);
                Assert.Equal(expectedResult, result);
            }
        }
    }
}
