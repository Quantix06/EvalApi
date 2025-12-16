using EvalApi.Src.Core.Repositories;
using EvalApi.Src.Models.User;

namespace EvalApi.Src.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }
}
