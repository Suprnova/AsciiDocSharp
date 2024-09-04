using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class ListItem(string marker, BaseInline[] principal, Block[]? blocks, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : AbstractListItem(marker, blocks, principal, id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.ListItem;
    }

    // TODO: Add a clause to the constructor that ensures principal is populated.
}
