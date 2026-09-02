namespace Blogging.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddBloggingData(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddScoped<DatabaseInitializer>();

        services.AddDbContext<BloggingDbContext>(
            options => options.UseSqlite(connectionString));

        return services;
    }
}
