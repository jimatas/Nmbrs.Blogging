namespace Blogging.Data;

public sealed class DatabaseInitializer(BloggingDbContext dbContext)
{
    private static readonly Guid InitialAuthorId = new("01a062ac-36a4-7060-9b5d-331347ac7c3f");

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory("App_Data");

        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Authors.AnyAsync(
            a => a.Id == InitialAuthorId,
            cancellationToken))
        {
            return;
        }

        dbContext.Authors.Add(
            new()
            {
                Id = InitialAuthorId,
                Name = "Jim",
                Surname = "Bosatlas"
            });

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
