using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public enum LeafBlockForm
    {
        Delimited,
        Indented,
        Paragraph
    }

    // TODO: Ensure name only accepts Listing, Literal, Paragraph, Pass, Stem, and Verse.
    // TODO: Ensure delimiter is set if form is Delimited
    public class LeafBlock(ElementType name, LeafBlockForm? form = null, BaseInline[]? inlines = null, string? delimiter = null, string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : Block(id, title, refText, metadata, location)
    {
        public


            ElementType Name = name;
        public BaseInline[] Inlines = inlines ?? [];
        // what does a null form mean
        public LeafBlockForm? Form = form;

        public string? Delimiter = delimiter;
    }
}
