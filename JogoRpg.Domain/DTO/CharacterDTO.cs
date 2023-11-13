namespace JogoRpg.Domain.DTO
{
    public class CharacterDTO
    {
        public long CharId { get; set; }
        public string? CharName { get; set; }
        public string? CharSex { get; set; }
        public CharacterClassType ClassType { get; set; }
    }
}