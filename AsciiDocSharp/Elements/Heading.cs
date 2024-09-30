namespace AsciiDocSharp.Elements
{
    // TODO: Confirm if Section (which inherits AbstractHeading) should be considered a Block type.
    public abstract class Heading(
        BaseInline title,
        int level,
        ElementType context,
        BaseInline? refText = null,
        Block? parent = null,
        Dictionary<string, string>? attributes = null
    ) : Block(context, title, refText, parent, attributes)
    {
        public int Level { get; set; } = level;
    }
}
