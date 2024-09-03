using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum BlockType
    {
        Audio,
        Video,
        Image,
        TOC
    }

    public class BlockMacro : AbstractBlock
    {
        //TODO: Change all of these Name variants into one big enum.
        public required BlockType Name;
        public const string Form = "macro";

        public string? Target;
    }
}
