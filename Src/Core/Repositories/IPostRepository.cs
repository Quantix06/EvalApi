using EvalApi.Src.Models.Post;

namespace EvalApi.Src.Core.Repositories;

public interface IPostRepository
{
    Task<PostModel> CreatePostAsync(PostModel post);
    Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId);
    Task<PostModel?> GetPostByIdAsync(int id);
    Task<PostModel> UpdatePostAsync(PostModel post);
    Task DeletePostAsync(int id);
}
