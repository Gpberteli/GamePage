using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.DarkWizard)]
public class DarkWizard : CharactersInfo
{
    public DarkWizard()
    {
        InitializeStats();

        Strong = 1;
        Speed = 2;
        Vitality = 6;
        Inteligence = 10;
        Dexterity = 9;
        Stamina = 2;

        Description = "Os Feiticeiros das Trevas são especialistas nas artes arcanas negras. " +
            "Eles manipulam energias sombrias para conjurar magias poderosas capazes de causar destruição em massa." +
            "Possuem um vasto conhecimento de feitiços ofensivos e habilidades para controlar as forças ocultas. " +
            "Sua magia sombria os torna temidos e respeitados no campo de batalha.";
    }
}
