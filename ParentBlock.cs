using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum AdmonitionVariant
    {
        Caution,
        Important,
        Note,
        Tip,
        Warning
    }

    public class ParentBlock : Block
    {
        public required ElementType Name;
        public const string Form = "delimited";
        public required string Delimiter;
        public required Block[] Blocks { get; set; } = [];

        public AdmonitionVariant? Variant;
    }
}
