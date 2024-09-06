namespace AsciiDocSharp.Elements
{
    // TODO: ensure name only accepts Audio, Video, Image, and TOC
    public class BlockMacro(
        ElementType name,
        string? target = null,
        string? id = null,
        BaseInline[]? title = null,
        BaseInline[]? refText = null,
        BlockMetadata? metadata = null,
        Location? location = null
    ) : Block(id, title, refText, metadata, location)
    {
        //TODO: Change all of these Name variants into one big enum.
        public ElementType Name = name;
        public const string Form = "macro";

        public string? Target = target;
    }
}
