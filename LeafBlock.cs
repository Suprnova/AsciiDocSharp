using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum LeafBlockType
    {
        Listing,
        Literal,
        Paragraph,
        Pass,
        Stem,
        Verse
    }

    public enum LeafBlockForm
    {
        Delimited,
        Indented,
        Paragraph
    }

    public class LeafBlock : AbstractBlock
    {
        public required LeafBlockType Name {  get; set; }
        public required BaseInline[] Inlines { get; set; } = [];

        public LeafBlockForm Form { get; set; }
    }
}
