using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DListItem(BaseInline[] terms, string marker, Block[]? blocks = null, BaseInline[]? principal = null, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : AbstractListItem(marker, blocks, principal, id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.DListItem;
        public BaseInline[] Terms = terms;
    }
}
