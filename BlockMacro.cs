using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    // TODO: ensure name only accepts Audio, Video, Image, and TOC
    public class BlockMacro(ElementType name, string? target, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        //TODO: Change all of these Name variants into one big enum.
        public required ElementType Name = name;
        public const string Form = "macro";

        public string? Target = target;
    }
}
