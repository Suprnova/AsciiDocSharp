using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class BlockMacro : Block
    {
        //TODO: Change all of these Name variants into one big enum.
        public required ElementType Name;
        public const string Form = "macro";

        public string? Target;
    }
}
