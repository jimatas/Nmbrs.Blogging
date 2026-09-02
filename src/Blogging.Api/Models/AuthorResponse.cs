namespace Blogging.Api.Models;

using Data.Entities;

public sealed record AuthorResponse(
    Guid Id,
    string Name,
    string Surname)
{
    public static AuthorResponse FromAuthor(Author author) =>
        new(
            author.Id,
            author.Name,
            author.Surname);
}
