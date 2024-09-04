using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DList : Block
    {
        public const ElementType Name = ElementType.DList;
        public required string Marker;
        public required NotImplementedException[] Items;
    }
}
