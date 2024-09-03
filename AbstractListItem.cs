using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class AbstractListItem : AbstractBlock
    {
        public required string Marker;

        public BaseInline[]? Principal { get; set; }
        public Block[]? Blocks { get; set; }
    }
}
