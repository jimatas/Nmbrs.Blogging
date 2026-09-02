namespace Blogging.Data.Configurations;

using Entities;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedNever().HasColumnName("id");
        builder.Property(p => p.AuthorId).HasColumnName("author_id");
        builder.Property(p => p.Title).HasMaxLength(100).HasColumnName("title");
        builder.Property(p => p.Description).HasMaxLength(1000).HasColumnName("description");
        builder.Property(p => p.Content).HasColumnName("content");

        builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId);
    }
}
