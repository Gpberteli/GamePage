using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Repositories;

public interface ICharacterRepository : IBaseRepository<CharacterDTO>
{
    Task<CharacterDTO> CreateCharacter(long userId, CharacterDTO character);
}

