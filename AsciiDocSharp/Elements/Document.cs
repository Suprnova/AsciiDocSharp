using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public class Document
    {
        public const ElementType Name = ElementType.Document;
        public const PositionType Type = PositionType.Block;

        public Dictionary<string, string>? Attributes;
        public Header? Header;
        public Block[]? Blocks;
        public Location? Location;

        public Document(Dictionary<string, string>? attributes = null, Header? header = null, Block[]? blocks = null, Location? location = null)
        {
            if (header is not null)
            {
                if (attributes is null)
                {
                    // Documentation is unclear, ask team
                    //throw new InvalidOperationException("A document that has a header must have attributes assigned to it.");
                }
            }

            Attributes = attributes;
            Header = header;
            Blocks = blocks;
            Location = location;
        }

        public static Document FromString(IEnumerable<string> strings)
        {
            throw new NotImplementedException();
        }
    }

    public class Header(BaseInline[]? title = null, Author[]? authors = null, Location? location = null)
    {
        public BaseInline[]? Title = title;
        public Author[]? Authors = authors;
        public Location? Location = location;
    }
}
