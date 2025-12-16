using EvalApi.Src.Models.User;

namespace EvalApi.Src.Core.Repositories;

public interface IUserRepository
{
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int id);
}
