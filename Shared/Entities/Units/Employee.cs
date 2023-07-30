namespace Shared.Entities.Units
{
    public class Employee : Entity
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public Contact Contact { get; set; }
    }
}