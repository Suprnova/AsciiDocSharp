namespace AsciiDocSharp.Elements;

public class DiscreteHeading(
    BaseInline title,
    int level,
    BaseInline? refText = null,
    Block? parent = null,
    Dictionary<string, string>? attributes = null
) : Heading(title, level, ElementType.Heading, refText, parent, attributes)
{
    public const ElementType Name = ElementType.Heading;
}
