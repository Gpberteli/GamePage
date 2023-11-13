using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Shooter)]
public class Shooter : CharactersInfo
{
    public Shooter()
    {
        InitializeStats();

        Strong = 1;
        Speed = 9;
        Vitality = 3;
        Inteligence = 1;
        Dexterity = 10;
        Stamina = 6;

        Description = "Os atiradores são especialistas em combate de curta a média distância, habilidosos no uso de pistolas e revólveres. " +
            "Com uma destreza incrível, são capazes de disparar rajadas de tiros precisos e letais contra seus oponentes. " +
            "Além das habilidades de tiro, possuem uma agilidade notável que lhes permite se movimentar rapidamente no campo de batalha, esquivando-se de ataques inimigos. " +
            "A combinação de velocidade e precisão faz dos Shooters combatentes formidáveis em qualquer confronto, sendo especialmente eficazes em situações de combate rápido e cercos táticos.";
    }
}
