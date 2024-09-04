using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum ListVariant
    {
        Callout,
        Ordered,
        Unordered
    }

    public class List(string marker, ListVariant variant, ListItem[] items, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.List;
        public required string Marker = marker;
        public required ListVariant Variant = variant;
        // TODO: validation that Items contains at least 1 item
        public required ListItem[] Items = items;
    }
}
