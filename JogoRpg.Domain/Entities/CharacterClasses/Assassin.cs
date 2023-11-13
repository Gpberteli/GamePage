using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Assassin)]
public class Assassin : CharactersInfo
{
    public Assassin()
    {
        InitializeStats();

        Strong = 7;
        Speed = 10;
        Vitality = 4;
        Inteligence = 2;
        Dexterity = 3;
        Stamina = 2;

        Description = "Os assassinos são mestres na arte do sigilo e da eliminação silenciosa." +
            "Usando adagas afiadas e habilidades furtivas, eles se movem nas sombras, atacando seus alvos de surpresa. " +
            "São conhecidos por sua agilidade e habilidades de evasão, tornando-os excelentes em emboscadas e ataques rápidos.";
    }
}
