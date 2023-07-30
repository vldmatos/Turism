using Shared.Models;

namespace Shared.Entities.Units
{
    public class Hotel : Entity
    {
        public string Name { get; set; }

        public HotelCategory Category { get; set; }

        public string Description { get; set; }

        public Address Address { get; set; }

        public Contact Contact { get; set; }

        public DateTime Checkin { get; set; }

        public DateTime Checkout { get; set; }
    }

    public enum HotelCategory
    {
        Star_01 = 1,
        Star_02 = 2,
        Star_03 = 3,
        Star_04 = 4,
        Star_05 = 5
    }
}