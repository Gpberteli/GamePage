using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Priest)]
public class Priest : CharactersInfo
{
    public Priest()
    {
        InitializeStats();

        Strong = 1;
        Speed = 1;
        Vitality = 7;
        Inteligence = 10;
        Dexterity = 8;
        Stamina = 3;

        Description = "Sacerdotes são devotos de divindades ou forças superiores, dedicados à cura e proteção." +
            " Possuem habilidades de cura poderosas para restaurar a saúde dos aliados e expulsar o mal. " +
            "Além disso, podem lançar bênçãos e encantamentos para fortalecer a si mesmos e aos seus companheiros.";
    }
}
