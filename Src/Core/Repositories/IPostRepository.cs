using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Repositories;

public interface IPostRepository
{
    Task<Post> CreatePostAsync(Post post);
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
    Task<Post?> GetPostByIdAsync(int id);
    Task<Post> UpdatePostAsync(Post post);
    Task DeletePostAsync(int id);
}
