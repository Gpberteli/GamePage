using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;
using AutoMapper;
using JogoRpg.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Domain.DTO.CharacterDTO>> Get()
        {
            var characterEntities = await _characterRepository.Get();
            return _mapper.Map<IEnumerable<Domain.DTO.CharacterDTO>>(characterEntities);
        }

        public override async Task<Domain.DTO.CharacterDTO> Get(long charId)
        {
            var characterEntity = await _characterRepository.Get(charId);
            return _mapper.Map<Domain.DTO.CharacterDTO>(characterEntity);
        }

        public async Task<Domain.DTO.CharacterDTO> CreateCharacter(long userId, Domain.DTO.CharacterDTO characterDto)
        {
            var characterEntity = _mapper.Map<Domain.Entities.CharacterDTO>(characterDto);
            return _mapper.Map<Domain.DTO.CharacterDTO>(await _characterRepository.CreateCharacter(userId, characterEntity));
        }

        public override async Task<Domain.DTO.CharacterDTO> Update(Domain.DTO.CharacterDTO characterDto)
        {
            var characterEntity = _mapper.Map<Domain.Entities.CharacterDTO>(characterDto);
            return _mapper.Map<Domain.DTO.CharacterDTO>(await _characterRepository.Update(characterEntity));
        }

        public async Task<Domain.DTO.CharacterDTO> Remove(long charId)
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