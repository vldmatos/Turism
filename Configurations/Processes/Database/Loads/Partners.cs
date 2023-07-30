using Units = Shared.Entities.Units;

namespace Configurations.Processes.Database.Loads
{
    internal class Partners
    {
        internal static Units.Partner[] Get()
        {
            var partners = new Units.Partner[]
            {
                new Units.Partner(){ Name = "Binesty" },
                new Units.Partner(){ Name = "Booking.com" },
                new Units.Partner(){ Name = "Kayak" },
                new Units.Partner(){ Name = "Samup" }
            };

            return partners;
        }
    }
}