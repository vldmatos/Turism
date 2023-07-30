using Shared.Entities;

namespace Business
{
    internal interface ICommand<TContext> where TContext : Entity
    {
        TContext Context { get; }

        ICommand<TContext> AssociatedCommand { get; }

        PriorityQueue<string, int> Services { get; }

        Task<TContext> Execute();

        Task<IEnumerable<string>> Verify();
    }
}