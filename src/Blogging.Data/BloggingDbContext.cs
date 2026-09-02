namespace Blogging.Data;

using Entities;

public sealed class BloggingDbContext(
    DbContextOptions<BloggingDbContext> options)
    : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BloggingDbContext).Assembly);
    }
}
