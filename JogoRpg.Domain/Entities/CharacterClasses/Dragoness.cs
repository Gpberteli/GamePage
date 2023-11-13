using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Dragoness)]
public class Dragoness : CharactersInfo
{
    public Dragoness()
    {
        InitializeStats();

        Strong = 7;
        Speed = 5;
        Vitality = 5;
        Inteligence = 5;
        Dexterity = 3;
        Stamina = 5;

        Description = "As Dragoness são guerreiras lendárias que possuem uma conexão profunda com os dragões. " +
            "Elas podem canalizar a força e os poderes dessas criaturas míticas em batalha. " +
            "Com escamas e asas de dragão, são capazes de desencadear fúria e poder inigualáveis. " +
            "Além disso, possuem habilidades elementais que envolvem fogo, gelo ou raio, dependendo do tipo de dragão com o qual têm afinidade. " +
            "As Dragoness são temíveis no campo de batalha e representam uma força imparável quando em sintonia com seus companheiros alados.";
    }
}
