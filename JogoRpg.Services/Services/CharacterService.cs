using AutoMapper;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;

namespace JogoRpg.Services.Services
{
    public class CharacterService : BaseService<Domain.DTO.CharacterDTO>, ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository characterRepository, IMapper mapper)
            : base(characterRepository)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<Domain.DTO.CharacterDTO> CreateCharacter(long userId, Domain.DTO.CharacterDTO characterDto)
        {
            var characterEntity = _mapper.Map<Domain.DTO.CharacterDTO>(characterDto);
            return _mapper.Map<Domain.DTO.CharacterDTO>(await _characterRepository.CreateCharacter(userId, characterEntity));
        }

        public override async Task<Domain.DTO.CharacterDTO> Remove(long charId)
        {
            var characterDto = await _characterRepository.Get(charId);
            if (characterDto != null)
            {
                return _mapper.Map<Domain.DTO.CharacterDTO>(await _characterRepository.Remove(characterDto));
            }
            return null;
        }
    }
}