namespace AsciiDocSharp.Elements
{
    // TODO: Confirm if Section (which inherits AbstractHeading) should be considered a Block type.
    public abstract class AbstractHeading(
        int level,
        BaseInline[] title,
        string? id = null,
        BaseInline[]? refText = null,
        BlockMetadata? metadata = null,
        Location? location = null
    ) : Block(id, title, refText, metadata, location)
    {
        public int Level { get; set; } = level;
    }
}
