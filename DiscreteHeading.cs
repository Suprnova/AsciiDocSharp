using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DiscreteHeading(int level, BaseInline[] title, string? id, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : AbstractHeading(level, title, id, refText, metadata, location)
    {
        public const ElementType Name = ElementType.Heading;
    }
}
