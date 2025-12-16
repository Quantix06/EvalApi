using EvalApi.Src.Core.Services;
using EvalApi.Src.Models;
using EvalApi.Src.Views.Dto;
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
            Id = post.Id,
            UserId = post.UserId,
            Title = post.Title,
            Body = post.Body
        };

        return Ok(postDto);
    }

    [HttpPut("{postId}")]
    public async Task<ActionResult<PostDto>> UpdatePost(int postId, [FromBody] UpdatePostDto updatePostDto)
    {
        if (postId != updatePostDto.Id)
        {
            return BadRequest("PostId in route must match Id in body");
        }

        var post = new Post
        {
            Id = updatePostDto.Id,
            UserId = updatePostDto.UserId,
            Title = updatePostDto.Title,
            Body = updatePostDto.Body
        };

        var updatedPost = await _postService.UpdatePostAsync(postId, post);

        var postDto = new PostDto
        {
            Id = updatedPost.Id,
            UserId = updatedPost.UserId,
            Title = updatedPost.Title,
            Body = updatedPost.Body
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
