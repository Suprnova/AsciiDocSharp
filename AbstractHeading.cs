using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    // TODO: Confirm if Section (which inherits AbstractHeading) should be considered a Block type.
    public abstract class AbstractHeading : Block
    {
        public required int Level { get; set; }
    }
    // TODO: Add a check in constructor to ensure Title is set.
}
