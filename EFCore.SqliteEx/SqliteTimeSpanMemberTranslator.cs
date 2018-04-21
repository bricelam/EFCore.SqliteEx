using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteTimeSpanMemberTranslator : IMemberTranslator
    {
        const int HoursPerDay = 24;
        const int MinutesPerHour = 60;
        const int SecondsPerMinute = 60;
        const int MillisecondsPerSecond = 1000;

        public Expression Translate(MemberExpression memberExpression)
        {
            if (memberExpression.Member.DeclaringType != typeof(TimeSpan))
                return null;

            switch (memberExpression.Member.Name)
            {
                case nameof(TimeSpan.Days):
                    return new ExplicitCastExpression(
                        Days(memberExpression),
                        typeof(int));

                case nameof(TimeSpan.Hours):
                    return Expression.Convert(
                        Expression.Modulo(
                            Hours(memberExpression),
                            Expression.Convert(
                                Expression.Constant(HoursPerDay),
                                typeof(double))),
                        typeof(int));

                case nameof(TimeSpan.Minutes):
                    return Expression.Convert(
                        Expression.Modulo(
                            Minutes(memberExpression),
                            Expression.Convert(
                                Expression.Constant(MinutesPerHour),
                                typeof(double))),
                        typeof(int));

                case nameof(TimeSpan.Seconds):
                    return Expression.Convert(
                        Expression.Modulo(
                            Seconds(memberExpression),
                            Expression.Convert(
                                Expression.Constant(SecondsPerMinute),
                                typeof(double))),
                        typeof(int));

                case nameof(TimeSpan.Milliseconds):
                    return Expression.Convert(
                        Expression.Modulo(
                            Milliseconds(memberExpression),
                            Expression.Convert(
                                Expression.Constant(MillisecondsPerSecond),
                                typeof(double))),
                        typeof(int));

                case nameof(TimeSpan.Ticks):
                    return new ExplicitCastExpression(
                        Expression.Multiply(
                            Days(memberExpression),
                            Expression.Convert(
                                Expression.Constant(TimeSpan.TicksPerDay),
                                typeof(double))),
                        typeof(long));

                case nameof(TimeSpan.TotalDays):
                    return Days(memberExpression);

                case nameof(TimeSpan.TotalHours):
                    return Hours(memberExpression);

                case nameof(TimeSpan.TotalMinutes):
                    return Minutes(memberExpression);

                case nameof(TimeSpan.TotalSeconds):
                    return Seconds(memberExpression);

                case nameof(TimeSpan.TotalMilliseconds):
                    return Milliseconds(memberExpression);
            }

            return null;
        }

        static Expression Days(MemberExpression memberExpression)
            => new SqlFunctionExpression("days", typeof(double), new[] { memberExpression.Expression });

        static Expression Hours(MemberExpression memberExpression)
            => Expression.Multiply(
                Days(memberExpression),
                Expression.Convert(
                    Expression.Constant(HoursPerDay),
                    typeof(double)));

        static Expression Minutes(MemberExpression memberExpression)
            => Expression.Multiply(
                Days(memberExpression),
                Expression.Convert(
                    Expression.Constant(HoursPerDay * MinutesPerHour),
                    typeof(double)));

        static Expression Seconds(MemberExpression memberExpression)
            => Expression.Multiply(
                Days(memberExpression),
                Expression.Convert(
                    Expression.Constant(HoursPerDay * MinutesPerHour * SecondsPerMinute),
                    typeof(double)));

        static Expression Milliseconds(MemberExpression memberExpression)
            => Expression.Multiply(
                Days(memberExpression),
                Expression.Convert(
                    Expression.Constant(HoursPerDay * MinutesPerHour * SecondsPerMinute * MillisecondsPerSecond),
                    typeof(double)));

    }
}
