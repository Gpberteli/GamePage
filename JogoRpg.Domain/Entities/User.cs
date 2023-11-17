namespace JogoRpg.Domain.Entities;

public class User
{
    public long UserId { get; set; }
    public string? UserName { get; set; }
    public string? NickName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPassword { get; private set; }
    public List<CharacterDTO>? Characters { get; set; }

}
