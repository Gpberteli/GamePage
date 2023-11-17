using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> Remove(long userId);
    Task<User> GetUserWithCharacters(long userId);
    Task<User> Authenticate(string username, string password);
}
