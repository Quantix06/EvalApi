using EvalApi.Src.Models.User;

namespace EvalApi.Src.Core.Services;

public interface IUserService
{
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
}
