using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Repositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
}
