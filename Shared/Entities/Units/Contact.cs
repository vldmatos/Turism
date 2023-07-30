namespace Shared.Entities.Units
{
    public class Contact : Entity
    {
        public int PrimaryPhone { get; set; }
        public int SecondaryPhone { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
    }
}