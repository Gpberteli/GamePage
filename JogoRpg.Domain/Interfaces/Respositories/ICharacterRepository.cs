using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Repositories;

public interface ICharacterRepository : IBaseRepository<Character>
{

    Task<Character> CreateCharacter(long userId, Character character);

}

