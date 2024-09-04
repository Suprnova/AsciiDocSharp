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
    public class ParentBlock(ElementType name, string delimiter, Block[]? blocks, AdmonitionVariant? variant, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        public required ElementType Name = name;
        public const string Form = "delimited";
        public required string Delimiter = delimiter;
        public required Block[] Blocks { get; set; } = blocks ?? [];

        public AdmonitionVariant? Variant = variant;
    }
}
