using System;
using System.Linq;
using Bricelam.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqliteExtensions(this IServiceCollection services)
        {
            var compositeMethodCallTranslator = Remove<ICompositeMethodCallTranslator>(services);
            var memberTranslator = Remove<IMemberTranslator>(services);
            var relationalConnection = Remove<IRelationalConnection>(services);

            return services
                .AddSingleton<ICompositeMethodCallTranslator>(
                    p => new SqliteExCompositeMethodCallTranslator(
                        compositeMethodCallTranslator(p),
                        p.GetRequiredService<RelationalCompositeMethodCallTranslatorDependencies>()))
                .AddSingleton<IMemberTranslator>(
                    p => new SqliteExCompositeMemberTranslator(
                        memberTranslator(p),
                        p.GetRequiredService<RelationalCompositeMemberTranslatorDependencies>()))
                .AddScoped<IRelationalConnection>(
                    p => new SqliteExRelationalConnection(relationalConnection(p)));
        }

        private static Func<IServiceProvider, TService> Remove<TService>(IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
            if (serviceDescriptor == null)
                throw new InvalidOperationException("Call UseSqlite() before ExtendSqlite().");

            services.Remove(serviceDescriptor);

            if (serviceDescriptor.ImplementationType != null)
            {
                return p => (TService)ActivatorUtilities.CreateInstance(p, serviceDescriptor.ImplementationType);
            }
            if (serviceDescriptor.ImplementationFactory != null)
            {
                return p => (TService)serviceDescriptor.ImplementationFactory(p);
            }

            throw new InvalidOperationException("Can't create service.");
            //return p => (TService)serviceDescriptor.ImplementationInstance;
        }
    }
}
