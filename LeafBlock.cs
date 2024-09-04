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

    // TODO: Ensure name only accepts Listing, Literal, Paragraph, Pass, Stem, and Verse.
    // TODO: Ensure delimiter is set if form is Delimited
    public class LeafBlock(ElementType name, LeafBlockForm form, BaseInline[]? inlines, string? delimiter, string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location) : Block(id, title, refText, metadata, location)
    {
        public required ElementType Name = name;
        public required BaseInline[] Inlines = inlines ?? [];
        public required LeafBlockForm Form = form;

        public string? Delimiter = delimiter;
    }
}
