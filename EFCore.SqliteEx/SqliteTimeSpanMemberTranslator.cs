using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteTimeSpanMemberTranslator : IMemberTranslator
    {
        public Expression Translate(MemberExpression memberExpression)
        {
            if (memberExpression.Member.DeclaringType != typeof(TimeSpan))
                return null;

            switch (memberExpression.Member.Name)
            {
                case nameof(TimeSpan.TotalDays):
                    return new SqlFunctionExpression(
                        "days",
                        memberExpression.Type,
                        new[] { memberExpression.Expression });
            }

            return null;
        }
    }
}
