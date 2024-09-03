using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum ParentBlockType
    {
        Admonition,
        Example,
        Sidebar,
        Open,
        Quote
    }

    public enum AdmonitionVariant
    {
        Caution,
        Important,
        Note,
        Tip,
        Warning
    }

    public class ParentBlock
    {
        public required ParentBlockType Type;
        public const string Form = "delimited";
        public required string Delimiter;
        public required Block[] Blocks { get; set; } = [];

        public AdmonitionVariant? Variant;
    }
}
