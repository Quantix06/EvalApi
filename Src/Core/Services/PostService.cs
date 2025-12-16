using EvalApi.Src.Core.Exceptions;
using EvalApi.Src.Core.Repositories;
using EvalApi.Src.Models.Post;

namespace EvalApi.Src.Core.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<PostModel> CreatePostAsync(int userId, PostModel post)
    {
        // Verify user exists
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }

        post.UserId = userId;
        return await _postRepository.CreatePostAsync(post);
    }

    public async Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId)
    {
        // Verify user exists? Not strictly required by prompt but good practice.
        // Prompt says: "Un endpoint ReST pour retrouver tous les articles d’un utilisateur"
        // If user doesn't exist, returning empty list is also fine, or 404.
        // I'll check user existence to be safe and consistent.
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }

        return await _postRepository.GetPostsByUserIdAsync(userId);
    }

    public async Task<PostModel> GetPostByIdAsync(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            throw new NotFoundException($"Post with id {id} not found");
        }
        return post;
    }

    public async Task<PostModel> UpdatePostAsync(int id, PostModel post)
    {
        var existingPost = await _postRepository.GetPostByIdAsync(id);
        if (existingPost == null)
        {
            throw new NotFoundException($"Post with id {id} not found");
        }

        if (existingPost.UserId != post.UserId)
        {
            throw new BadRequestException("UserId does not match the post author");
        }

        post.Id = id; // Ensure ID is set
        return await _postRepository.UpdatePostAsync(post);
    }

    public async Task DeletePostAsync(int id)
    {
        var existingPost = await _postRepository.GetPostByIdAsync(id);
        if (existingPost == null)
        {
            throw new NotFoundException($"Post with id {id} not found");
        }
        await _postRepository.DeletePostAsync(id);
    }
}
