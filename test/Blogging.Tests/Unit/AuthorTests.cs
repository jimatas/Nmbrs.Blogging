namespace Blogging.Tests.Unit;

using Data.Entities;

public sealed class AuthorTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void WritePost_ValidArguments_CreatesPost()
    {
        // Arrange
        var author = _fixture.Create<Author>();
        var title = _fixture.Create<string>();
        var content = _fixture.Create<string>();
        var description = _fixture.Create<string>();

        // Act
        var post = author.WritePost(
            title,
            content,
            description);

        // Assert
        Assert.NotEqual(Guid.Empty, post.Id);
        Assert.Equal(title, post.Title);
        Assert.Equal(content, post.Content);
        Assert.Equal(description, post.Description);
        Assert.Equal(author.Id, post.AuthorId);
        Assert.Same(author, post.Author);
    }
}
