namespace AsciiDocSharp.Elements;

public enum ListVariant
{
    Callout,
    Ordered,
    Unordered,
}

public class List(
    string marker,
    ListVariant variant,
    ListItem[] items,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : Block(ElementType.List, title, refText, parent, attributes)
{
    public string Marker = marker;
    public ListVariant Variant = variant;

    // TODO: validation that Items contains at least 1 item
    public ListItem[] Items = items;
}
