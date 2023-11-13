using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;
using static JogoRpg.Services.Services.CharacterService;
using System;

namespace JogoRpg.Services.Services;

public class CharacterService : BaseService<Character>, ICharacterService
{
    private readonly ICharacterRepository _characterRepository;

    public CharacterService(ICharacterRepository characterRepository)
        : base(characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<Character>> Get()
    {
        return await _characterRepository.GetAsync();
    }

    public override async Task<Character> Get(long charId)
    {
        return await _characterRepository.GetAsync(charId);
    }

    public async Task<Character> CreateCharacter(long userId, Character character)
    {
        return await _characterRepository.CreateCharacter(userId, character);
    }

    public override async Task<Character> Update(Character character)
    {
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> Remove(long charId)
    {
        var character = await _characterRepository.GetAsync(charId);
        if (character != null)
        {
            return await _characterRepository.RemoveAsync(character);
        }
        return null;
    }
}

