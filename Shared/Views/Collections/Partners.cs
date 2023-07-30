using Shared.Entities.Units;
using System.Collections;

namespace Shared.Views.Collections
{
    public class Partners : View, IEnumerable<Partner>
    {
        private readonly IEnumerable<Partner> partners;
        public Partners(IEnumerable<Partner> partners)
        {
            this.partners = partners;
        }

        public IEnumerator<Partner> GetEnumerator() => partners?.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
