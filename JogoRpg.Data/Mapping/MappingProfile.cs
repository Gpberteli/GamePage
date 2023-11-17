using AutoMapper;
using JogoRpg.Domain.DTO;
using JogoRpg.Domain.Entities;

namespace JogoRpg.Data.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CharacterDTO, CharacterDTO>();
        CreateMap<User, UserDTO>();
    }
}
