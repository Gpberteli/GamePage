using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Witch)]
public class Witch : CharactersInfo
{
    public Witch()
    {
        InitializeStats();

        Strong = 1;
        Speed = 2;
        Vitality = 5;
        Inteligence = 10;
        Dexterity = 10;
        Stamina = 2;

        Description = "Bruxas são canalizadoras de magia arcana, capazes de conjurar feitiços poderosos e controlar os elementos. " +
            "Possuem um vasto conhecimento das artes místicas e são capazes de causar devastação com seus poderosos feitiços. " +
            "São temidas e reverenciadas por sua habilidade de manipular a magia a seu favor.";
    }
}
