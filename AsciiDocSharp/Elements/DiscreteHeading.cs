using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public class DiscreteHeading(int level, BaseInline[] title, string? id = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : AbstractHeading(level, title, id, refText, metadata, location)
    {
        public const ElementType Name = ElementType.Heading;
    }
}
