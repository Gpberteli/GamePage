using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Warrior)]
public class Warrior : CharactersInfo
{
    public Warrior()
    {
        InitializeStats();

        Strong = 10;
        Speed = 2;
        Vitality = 7;
        Intelligence = 1;
        Dexterity = 2;
        Stamina = 8;

        Description = "Guerreiros são combatentes robustos e especialistas em combate corpo a corpo." +
            "Usam uma variedade de armas e armaduras pesadas para enfrentar os inimigos de frente. " +
            "São conhecidos por sua força física e habilidades de defesa, sendo os pilares de resistência em batalha.";
    }
}
