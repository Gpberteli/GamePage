namespace JogoRpg.Domain.Reflection;

[AttributeUsage(AttributeTargets.Class)]
public class CharacterClassAttribute : Attribute
{
    public CharacterClassType Type { get; }

    public CharacterClassAttribute(CharacterClassType type)
    {
        Type = type;
    }
}

