using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExSqlTranslatingExpressionVisitorFactory : SqlTranslatingExpressionVisitorFactory
    {
        public SqliteExSqlTranslatingExpressionVisitorFactory(SqlTranslatingExpressionVisitorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override SqlTranslatingExpressionVisitor Create(
            RelationalQueryModelVisitor queryModelVisitor,
            SelectExpression targetSelectExpression,
            Expression topLevelPredicate,
            bool inProjection)
            => new SqliteExSqlTranslatingExpressionVisitor(
                Dependencies,
                queryModelVisitor,
                targetSelectExpression,
                topLevelPredicate,
                inProjection);
    }
}
