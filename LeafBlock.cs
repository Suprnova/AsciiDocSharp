using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum LeafBlockForm
    {
        Delimited,
        Indented,
        Paragraph
    }

    public class LeafBlock : Block
    {
        public required ElementType Name {  get; set; }
        public required BaseInline[] Inlines { get; set; } = [];

        public LeafBlockForm Form { get; set; }
    }
}
