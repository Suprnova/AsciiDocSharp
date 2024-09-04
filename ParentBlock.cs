using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum AdmonitionVariant
    {
        Caution,
        Important,
        Note,
        Tip,
        Warning
    }

    // TODO: Ensure variant is set if name is admonition.
    public class ParentBlock(ElementType name, string delimiter, Block[]? blocks = null, AdmonitionVariant? variant = null, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : Block(id, title, refText, metadata, location)
    {
        public
            ElementType Name = name;
        public const string Form = "delimited";
        public string Delimiter = delimiter;
        public Block[] Blocks { get; set; } = blocks ?? [];

        public AdmonitionVariant? Variant = variant;
    }
}
