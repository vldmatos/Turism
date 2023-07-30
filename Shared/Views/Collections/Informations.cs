using Shared.Entities.Units;
using System.Collections;

namespace Shared.Views.Collections
{
    public class Informations : View, IEnumerable<Information>
    {
        private readonly IEnumerable<Information> informations;
        public Informations(IEnumerable<Information> informations)
        {
            this.informations = informations;
        }

        public IEnumerator<Information> GetEnumerator() => informations?.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
