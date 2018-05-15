using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Sqlite.Query.ExpressionTranslators.Internal;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExCompositeMethodCallTranslator : SqliteCompositeMethodCallTranslator
    {
        public SqliteExCompositeMethodCallTranslator(RelationalCompositeMethodCallTranslatorDependencies dependencies)
            : base(dependencies)
            => AddTranslators(new[] { new SqliteTimeSpanMethodTranslator() });
    }
}
