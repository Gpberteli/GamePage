using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Services;

public interface IUserService : IBaseService<UserDTO>
{
    Task<UserDTO> GetUserWithCharacters(long userId);
    Task<UserDTO> Remove(long userId);
}
