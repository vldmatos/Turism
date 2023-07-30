namespace Shared.Entities.Units
{
    public class Policy : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsEnabled { get; set; }
    }
}