using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Services;

public interface IUserService : IBaseService<User>
{
    Task<User> GetUserWithCharacters(long userId);
}
