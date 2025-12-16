using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services;

public interface IPostService
{
    Task<Post> CreatePostAsync(int userId, Post post);
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
    Task<Post> GetPostByIdAsync(int id);
    Task<Post> UpdatePostAsync(int id, Post post);
    Task DeletePostAsync(int id);
}
