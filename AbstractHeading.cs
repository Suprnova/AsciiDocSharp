using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    // TODO: Confirm if Section (which inherits AbstractHeading) should be considered a Block type.
    public abstract class AbstractHeading(int level, BaseInline[] title, string? id, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        public required int Level { get; set; } = level;
    }
}
