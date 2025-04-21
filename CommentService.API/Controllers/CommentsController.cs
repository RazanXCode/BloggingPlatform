using BloggingPlatform.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("comments")]
public class CommentsController : ControllerBase
{
    private static readonly List<CommentDto> Comments = new();

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public CommentsController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(CommentDto comment)
    {
        var postServiceUrl = _config["PostServiceUrl"];
        var response = await _httpClient.GetAsync($"{postServiceUrl}/posts/{comment.PostId}");

        if (!response.IsSuccessStatusCode)
            return BadRequest("Invalid postId");

        Comments.Add(comment);
        return Ok(comment);
    }

    [HttpGet("{postId}")]
    public IActionResult GetComments(Guid postId)
    {
        var postComments = Comments.Where(c => c.PostId == postId);
        return Ok(postComments);
    }
}
