using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public enum BreakVariant
    {
        Page,
        Thematic
    }

    public class Break(BreakVariant variant, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.Break;
        public BreakVariant Variant = variant;
    }
}
