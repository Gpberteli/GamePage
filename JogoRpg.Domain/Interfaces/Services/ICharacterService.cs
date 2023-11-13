using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Services;

public interface ICharacterService : IBaseService<Character>
{
    Task<IEnumerable<Character>> Get();
    Task<Character> Remove(long charId);
    Task<Character> CreateCharacter(long userId, Character character);
}
