using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace JogoRpg.Domain.Entities;

public class Character
{
    public long CharId { get; set; }
    public string? CharName { get; set; }
    public CharactersInfo? CharStatus { get; set; }
    public Sex CharSex { get; set; }

    [ForeignKey("ClassId")]
    public CharacterClassType ClassType { get; set; }

    public int ClassId { get; set; }

    public string GetClassName()
    {
        return ClassType.ToString();
    }

    public ClassReference? ClassReference { get; set; }

    public User? User { get; set; }
    public long UserId { get; set; }

}
