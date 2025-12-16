using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
