using AutoMapper;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

namespace JogoRpg.Data.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Character, CharacterDTO>();
        CreateMap<User, UserDTO>();

    }
   
}
