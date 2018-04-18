using Bricelam.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseSqliteExtensions(this DbContextOptionsBuilder optionsBuilder)
        {
            var extension = optionsBuilder.Options.FindExtension<SqliteExtensionsOptionsExtension>()
                ?? new SqliteExtensionsOptionsExtension();

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }

        public static DbContextOptionsBuilder<TContext> UseSqliteExtensions<TContext>(
            this DbContextOptionsBuilder<TContext> optionsBuilder)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseSqliteExtensions((DbContextOptionsBuilder)optionsBuilder);
    }
}
