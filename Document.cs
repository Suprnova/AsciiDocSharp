using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Document
    {
        public const string Name = "document";
        public const string Type = "block";

        public Dictionary<string, string>? Attributes;
        public Heading? Heading;
        public SectionBody[]? Blocks;
        public Location? Location;
    }

    public class Heading
    {
        public BaseInline[]? Title;
        public NotImplementedException[]? Authors;
        public Location? Location;
    }
}
