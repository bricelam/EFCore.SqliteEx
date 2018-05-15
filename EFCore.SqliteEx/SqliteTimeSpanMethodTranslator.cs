using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteTimeSpanMethodTranslator : IMethodCallTranslator
    {
        static readonly MethodInfo _fromDays = typeof(TimeSpan)
            .GetRuntimeMethod(nameof(TimeSpan.FromDays), new[] { typeof(double) });

        static readonly MethodInfo _negate = typeof(TimeSpan)
            .GetRuntimeMethod(nameof(TimeSpan.Negate), Type.EmptyTypes);

        public Expression Translate(MethodCallExpression methodCallExpression)
        {
            var method = methodCallExpression.Method;
            if (Equals(method, _fromDays))
            {
                return new SqlFunctionExpression(
                    "timespan",
                    methodCallExpression.Type,
                    new[] { methodCallExpression.Arguments[0] });
            }
            else if (Equals(method, _negate))
            {
                return new SqlFunctionExpression(
                    "timespan",
                    methodCallExpression.Type,
                    new[]
                    {
                        Expression.Negate(
                            new SqlFunctionExpression(
                                "days",
                                typeof(double),
                                new[] { methodCallExpression.Object }))
                    });
            }

            return null;
        }
    }
}
