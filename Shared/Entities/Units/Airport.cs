using Shared.Models;

namespace Shared.Entities.Units
{
    public class Airport : Entity
    {
        public string Name { get; set; }

        public string Acronym { get; set; }

        public Address Address { get; set; }
    }
}