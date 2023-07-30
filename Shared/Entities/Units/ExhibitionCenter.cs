using Shared.Models;

namespace Shared.Entities.Units
{
    public class ExhibitionCenter : Entity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
    }
}