using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class AbstractListItem(string marker, Block[]? blocks = null, BaseInline[]? principal = null, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : AbstractBlock(id, title, refText, metadata, location)
    {
        // TODO: This does not include Section objects, should create a new class or add validation
        public Block[]? Blocks { get; set; } = blocks ?? ([]);
        public string Marker = marker;

        public BaseInline[]? Principal = principal;
    }
}
