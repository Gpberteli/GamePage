using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Hunter)]
public class Hunter : CharactersInfo
{
    public Hunter()
    {
        InitializeStats();

        Strong = 1;
        Speed = 7;
        Vitality = 3;
        Inteligence = 1;
        Dexterity = 10;
        Stamina = 8;

        Description = "Caçadores são mestres em rastreamento e combate a distância. " +
            "Armados com arcos e flechas, eles são especialistas em abater presas de longe. " +
            "Possuem grande habilidade em identificar e explorar fraquezas nos inimigos, tornando-os eficazes tanto em caçadas como em batalhas.";
    }
}
