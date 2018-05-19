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
                .AddOrReplace(ServiceDescriptor.Singleton<ICompositeMethodCallTranslator, SqliteExCompositeMethodCallTranslator>())
                .AddOrReplace(ServiceDescriptor.Singleton<IMemberTranslator, SqliteExCompositeMemberTranslator>())
                .AddOrReplace(ServiceDescriptor.Singleton<ISqlTranslatingExpressionVisitorFactory, SqliteExSqlTranslatingExpressionVisitorFactory>())
                .AddOrReplace(ServiceDescriptor.Scoped<IRelationalConnection, SqliteExRelationalConnection>());

        private static IServiceCollection AddOrReplace(this IServiceCollection services, ServiceDescriptor serviceDescriptor)
        {
            var serviceDescriptorToReplace = services.FirstOrDefault(
                d => d.ServiceType == serviceDescriptor.ServiceType);
            if (serviceDescriptorToReplace != null)
            {
                Debug.Assert(serviceDescriptorToReplace.Lifetime == serviceDescriptor.Lifetime);
                services.Remove(serviceDescriptorToReplace);
            }

            services.Add(serviceDescriptor);

            return services;
        }
    }
}
