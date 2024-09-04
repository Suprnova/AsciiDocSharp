using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class DListItem : AbstractListItem
    {
        public const ElementType Name = ElementType.DListItem;
        public required BaseInline[] Terms;
    }
}
