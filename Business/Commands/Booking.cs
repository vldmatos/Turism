using Contexts = Shared.Entities.Contexts;

namespace Business.Commands
{
    internal class Booking : ICommand<Contexts.Booking>
    {
        public Contexts.Booking Context { get; private set; }

        public ICommand<Contexts.Booking> AssociatedCommand { get; }

        public PriorityQueue<string, int> Services { get; } = new();

        public Booking(Contexts.Booking booking)
        {
            Context = booking;

            Services.Enqueue(AvailableServices.Save, 1);
        }

        public Task<Contexts.Booking> Execute()
        {
            Context.Price = (int)Context.Hotel.Category * 300;

            return Task.FromResult(Context);
        }

        public Task<IEnumerable<string>> Verify()
        {
            var errors = new List<string>();

            if (!Context.Hotel.Enabled)
                errors.Add($"The Hotel is disabled");

            if (!Context.User.Enabled)
                errors.Add($"The Hotel is disabled");

            return Task.FromResult(errors.AsEnumerable());
        }
    }
}