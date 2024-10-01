namespace AsciiDocSharp.Elements;

public class AbstractListItem(
    string marker,
    ElementType context,
    Block[]? blocks = null,
    BaseInline[]? principal = null,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : Block(context, title, refText, parent, attributes)
{
    // TODO: This does not include Section objects, should create a new class or add validation
    public Block[]? Blocks { get; set; } = blocks ?? ([]);
    public string Marker = marker;

    public BaseInline[]? Principal = principal;
}
