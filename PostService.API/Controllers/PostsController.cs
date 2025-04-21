using BloggingPlatform.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private static readonly List<PostDto> Posts = new();

    [HttpPost]
    public IActionResult CreatePost(PostDto post)
    {
        post.Id = Guid.NewGuid();
        Posts.Add(post);
        return Ok(post);
    }

    [HttpGet]
    public IActionResult GetPosts() => Ok(Posts);

    [HttpGet("{id}")]
    public IActionResult GetPostById(Guid id)
    {
        var post = Posts.FirstOrDefault(p => p.Id == id);
        return post is null ? NotFound() : Ok(post);
    }
}
