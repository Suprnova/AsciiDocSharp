using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DList : AbstractBlock
    {
        public const string Name = "dlist";
        public required string Marker;
        public required NotImplementedException[] Items;
    }
}
