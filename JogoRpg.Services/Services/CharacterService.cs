using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;
using static JogoRpg.Services.Services.CharacterService;
using System;
using AutoMapper;
using JogoRpg.Domain.DTO;

namespace JogoRpg.Services.Services;

public class CharacterService : BaseService<Character>, ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    public CharacterService(ICharacterRepository characterRepository, IMapper mapper)
        : base(characterRepository)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Character>> Get()
    {
        var characterEntities = await _characterRepository.Get();
        return _mapper.Map<IEnumerable<Character>>(characterEntities);
    }

    public override async Task<Character> Get(long charId)
    {
        var characterEntities = await _characterRepository.Get();
        return _mapper.Map<Character>(characterEntities);
    }

    public async Task<Character> CreateCharacter(long userId, Character characterDto)
    {
        return await _characterRepository.CreateCharacter(userId, characterDto);
    }

    public override async Task<Character> Update(Character characterDto)
    {
        return await _characterRepository.Update(characterDto);
    }

    public async Task<Character> Remove(long charId)
    {
        var characterDto = await _characterRepository.Get(charId);
        if (characterDto != null)
        {
            return await _characterRepository.Remove(characterDto);
        }
        return null;
    }
}

