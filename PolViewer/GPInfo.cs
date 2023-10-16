using System.Collections.Generic;
using System.Reflection.Emit;

namespace PolViewer
{
    internal class GPInfo : ValueObject<GPInfo>
    {
        internal string Name { get; set; }
        internal string Guid { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Guid;
        }
    }
}
