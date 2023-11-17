using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Repositories;

public interface IUserRepository : IBaseRepository<UserDTO>
{
    Task<UserDTO> GetUserWithCharacters(long userId);
    Task<UserDTO> Authenticate(string username, string password);
}