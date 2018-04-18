using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bricelam.EntityFrameworkCore.Sqlite
{
    class SqliteExOptionsExtension : IDbContextOptionsExtension
    {
        public string LogFragment
            => string.Empty;

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
