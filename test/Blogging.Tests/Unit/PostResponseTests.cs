namespace Blogging.Tests.Unit;

using Api.Models;
using Data.Entities;

public sealed class PostResponseTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void FromPost_PostWithoutAuthor_MapsPostProperties()
    {
        // Arrange
        var post = _fixture.Build<Post>()
            .Without(p => p.Author)
            .Create();

        // Act
        var response = PostResponse.FromPost(post);

        // Assert
        Assert.Null(response.Author);
        Assert.Equal(post.Id, response.Id);
        Assert.Equal(post.AuthorId, response.AuthorId);
        Assert.Equal(post.Title, response.Title);
        Assert.Equal(post.Description, response.Description);
        Assert.Equal(post.Content, response.Content);
    }

    [Fact]
    public void FromPost_PostWithAuthor_MapsPostAndAuthorProperties()
    {
        // Arrange
        var author = _fixture.Create<Author>();

        var post = _fixture.Build<Post>()
            .With(p => p.AuthorId, author.Id)
            .With(p => p.Author, author)
            .Create();

        // Act
        var response = PostResponse.FromPost(post);

        // Assert
        Assert.NotNull(response.Author);
        Assert.Equal(post.Author!.Id, response.Author.Id);
        Assert.Equal(post.Author.Name, response.Author.Name);
        Assert.Equal(post.Author.Surname, response.Author.Surname);
        Assert.Equal(post.Id, response.Id);
        Assert.Equal(post.AuthorId, response.AuthorId);
        Assert.Equal(post.Title, response.Title);
        Assert.Equal(post.Description, response.Description);
        Assert.Equal(post.Content, response.Content);
    }
}
