namespace Shared.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string StateAcronym { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}