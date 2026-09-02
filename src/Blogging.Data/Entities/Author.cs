namespace Blogging.Data.Entities;

public sealed class Author
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Surname { get; set; }

    public Post WritePost(
        string title,
        string content,
        string? description = null) =>
        new()
        {
            Id = Guid.CreateVersion7(),
            AuthorId = Id,
            Title = title,
            Content = content,
            Description = description,
            Author = this
        };
}
