using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public abstract class Block(string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : AbstractBlock(id, title, refText, metadata, location)
    {
    }
}
