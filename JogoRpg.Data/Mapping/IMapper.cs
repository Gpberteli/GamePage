using AutoMapper;
using JogoRpg.Data.Context;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

public class MeuServico
{
    private readonly IMapper _mapper;
    private readonly EntityContext _context;

    public MeuServico(IMapper mapper, EntityContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public JogoRpg.Domain.DTO.CharacterDTO ObterCharacterDTO(int characterId)
    {
        JogoRpg.Domain.Entities.CharacterDTO character = _context.Characters.FirstOrDefault(c => c.CharId == characterId);
        JogoRpg.Domain.DTO.CharacterDTO characterDTO = _mapper.Map<JogoRpg.Domain.DTO.CharacterDTO>(character);

        return characterDTO;
    }
}