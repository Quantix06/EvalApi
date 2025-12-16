using EvalApi.Src.Core.Services;
using EvalApi.Src.Models;
using EvalApi.Src.Views.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EvalApi.Src.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPostService _postService;

    public UsersController(IUserService userService, IPostService postService)
    {
        _userService = userService;
        _postService = postService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var user = new User
        {
            Name = createUserDto.Name,
            Username = createUserDto.Username,
            Email = createUserDto.Email
        };

        var createdUser = await _userService.CreateUserAsync(user);

        var userDto = new UserDto
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Username = createdUser.Username,
            Email = createdUser.Email
        };

        // Ideally return CreatedAtAction, but for simplicity returning Ok/Created with body
        return StatusCode(201, userDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        var userDtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Username = u.Username,
            Email = u.Email
        });

        return Ok(userDtos);
    }

    [HttpPost("{userId}/posts")]
    public async Task<ActionResult<PostDto>> CreatePost(int userId, [FromBody] CreatePostDto createPostDto)
    {
        if (userId != createPostDto.UserId)
        {
            return BadRequest("UserId in route must match UserId in body");
        }

        var post = new Post
        {
            UserId = createPostDto.UserId,
            Title = createPostDto.Title,
            Body = createPostDto.Body
        };

        var createdPost = await _postService.CreatePostAsync(userId, post);

        var postDto = new PostDto
        {
            Id = createdPost.Id,
            UserId = createdPost.UserId,
            Title = createdPost.Title,
            Body = createdPost.Body
        };

        return StatusCode(201, postDto);
    }

    [HttpGet("{userId}/posts")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByUser(int userId)
    {
        var posts = await _postService.GetPostsByUserIdAsync(userId);
        var postDtos = posts.Select(p => new PostDto
        {
            Id = p.Id,
            UserId = p.UserId,
            Title = p.Title,
            Body = p.Body
        });

        return Ok(postDtos);
    }
}
