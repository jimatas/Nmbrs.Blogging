namespace Blogging.Api.Models;

using Data.Entities;

public sealed record PostResponse(
    Guid Id,
    Guid AuthorId,
    string Title,
    string? Description,
    string Content,
    AuthorResponse? Author)
{
    public static PostResponse FromPost(Post post) =>
        new(
            post.Id,
            post.AuthorId,
            post.Title,
            post.Description,
            post.Content,
            post.Author is { } author
                ? AuthorResponse.FromAuthor(author)
                : null);
}
