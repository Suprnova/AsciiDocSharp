namespace AsciiDocSharp.Elements;

public enum LeafBlockForm
{
    Delimited,
    Indented,
    Paragraph,
}

// TODO: Ensure name only accepts Listing, Literal, Paragraph, Pass, Stem, and Verse.
// TODO: Ensure delimiter is set if form is Delimited
public class LeafBlock(
    ElementType context,
    LeafBlockForm? form = null,
    BaseInline[]? inlines = null,
    string? delimiter = null,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : Block(context, title, refText, parent, attributes)
{
    public BaseInline[] Inlines = inlines ?? [];

    // what does a null form mean
    public LeafBlockForm? Form = form;

    public string? Delimiter = delimiter;
}
