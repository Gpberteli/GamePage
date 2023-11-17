using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Fairy)]
public class Fairy : CharactersInfo
{
    public Fairy()
    {
        InitializeStats();

        Strong = 1;
        Speed = 2;
        Vitality = 5;
        Intelligence = 10;
        Dexterity = 8;
        Stamina = 2;

        Description = "As fadas são seres mágicos e etéreos, conhecidos por sua graça e poderes encantadores. " +
            "Elas têm uma conexão especial com a natureza e a magia, permitindo-lhes curar feridas, lançar feitiços de proteção e até mesmo conceder bênçãos aos aliados. " +
            "Apesar de sua aparência delicada, as fadas são forças formidáveis no campo de batalha, capazes de influenciar eventos com magia pura e positiva.";
    }
}
