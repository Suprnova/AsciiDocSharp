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

    public class List : Block
    {
        public const ElementType Name = ElementType.List;
        public required string Marker;
        public required ListVariant Variant;
        public required NotImplementedException[] Items;
    }
}
