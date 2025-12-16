using EvalApi.Data;
using EvalApi.Src.Core.Repositories.Entities;
using EvalApi.Src.Models.User;
using Microsoft.EntityFrameworkCore;

namespace EvalApi.Src.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        var entity = new UserEntity
        {
            Name = user.Name,
            Username = user.Username,
            Email = user.Email
        };

        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        user.Id = entity.Id;
        return user;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        var entities = await _context.Users.ToListAsync();
        return entities.Select(e => new UserModel
        {
            Id = e.Id,
            Name = e.Name,
            Username = e.Username,
            Email = e.Email
        });
    }

    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null) return null;

        return new UserModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Username = entity.Username,
            Email = entity.Email
        };
    }
}
