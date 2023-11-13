using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Lancer)]
public class Lancer : CharactersInfo
{
    public Lancer()
    {
        InitializeStats();

        Strong = 8;
        Speed = 7;
        Vitality = 4;
        Inteligence = 1;
        Dexterity = 6;
        Stamina = 4;

        Description = "Lanceros são guerreiros habilidosos no combate corpo a corpo com lanças e outras armas de haste. " +
            "São conhecidos por sua habilidade em manter a distância e controlar o campo de batalha. " +
            "Sua precisão e alcance com lanças os tornam formidáveis em combates táticos.";
    }
}
