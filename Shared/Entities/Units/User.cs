namespace Shared.Entities.Units
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}