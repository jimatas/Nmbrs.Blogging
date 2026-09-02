namespace Blogging.Tests.Unit;

using Api.Models;
using Data.Entities;

public sealed class AuthorResponseTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void FromAuthor_ValidAuthor_MapsAuthorProperties()
    {
        // Arrange
        var author = _fixture.Create<Author>();

        // Act
        var response = AuthorResponse.FromAuthor(author);

        // Assert
        Assert.Equal(author.Id, response.Id);
        Assert.Equal(author.Name, response.Name);
        Assert.Equal(author.Surname, response.Surname);
    }
}
