using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Section(int level, BaseInline[] title, SectionBody[]? blocks, string? id, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : AbstractHeading(level, title, id, refText, metadata, location)
    {
        public const ElementType Name = ElementType.Section;

        public required SectionBody[] Blocks { get; set; } = blocks ?? ([]);
    }
}
