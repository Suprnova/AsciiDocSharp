namespace AsciiDocSharp.Elements;

public class DescriptionListItem(
    string marker,
    BaseInline[] terms,
    Block[]? blocks = null,
    BaseInline[]? principal = null,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : AbstractListItem(marker, ElementType.DListItem, blocks, principal, title, refText, parent, attributes)
{
    public BaseInline[] Terms = terms;
}
