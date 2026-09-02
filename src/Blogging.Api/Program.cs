using Blogging.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("Blogging")
    ?? throw new InvalidOperationException(
        "Connection string 'Blogging' was not configured.");

builder.Services.AddBloggingData(connectionString);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddOpenApi();

var app = builder.Build();

await InitializeDatabaseAsync(app.Services);

app.MapOpenApi();

app.MapControllers();

app.Run();

static async Task InitializeDatabaseAsync(IServiceProvider provider)
{
    using var scope = provider.CreateScope();
    
    var initializer = scope.ServiceProvider
        .GetRequiredService<DatabaseInitializer>();

    await initializer.InitializeAsync();
}
