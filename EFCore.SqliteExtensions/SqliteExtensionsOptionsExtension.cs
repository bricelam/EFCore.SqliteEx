using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExtensionsOptionsExtension : IDbContextOptionsExtension
    {
        public string LogFragment => throw new System.NotImplementedException();

        public long GetServiceProviderHashCode()
            => 0;

        public void Validate(IDbContextOptions options)
        {
            // TODO
        }

        public bool ApplyServices(IServiceCollection services)
        {
            services.AddSqliteExtensions();

            return false;
        }
    }
}
