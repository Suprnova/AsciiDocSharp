using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum ListVariant
    {
        Callout,
        Ordered,
        Unordered
    }

    public class List : AbstractBlock
    {
        public const string Name = "list";
        public required string Marker;
        public required ListVariant Variant;
        public required NotImplementedException[] Items;
    }
}
