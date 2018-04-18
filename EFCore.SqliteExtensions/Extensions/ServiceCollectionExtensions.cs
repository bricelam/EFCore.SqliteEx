namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqliteExtensions(this IServiceCollection services)
            // TODO: Register additional IMemberTranslator
            => services;
    }
}
