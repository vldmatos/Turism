using Units = Shared.Entities.Units;

namespace Configurations.Processes.Database.Loads
{
    internal class Informations
    {
        internal static Units.Information[] Get()
        {
            var informations = new Units.Information[]
           {
                new Units.Information()
                {
                    Title = "About Us",
                    Description = @"Founded in 1996, Via HG Turismo has always had the goal of making sure our clients have the best travel experience, offering the best structure and support to attend to our partners’ most diverse requests.
                                    Focused on excellence and personalized attendment, we count with a team of highly qualified and trained consultants to provide safety, trust and comfort, making your trip even better."
                },

                new Units.Information()
                {
                    Title = "Corporate",
                    Description = @"Our corporate department is formed by professionals specialized in business tourism offering your company the best service in corporate travel aiming your comfort, safety and economy mainly.
                                    We conduct a research and analysis so that each costumer has their needs met in an exclusive and personalized way.
                                    Our team holds the entire structure and identification of management appropriate to the profile and the turnover of your company in corporate travel programs."
                },

                 new Units.Information()
                {
                    Title = "Incentive",
                    Description = @"Travels are unforgettable and are in the top of wishes list of any person. They can be an excellent way to increase the search for better results, to reward and recognize the dedication and the talent of
                                    the collaborators. Thinking in that, Via HG Turismo offers the service of incentive travel . We work with a Specialized Team to assist you in the entire planning and execution process of your trip!
                                    Our goal is to take care of every detail and make dreams come true!
                                    COME MAKE YOUR TRIP WITH US!"
                },
           };

            return informations;
        }
    }
}