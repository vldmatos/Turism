namespace Shared.Entities.Units
{
    public class Event : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Contact Contact { get; set; }

        public ExhibitionCenter ExhibitionCenter { get; set; }

        public Organizer Organizer { get; set; }

        public List<Service> Services { get; set; }
    }
}