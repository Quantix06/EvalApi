using EvalApi.Src.Core.Services;
using EvalApi.Src.Models.Post;
using EvalApi.Src.Models.User;
using EvalApi.Src.Views.Dto.Post;
using EvalApi.Src.Views.Dto.User;
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
        var user = new UserModel
        {
            Name = createUserDto.name,
            Username = createUserDto.username,
            Email = createUserDto.email
        };

        var createdUser = await _userService.CreateUserAsync(user);

        var userDto = new UserDto
        {
            id = createdUser.Id,
            name = createdUser.Name,
            username = createdUser.Username,
            email = createdUser.Email
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
            id = u.Id,
            name = u.Name,
            username = u.Username,
            email = u.Email
        });

        return Ok(userDtos);
    }

    [HttpPost("{userId}/posts")]
    public async Task<ActionResult<PostDto>> CreatePost(int userId, [FromBody] CreatePostDto createPostDto)
    {
        if (userId != createPostDto.userId)
        {
            return BadRequest("UserId in route must match UserId in body");
        }

        var post = new PostModel
        {
            UserId = createPostDto.userId,
            Title = createPostDto.title,
            Body = createPostDto.body
        };

        var createdPost = await _postService.CreatePostAsync(userId, post);

        var postDto = new PostDto
        {
            id = createdPost.Id,
            userId = createdPost.UserId,
            title = createdPost.Title,
            body = createdPost.Body
        };

        return StatusCode(201, postDto);
    }

    [HttpGet("{userId}/posts")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByUser(int userId)
    {
        var posts = await _postService.GetPostsByUserIdAsync(userId);
        var postDtos = posts.Select(p => new PostDto
        {
            id = p.Id,
            userId = p.UserId,
            title = p.Title,
            body = p.Body
        });

        return Ok(postDtos);
    }
}
