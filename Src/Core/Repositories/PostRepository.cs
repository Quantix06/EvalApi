using EvalApi.Data;
using EvalApi.Src.Core.Repositories.Entities;
using EvalApi.Src.Models.Post;
using Microsoft.EntityFrameworkCore;

namespace EvalApi.Src.Core.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PostModel> CreatePostAsync(PostModel post)
    {
        var entity = new PostEntity
        {
            UserId = post.UserId,
            Title = post.Title,
            Body = post.Body
        };

        _context.Posts.Add(entity);
        await _context.SaveChangesAsync();

        post.Id = entity.Id;
        return post;
    }

    public async Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId)
    {
        var entities = await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        return entities.Select(e => new PostModel
        {
            Id = e.Id,
            UserId = e.UserId,
            Title = e.Title,
            Body = e.Body
        });
    }

    public async Task<PostModel?> GetPostByIdAsync(int id)
    {
        var entity = await _context.Posts.FindAsync(id);
        if (entity == null) return null;

        return new PostModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Title = entity.Title,
            Body = entity.Body
        };
    }

    public async Task<PostModel> UpdatePostAsync(PostModel post)
    {
        var entity = await _context.Posts.FindAsync(post.Id);
        if (entity != null)
        {
            entity.Title = post.Title;
            entity.Body = post.Body;
            // UserId is not updated as per requirements, but we should check it in Service/Controller
            await _context.SaveChangesAsync();
        }
        return post;
    }

    public async Task DeletePostAsync(int id)
    {
        var entity = await _context.Posts.FindAsync(id);
        if (entity != null)
        {
            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
