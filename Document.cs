using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Document
    {
        public const ElementType Name = ElementType.Document;
        public const PositionType Type = PositionType.Block;

        public Dictionary<string, string>? Attributes;
        public Heading? Heading;
        public SectionBody[]? Blocks;
        public Location? Location;

        public Document(Dictionary<string, string>? attributes, Heading? heading, SectionBody[]? blocks, Location? location)
        {
            if (heading is not null)
            {
                if (attributes is null)
                {
                    throw new InvalidOperationException("A document that has a header must have attributes assigned to it.");
                }
            }

            Attributes = attributes;
            Heading = heading;
            Blocks = blocks;
            Location = location;
        }

        public static Document FromString(IEnumerable<string> strings)
        {
            throw new NotImplementedException();
        }
    }

    public class Heading
    {
        public BaseInline[]? Title;
        public Author[]? Authors;
        public Location? Location;
    }
}
