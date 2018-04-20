using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteTimeSpanMethodTranslator : IMethodCallTranslator
    {
        public Expression Translate(MethodCallExpression methodCallExpression)
        {
            // TODO: Use MethodInfo instead
            if (methodCallExpression.Method.DeclaringType != typeof(TimeSpan))
                return null;

            switch (methodCallExpression.Method.Name)
            {
                case nameof(TimeSpan.FromDays):
                    return new SqlFunctionExpression(
                        "timespan",
                        methodCallExpression.Type,
                        new[] { methodCallExpression.Arguments[0] });
            }

            return null;
        }
    }
}
