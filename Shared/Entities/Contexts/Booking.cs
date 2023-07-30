using Shared.Entities.Units;

namespace Shared.Entities.Contexts
{
    public class Booking : Entity
    {
        public User User { get; set; }

        public Hotel Hotel { get; set; }

        public decimal Price { get; set; }
    }
}