using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class AbstractListItem(string marker, Block[]? blocks, BaseInline[]? principal, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : AbstractBlock(id, title, refText, metadata, location)
    {
        // TODO: This does not include Section objects, should create a new class or add validation
        public required Block[]? Blocks { get; set; } = blocks ?? ([]);
        public required string Marker = marker;

        public BaseInline[]? Principal = principal;
    }
}
