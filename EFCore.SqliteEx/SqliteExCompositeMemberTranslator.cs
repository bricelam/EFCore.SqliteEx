using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Sqlite.Query.ExpressionTranslators.Internal;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExCompositeMemberTranslator : SqliteCompositeMemberTranslator
    {
        public SqliteExCompositeMemberTranslator(RelationalCompositeMemberTranslatorDependencies dependencies)
            : base(dependencies)
            => AddTranslators(new[] { new SqliteTimeSpanMemberTranslator() });
    }
}
