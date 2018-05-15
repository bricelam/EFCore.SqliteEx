using System.Diagnostics;
using System.Linq;
using Bricelam.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqliteEx(this IServiceCollection services)
            => services
                .AddOrReplace<ICompositeMethodCallTranslator, SqliteExCompositeMethodCallTranslator>(
                    ServiceLifetime.Singleton)
                .AddOrReplace<IMemberTranslator, SqliteExCompositeMemberTranslator>(ServiceLifetime.Singleton)
                .AddOrReplace<ISqlTranslatingExpressionVisitorFactory, SqliteExSqlTranslatingExpressionVisitorFactory>(
                    ServiceLifetime.Singleton)
                .AddOrReplace<IRelationalConnection, SqliteExRelationalConnection>(ServiceLifetime.Scoped);

        private static IServiceCollection AddOrReplace<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime lifetime)
        {
            var serviceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
            if (serviceDescriptor != null)
            {
                Debug.Assert(serviceDescriptor.Lifetime == lifetime);
                services.Remove(serviceDescriptor);
            }

            services.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));

            return services;
        }
    }
}
