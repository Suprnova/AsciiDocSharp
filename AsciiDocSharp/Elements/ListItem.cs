namespace AsciiDocSharp.Elements;

public class ListItem(
    string marker,
    BaseInline[] principal,
    Block[]? blocks = null,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : AbstractListItem(marker, ElementType.ListItem, blocks, principal, title, refText, parent, attributes)
{
}

// TODO: Add a clause to the constructor that ensures principal is populated.
