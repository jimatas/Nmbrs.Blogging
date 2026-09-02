namespace Blogging.Api.Models;

public sealed record CreatePostRequest(
    Guid AuthorId,

    [MaxLength(100)]
    string Title,

    [MaxLength(1000)]
    string? Description,

    string Content);
