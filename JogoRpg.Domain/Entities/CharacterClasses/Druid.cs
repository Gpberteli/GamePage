using JogoRpg.Domain.Entities.CharacterClass;
using JogoRpg.Domain.Reflection;

namespace JogoRpg.Domain.Entities.CharacterClasses;

[CharacterClass(CharacterClassType.Druid)]
public class Druid : CharactersInfo
{
    public Druid()
    {
        InitializeStats();

        Strong = 1;
        Speed = 2;
        Vitality = 8;
        Intelligence = 9;
        Dexterity = 7;
        Stamina = 3;

        Description = "Druidas são sábios ligados à natureza, capazes de se comunicar com animais e controlar os elementos. " +
            "Eles usam magia verde para curar e proteger, além de invocar a força dos elementos para causar danos. " +
            "São protetores da vida selvagem e têm uma forte ligação com as florestas e áreas naturais.";
    }
}
