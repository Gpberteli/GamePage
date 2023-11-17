using JogoRpg.Domain.DTO;
namespace JogoRpg.Domain.Interface.Services
{
    public interface ICharacterService : IBaseService<DTO.CharacterDTO>
    {
        Task<DTO.CharacterDTO> CreateCharacter(long userId, DTO.CharacterDTO characterDto);
        Task<DTO.CharacterDTO> Remove(long charId);
    }
}