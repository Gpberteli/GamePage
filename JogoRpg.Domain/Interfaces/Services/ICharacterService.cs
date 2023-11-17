using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

namespace JogoRpg.Domain.Interface.Services;

public interface ICharacterService : IBaseService<DTO.CharacterDTO>
{
    Task<DTO.CharacterDTO> CreateCharacter(long userId, DTO.CharacterDTO characterDto);
    Task<IEnumerable<DTO.CharacterDTO>> Get();
    Task<DTO.CharacterDTO> Remove(long charId);

}
