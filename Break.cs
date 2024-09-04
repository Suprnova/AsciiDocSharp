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

    public class Break(BreakVariant variant, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.Break;
        public required BreakVariant Variant = variant;
    }
}
