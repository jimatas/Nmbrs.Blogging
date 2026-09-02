namespace Blogging.Data.Entities;

public sealed class Post
{
    public Guid Id { get; init; }
    public Guid AuthorId { get; init; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Content { get; set; }
    public Author? Author { get; init; }
}
