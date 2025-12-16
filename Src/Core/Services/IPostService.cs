using EvalApi.Src.Models.Post;

namespace EvalApi.Src.Core.Services;

public interface IPostService
{
    Task<PostModel> CreatePostAsync(int userId, PostModel post);
    Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId);
    Task<PostModel> GetPostByIdAsync(int id);
    Task<PostModel> UpdatePostAsync(int id, PostModel post);
    Task DeletePostAsync(int id);
}
