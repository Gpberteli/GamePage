using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.DarkPriest)]
public class DarkPriest : CharactersInfo
{
    public DarkPriest()
    {
        InitializeStats();

        Strong = 1;
        Speed = 2;
        Vitality = 6;
        Intelligence = 10;
        Dexterity = 9;
        Stamina = 2;

        Description = "O Sacerdote das Trevas, ou Dark Priest, é um praticante de magia obscura e sinistra. " +
            "Ao contrário dos sacerdotes tradicionais, eles canalizam o poder das trevas para conjurar magias destrutivas e controlar as energias sombrias. " +
            "Possuem a capacidade de infligir maldições e causar danos terríveis aos inimigos. " +
            "São temidos e respeitados por sua habilidade de manipular forças ocultas.";
    }
}
