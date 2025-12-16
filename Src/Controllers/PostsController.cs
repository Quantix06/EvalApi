using EvalApi.Src.Core.Services;
using EvalApi.Src.Models.Post;
using EvalApi.Src.Views.Dto.Post;
using Microsoft.AspNetCore.Mvc;

namespace EvalApi.Src.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("{postId}")]
    public async Task<ActionResult<PostDto>> GetPost(int postId)
    {
        var post = await _postService.GetPostByIdAsync(postId);
        var postDto = new PostDto
        {
            id = post.Id,
            userId = post.UserId,
            title = post.Title,
            body = post.Body
        };

        return Ok(postDto);
    }

    [HttpPut("{postId}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int postId, [FromBody] UpdatePostDto updatePostDto)
    {
        if (postId != updatePostDto.id)
        {
            return BadRequest("PostId in route must match Id in body");
        }

        var post = new PostModel
        {
            Id = updatePostDto.id,
            UserId = updatePostDto.userId,
            Title = updatePostDto.title,
            Body = updatePostDto.body
        };

        var updatedPost = await _postService.UpdatePostAsync(postId, post);

        var postDto = new PostDto
        {
            id = updatedPost.Id,
            userId = updatedPost.UserId,
            title = updatedPost.Title,
            body = updatedPost.Body
        };

        return Ok(postDto);
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        await _postService.DeletePostAsync(postId);
        return NoContent();
    }
}
