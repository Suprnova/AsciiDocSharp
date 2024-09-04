using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum BreakVariant
    {
        Page,
        Thematic
    }

    public class Break : Block
    {
        public const ElementType Name = ElementType.Break;
        public required BreakVariant Variant;
    }
}
