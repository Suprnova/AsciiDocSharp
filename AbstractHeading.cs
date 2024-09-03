using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public abstract class AbstractHeading : AbstractBlock
    {
        public required int Level { get; set; }
    }
    // TODO: Add a check in constructor to ensure Title is set.
}
