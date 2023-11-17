using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Necromant)]
public class Necromant : CharactersInfo
{
    public Necromant()
    {
        InitializeStats();

        Strong = 1;
        Speed = 3;
        Vitality = 7;
        Intelligence = 10;
        Dexterity = 7;
        Stamina = 2;

        Description = "Necromantes são usuários de magia negra que dominam o poder sobre a morte e os mortos. " +
            "São capazes de invocar esqueletos, zumbis e outras criaturas mortas-vivas para lutar ao seu lado. " +
            "Possuem habilidades sombrias que drenam a vida dos inimigos e manipulam os poderes da escuridão.";
    }
}
