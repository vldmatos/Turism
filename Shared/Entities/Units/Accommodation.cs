namespace Shared.Entities.Units
{
    public class Accommodation : Entity
    {
        public string Name { get; set; }

        public char Acronym { get; set; }

        public AccomodationType Type { get; set; }
    }

    public enum AccomodationType
    {
        Single,
        Double,
        Triple
    }
}