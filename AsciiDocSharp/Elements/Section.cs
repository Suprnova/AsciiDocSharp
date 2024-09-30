namespace AsciiDocSharp.Elements
{
    public class Section(
        int level,
        BaseInline title,
        Block[]? blocks = null,
        BaseInline? refText = null,
        Block? parent = null,
        Dictionary<string, string>? attributes = null
    ) : Heading(title, level, ElementType.Section, refText, parent, attributes)
    {
        public const ElementType Name = ElementType.Section;

        public Block[] Blocks { get; set; } = blocks ?? ([]);
    }
}
