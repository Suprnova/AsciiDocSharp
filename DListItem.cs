using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DListItem(BaseInline[] terms, string marker, Block[]? blocks, BaseInline[]? principal, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : AbstractListItem(marker, blocks, principal, id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.DListItem;
        public required BaseInline[] Terms = terms;
    }
}
