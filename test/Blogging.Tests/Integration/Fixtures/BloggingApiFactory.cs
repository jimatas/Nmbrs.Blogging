namespace Blogging.Tests.Integration.Fixtures;

using Data;

public sealed class BloggingApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databasePath =
        Path.Combine(
            Path.GetTempPath(),
            $"blogging-tests-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<BloggingDbContext>>();
            services.RemoveAll<BloggingDbContext>();

            services.AddDbContext<BloggingDbContext>(
                options => options.UseSqlite(
                    $"Data Source={_databasePath};Pooling=False"));
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (File.Exists(_databasePath))
        {
            File.Delete(_databasePath);
        }
    }
}
