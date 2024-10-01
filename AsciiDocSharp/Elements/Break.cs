namespace AsciiDocSharp.Elements;

public enum BreakVariant
{
    Page,
    Thematic,
}

public class Break(
    BreakVariant variant,
    BaseInline? title = null,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : Block(ElementType.Break, title, refText, parent, attributes)
{
    public BreakVariant Variant = variant;
}
