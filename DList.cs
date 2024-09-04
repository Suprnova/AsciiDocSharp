using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DList(string marker, DListItem[] items, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.DList;
        public string Marker = marker;
        // TODO: validation that Items contains at least 1 item
        public DListItem[] Items = items;
    }
}
