using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public abstract class Block(string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : AbstractBlock(id, title, refText, metadata, location)
    {
    }
}
