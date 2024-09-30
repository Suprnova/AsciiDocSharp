namespace AsciiDocSharp.Elements
{
    public enum AdmonitionVariant
    {
        Caution,
        Important,
        Note,
        Tip,
        Warning,
    }

    // TODO: Ensure variant is set if name is admonition.
    public class ParentBlock(
        ElementType context,
        string delimiter,
        Block[]? blocks = null,
        AdmonitionVariant? variant = null,
        BaseInline? title = null,
        BaseInline? refText = null,
        Block? parent = null,
        Dictionary<string, string>? attributes = null
    ) : Block(context, title, refText, parent, attributes)
    {
        public string Delimiter = delimiter;
        public Block[] Blocks { get; set; } = blocks ?? [];

        public AdmonitionVariant? Variant = variant;
    }
}
