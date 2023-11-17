using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Monk)]
public class Monk : CharactersInfo
{
    public Monk()
    {
        InitializeStats();

        Strong = 7;
        Speed = 7;
        Vitality = 4;
        Intelligence = 5;
        Dexterity = 3;
        Stamina = 4;

        Description = "Os monges são mestres das artes marciais, treinados na disciplina do corpo e da mente. " +
            "Com habilidades aprimoradas de combate desarmado, são capazes de desferir golpes precisos e poderosos. " +
            "Além disso, possuem técnicas de meditação que lhes concedem agilidade e resistência sobrenaturais. " +
            "Os monges são conhecidos por sua busca pela perfeição física e espiritual, utilizando sua força interior para superar desafios e proteger os indefesos. " +
            "Eles são guerreiros altamente disciplinados que buscam o equilíbrio entre corpo e alma..";
    }
}
