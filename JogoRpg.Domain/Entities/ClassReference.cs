using JogoRpg.Domain.DTO;

namespace JogoRpg.Domain.Entities
    {
        public class ClassReference
        {
            public int ClassId { get; set; }
            public string? ClassName { get; set; }
            public ICollection<CharacterDTO>? Characters { get; set; }
        }
    }