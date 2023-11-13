namespace JogoRpg.Domain.Entities
{
    public class ClassReference
    {
        public int ClassId { get; set; }
        public string? ClassName { get; set; }


        public ICollection<Character>? Characters { get; set; }
    }
}