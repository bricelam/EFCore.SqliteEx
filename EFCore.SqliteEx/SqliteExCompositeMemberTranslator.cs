using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExCompositeMemberTranslator : RelationalCompositeMemberTranslator
    {
        public SqliteExCompositeMemberTranslator(
            IMemberTranslator memberTranslator,
            RelationalCompositeMemberTranslatorDependencies dependencies)
            : base(dependencies)
            => AddTranslators(
                new[]
                {
                    memberTranslator,
                    new SqliteTimeSpanMemberTranslator()
                });
    }
}
