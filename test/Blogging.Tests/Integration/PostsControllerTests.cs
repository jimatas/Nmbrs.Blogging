namespace Blogging.Tests.Integration;

using Api.Models;
using Data;
using Data.Entities;
using Fixtures;

public sealed class PostsControllerTests : IClassFixture<BloggingApiFactory>
{
    private readonly Fixture _fixture = new();
    private readonly BloggingApiFactory _factory;
    private readonly HttpClient _client;

    public PostsControllerTests(BloggingApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePost_ValidRequest_ReturnsCreatedPost()
    {
        // Arrange
        var author = await CreateAuthorAsync();

        var request = _fixture.Build<CreatePostRequest>()
            .With(p => p.AuthorId, author.Id)
            .Create();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/post",
            request,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var post = await response.Content
            .ReadFromJsonAsync<PostResponse>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(post);
        Assert.Equal(author.Id, post.AuthorId);
        Assert.Equal(request.Title, post.Title);
        Assert.Equal(request.Description, post.Description);
        Assert.Equal(request.Content, post.Content);

        Assert.NotNull(post.Author);
        Assert.Equal(author.Id, post.Author.Id);
    }

    [Fact]
    public async Task CreatePost_UnknownAuthor_ReturnsBadRequest()
    {
        // Arrange
        var request = _fixture.Build<CreatePostRequest>()
            .With(p => p.AuthorId, _fixture.Create<Guid>())
            .Create();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/post",
            request,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetPost_ExistingPost_ReturnsPost()
    {
        // Arrange
        var existingPost = await CreatePostAsync();

        // Act
        var response = await _client.GetAsync(
            $"/post/{existingPost.Id}",
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var post = await response.Content
            .ReadFromJsonAsync<PostResponse>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(post);
        Assert.Equal(existingPost.Id, post.Id);
        Assert.Equal(existingPost.AuthorId, post.AuthorId);
        Assert.Equal(existingPost.Title, post.Title);
        Assert.Equal(existingPost.Description, post.Description);
        Assert.Equal(existingPost.Content, post.Content);
        Assert.Null(post.Author);
    }

    [Fact]
    public async Task GetPost_WithAuthorIncluded_ReturnsPostWithAuthor()
    {
        // Arrange
        var existingPost = await CreatePostAsync();

        // Act
        var response = await _client.GetAsync(
            $"/post/{existingPost.Id}?include=author",
            TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var post = await response.Content
            .ReadFromJsonAsync<PostResponse>(
                TestContext.Current.CancellationToken);

        Assert.NotNull(post);
        Assert.NotNull(post.Author);
        Assert.Equal(existingPost.Author!.Id, post.Author.Id);
        Assert.Equal(existingPost.Author.Name, post.Author.Name);
        Assert.Equal(existingPost.Author.Surname, post.Author.Surname);
    }

    private async Task<Author> CreateAuthorAsync()
    {
        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<BloggingDbContext>();

        var author = _fixture.Create<Author>();

        dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync();

        return author;
    }

    private async Task<Post> CreatePostAsync()
    {
        using var scope = _factory.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<BloggingDbContext>();

        var author = _fixture.Create<Author>();

        var post = _fixture.Build<Post>()
            .With(p => p.AuthorId, author.Id)
            .With(p => p.Author, author)
            .Create();

        dbContext.Posts.Add(post);
        await dbContext.SaveChangesAsync();

        return post;
    }
}
