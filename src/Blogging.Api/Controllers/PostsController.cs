namespace Blogging.Api.Controllers;

using Data;
using Data.Entities;
using Models;

[Route("post")]
[ApiController]
public class PostsController(BloggingDbContext dbContext) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostResponse>> GetPost(
        Guid id,
        IncludeWithPost include,
        CancellationToken cancellationToken)
    {
        IQueryable<Post> posts = dbContext.Posts.AsNoTracking();

        switch (include)
        {
            case IncludeWithPost.None:
                break;

            case IncludeWithPost.Author:
                posts = posts.Include(p => p.Author);
                break;

            default:
                return BadRequest();
        }

        var post = await posts.SingleOrDefaultAsync(
            p => p.Id == id,
            cancellationToken);

        if (post is null)
        {
            return NotFound();
        }

        var response = PostResponse.FromPost(post);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost(
        CreatePostRequest request,
        CancellationToken cancellationToken)
    {
        var author = await dbContext.Authors.FindAsync(
            keyValues: [request.AuthorId],
            cancellationToken);

        if (author is null)
        {
            return BadRequest();
        }

        var post = author.WritePost(
            request.Title,
            request.Content,
            request.Description);

        dbContext.Posts.Add(post);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = PostResponse.FromPost(post);

        return CreatedAtAction(
            nameof(GetPost),
            new { id = post.Id },
            response);
    }
}
