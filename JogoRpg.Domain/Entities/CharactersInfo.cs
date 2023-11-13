using System.ComponentModel.DataAnnotations;

namespace JogoRpg.Domain.Entities.CharacterClass;
    public class CharactersInfo
    {

        [Key]
        public int CharactersInfoId { get; set; }
        public int Strong { get; set; }
        public int Speed { get; set; }
        public int Vitality { get; set; }
        public int Inteligence { get; set; }
        public int Dexterity { get; set; }
        public int Stamina { get; set; }
        public string? Description { get; set; }

    public void InitializeStats()
    {
        var properties = GetType().GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(int) && property.CanWrite)
            {
                property.SetValue(this, 0); // Define o valor inicial como 0, você pode ajustar conforme necessário.
            }
        }
    }
}

