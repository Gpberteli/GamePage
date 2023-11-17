namespace JogoRpg.Domain.DTO
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? NickName { get; set; }
        public string? UserEmail { get; set; }
        public List<CharacterDTO>? Characters { get; set; }
    }
}