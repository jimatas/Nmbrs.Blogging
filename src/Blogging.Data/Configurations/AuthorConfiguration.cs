namespace Blogging.Data.Configurations;

using Entities;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Author");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).ValueGeneratedNever().HasColumnName("id");
        builder.Property(a => a.Name).HasMaxLength(50).HasColumnName("name");
        builder.Property(a => a.Surname).HasMaxLength(100).HasColumnName("surname");
    }
}
