using AutoMapper;
using JogoRpg.Data.Context;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

namespace JogoRpg.Data.Services
{
    public class MeuServico
    {
        private readonly IMapper _mapper;
        private readonly EntityContext _context;

        public MeuServico(IMapper mapper, EntityContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public CharacterDTO ObterCharacterDTO(int characterId)
        {
            CharacterDTO character = _context.Characters.FirstOrDefault(c => c.CharId == characterId);
            CharacterDTO characterDTO = _mapper.Map<CharacterDTO>(character);

            return characterDTO;
        }
    }
}