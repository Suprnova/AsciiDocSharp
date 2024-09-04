using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class AbstractListItem : AbstractBlock
    {
        // TODO: This does not include Section objects, should create a new class or add validation
        public required Block[]? Blocks { get; set; } = [];
        public required string Marker;

        public BaseInline[]? Principal { get; set; }
    }
}
