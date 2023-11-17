using System.Threading.Tasks;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;
using JogoRpg.Services.Services;

namespace JogoRpg.Service.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
        : base(userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserWithCharacters(long userId)
    {
        return await _userRepository.GetUserWithCharacters(userId);
    }

    public override async Task<User> Add(User user)
    {
        return await _userRepository.Add(user);
    }

    public override async Task<User> Get(long userId)
    {
        return await _userRepository.Get(userId);
    }

    public override async Task<User> Update(User user)
    {
        return await _userRepository.Update(user);
    }

    public async Task<User> Remove(long userId)
    {
        return await _userRepository.Remove(userId);
    }
}