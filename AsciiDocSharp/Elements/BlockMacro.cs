namespace AsciiDocSharp.Elements;

// TODO: ensure context only accepts Audio, Video, Image, and TOC
public class BlockMacro(
    ElementType context,
    string? target = null,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : Block(context, title, refText, parent, attributes)
{
    public string? Target = target;
}
