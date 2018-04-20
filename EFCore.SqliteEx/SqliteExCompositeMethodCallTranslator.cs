using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExCompositeMethodCallTranslator : RelationalCompositeMethodCallTranslator
    {
        readonly ICompositeMethodCallTranslator _compositeMethodCallTranslator;

        public SqliteExCompositeMethodCallTranslator(
            ICompositeMethodCallTranslator compositeMethodCallTranslator,
            RelationalCompositeMethodCallTranslatorDependencies dependencies)
            : base(dependencies)
        {
            _compositeMethodCallTranslator = compositeMethodCallTranslator;

            AddTranslators(
                new[]
                {
                    new SqliteTimeSpanMethodTranslator()
                });
        }

        public override Expression Translate(MethodCallExpression methodCallExpression, IModel model)
            => _compositeMethodCallTranslator.Translate(methodCallExpression, model)
                ?? base.Translate(methodCallExpression, model);
    }
}
