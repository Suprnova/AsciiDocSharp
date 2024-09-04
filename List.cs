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

    public class List(string marker, ListVariant variant, ListItem[] items, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.List;
        public string Marker = marker;
        public ListVariant Variant = variant;
        // TODO: validation that Items contains at least 1 item
        public ListItem[] Items = items;
    }
}
