namespace AsciiDocSharp.Elements;

public class DescriptionList(
    string marker,
    DescriptionListItem[] items,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
    ) : Block(ElementType.DList, title, refText, parent, attributes)
{
    public string Marker = marker;

    // TODO: validation that Items contains at least 1 item
    public DescriptionListItem[] Items = items;
}
