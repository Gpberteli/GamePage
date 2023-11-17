using JogoRpg.Domain.Entities;
using JogoRpg.Domain.Enum;

namespace JogoRpg.Domain.DTO
{
    public class CharacterDTO
    {
        public long CharId { get; set; }
        public string? CharName { get; set; }
        public Sex CharSex { get; set; }
        public CharacterClassType ClassType { get; set; }
        public CharacterInfosDTO CharStatus { get; set; }
        public long UserId { get; set; }
        public long ClassId { get; set; }
        public User User { get; set; }  // Adicionando a referência ao User
    }
}