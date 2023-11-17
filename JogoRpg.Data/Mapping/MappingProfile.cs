using AutoMapper;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Entities.CharacterClass;

namespace JogoRpg.Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CharacterDTO, CharacterDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<CharactersInfo, CharacterInfosDTO>();
        }
    }
}