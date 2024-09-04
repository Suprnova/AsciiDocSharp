using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DList(string marker, DListItem[] items, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.DList;
        public required string Marker = marker;
        // TODO: validation that Items contains at least 1 item
        public required DListItem[] Items = items;
    }
}
