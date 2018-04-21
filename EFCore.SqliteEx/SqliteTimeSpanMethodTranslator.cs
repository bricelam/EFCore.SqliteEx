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

        public Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (Equals(methodCallExpression.Method, _fromDays))
                return new SqlFunctionExpression(
                    "timespan",
                    methodCallExpression.Type,
                    new[] { methodCallExpression.Arguments[0] });

            return null;
        }
    }
}
